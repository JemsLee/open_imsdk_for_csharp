using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using open_imsdk_for_cs.observer;
using WebSocketSharp;

namespace open_imsdk_for_cs
{
    public class OpenWebSocket
    {

        public String ipAndPort = "";
        public String fromUid = "";
        public String needAck = "";
        public String token = "";
        public String deviceId = "";
        public String fbFlag = "";
        public String secKey = "";

        public bool isLogin = false;
        bool isStarted = false;

        public OBPublishSub oBPublishSub;

        public WebSocket ws ;
        System.Timers.Timer pingTimer;
        System.Timers.Timer checkTimer;

        /// <summary>
        /// 初始化定时器
        /// </summary>
        public void initTimer()
        {
            pingTimer = new System.Timers.Timer(5000);
            pingTimer.Elapsed += new System.Timers.ElapsedEventHandler(pingExecute);
            pingTimer.AutoReset = true;
            pingTimer.Enabled = true;

            checkTimer = new System.Timers.Timer(3000);
            checkTimer.Elapsed += new System.Timers.ElapsedEventHandler(checkConnect);
            checkTimer.AutoReset = true;
            checkTimer.Enabled = true;

        }

        /// <summary>
        /// 检查断网
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void checkConnect(object source, System.Timers.ElapsedEventArgs e)
        {
            if (isStarted)
            {
                checkTimer.Stop(); //先关闭定时器
                if (!ws.IsAlive)
                {
                    Console.WriteLine("Reconnecting......");
                    ws.Connect();
                }
                checkTimer.Start(); //执行完毕后再开启器
            }
            
        }

        /// <summary>
        /// ping
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void pingExecute(object source, System.Timers.ElapsedEventArgs e)
        {
            if (isStarted)
            {
                pingTimer.Stop(); //先关闭定时器
                if (isLogin)
                {
                    ws.Send(ping());
                }
                pingTimer.Start(); //执行完毕后再开启器
            }
        }

        /// <summary>
        ///  初始化socket
        /// </summary>
        public void initSocket()
        {
            using (ws = new WebSocket("ws://"+ ipAndPort))
            {

                ws.OnMessage += (sender, e) =>
                {
                    String message = e.Data + "";
                    if (message.IndexOf("{") == -1)
                    {
                        message = AesUtils.AesDecrypt(message, secKey);
                    }
                    else
                    {
                        JObject json = (JObject)JsonConvert.DeserializeObject(message);
                        String resDesc = json["resDesc"].ToString();
                        if(resDesc.Equals("登录成功"))
                        {
                            isLogin = true;
                        }
                    }
                    sendAck(message);
                    oBPublishSub.Raise(message);

                };
                ws.OnOpen += (sender, e) => {
                    ws.Send(login());
                };
                ws.OnError += (sender, e) =>
                {
                    isLogin = false;
                    oBPublishSub.Raise(e.Message);
                };
                ws.OnClose += (sender, e) =>
                {
                    isLogin = false;
                    oBPublishSub.Raise(e.Reason);
                };

            }

            pingTimer.Start();
            isStarted = true;
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        String login()
        {
            MessageBody messageBody = new MessageBody();
            messageBody.eventId = "1000000";
            messageBody.fromUid = fromUid;
            messageBody.token = token;
            messageBody.deviceId = deviceId;
            messageBody.fbFlag = fbFlag;
            messageBody.cTimest = TimeUtils.getTimetamp();

            String value = JsonConvert.SerializeObject(messageBody);
            return value;
        }

        /// <summary>
        /// ping消息
        /// </summary>
        /// <returns></returns>
        String ping()
        {
            MessageBody messageBody = new MessageBody();
            messageBody.eventId = "9000000";
            messageBody.fromUid = fromUid;
            messageBody.deviceId = deviceId;
            messageBody.fbFlag = fbFlag;
            messageBody.cTimest = TimeUtils.getTimetamp();

            String value = JsonConvert.SerializeObject(messageBody);
            value = AesUtils.AesEncrypt(value, secKey);
            return value;
        }

        /// <summary>
        /// 指定的事件 自动发送ACK
        /// </summary>
        /// <param name="message"></param>
        void sendAck(String message)
        {
            JObject json = (JObject)JsonConvert.DeserializeObject(message);
            String eventId = json["eventId"].ToString();
            if(needAck.IndexOf(eventId) >=0)
            {
                MessageBody messageBody = new MessageBody();
                messageBody.eventId = "1000002";
                messageBody.fromUid = fromUid;
                messageBody.deviceId = deviceId;
                messageBody.fbFlag = fbFlag;
                messageBody.isAck = "1";
                messageBody.mType = "1";
                messageBody.cTimest = TimeUtils.getTimetamp();
                messageBody.dataBody = json["sTimest"].ToString();

                String value = JsonConvert.SerializeObject(messageBody);
                value = AesUtils.AesEncrypt(value, secKey);
                ws.Send(value);
            }

        }

        /// <summary>
        /// 停止socket
        /// </summary>
        void stop()
        {
            checkTimer.Stop();
            pingTimer.Stop();
            pingTimer = null;
            checkTimer = null;
            ws.Close();
            ws = null;
            isStarted = false;
            isLogin = false;
        }
    }
}

