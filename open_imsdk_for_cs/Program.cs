// See https://aka.ms/new-console-template for more information
using open_imsdk_for_cs;

namespace open_imsdk_for_cs_ns
{
    class mainsdk
    {

        static void Main(string[] args)
        {
            WebSocketManager.Instance.ipAndPort = "xxx.xxx.xxx.xxx:xxx";
            WebSocketManager.Instance.fromUid = "xxxx";
            WebSocketManager.Instance.token = "xxxx";
            WebSocketManager.Instance.deviceId = TimeUtils.getTimetamp();
            WebSocketManager.Instance.needAck = "1000001,5000004,8000000";
            WebSocketManager.Instance.fbFlag = "xxxx";
            WebSocketManager.Instance.start();
            onMessage();
            Console.WriteLine("Press enter !");
            Console.ReadLine();

        }

        static void onMessage()
        {
            WebSocketManager.Instance.oBPublishSub.OnChange += (sender, e) =>
            {
                Console.WriteLine(e.Value);
            };

        }


    }

}
