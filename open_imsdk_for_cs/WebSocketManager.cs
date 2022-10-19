using System;
using open_imsdk_for_cs.observer;

namespace open_imsdk_for_cs
{
    public sealed class WebSocketManager
    {

        public String ipAndPort = "";
        public String fromUid = "";
        public String needAck = "";
        public String token = "";
        public String deviceId = "";
        public String fbFlag = "";

        public OBPublishSub oBPublishSub;

        private static WebSocketManager instance = null;
        private static object obj = new object();
        private WebSocketManager() { }
        public static WebSocketManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (obj)
                    {
                        if (instance == null)
                        {
                            instance = new WebSocketManager();
                        }
                    }
                }
                return instance;
            }
        }

        public void start()
        {
            oBPublishSub = new OBPublishSub();
            Console.WriteLine("start socket......");
            String key = AesUtils.md5(fromUid);
            OpenWebSocket openWebSocket = new OpenWebSocket();
            openWebSocket.secKey = key;
            openWebSocket.oBPublishSub = oBPublishSub;
            openWebSocket.fromUid = fromUid;
            openWebSocket.token = token;
            openWebSocket.ipAndPort = ipAndPort;
            openWebSocket.deviceId = deviceId;
            openWebSocket.needAck = needAck;
            openWebSocket.initTimer();
            openWebSocket.initSocket();
            openWebSocket.ws.Connect();
        }
    }
}

