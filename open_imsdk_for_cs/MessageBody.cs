using System;
namespace open_imsdk_for_cs
{
    public class MessageBody
    {
        public String eventId = "";//事件ID，参考事件ID文件
        public String fromUid = "";//发送者ID
        public String token = "";//发送者token
        public String channelId = "";//用户的channel

        public String toUid = "";//接收者ID，多个以逗号隔开  重点：对于客户端发送过来的消息，不能和groupId并存，两者只能同时出现一个
        public String mType = "";//消息类型
        public String cTimest = "";//客户端发送时间搓
        public String sTimest = "";//服务端接收时间搓
        public String dataBody = "";//消息体，可以自由定义，以字符串格式传入

        public String isGroup = "0";//是否群组 1-群组，0-个人
        public String groupId = "";//群组ID ，对于客户端发送过来的消息，不能和toUid并存，两者只能同时出现一个
        public String groupName = "";//群组名称

        public String pkGroupId = ""; //pk时使用
        public String spUid = "";//特殊用户ID
        public String isAck = "0";//客户端接收到服务端发送的消息后，返回的状态= 1；dataBody结构 sTimest,sTimest,sTimest,sTimest......

        public String isCache = "0";//是否需要存离线 1-需要，0-不需要
        public String deviceId = "";//唯一设备id，目前用AFID作为标识，登录时带入
        public String oldChannelId = "";//准备离线的channel
        public String isRoot = "0";//是否机器人 1-机器人

        public String fbFlag = "";//分包的标记
    }
}

