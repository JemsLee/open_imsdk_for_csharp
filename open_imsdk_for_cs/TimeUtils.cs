using System;
namespace open_imsdk_for_cs
{
    public class TimeUtils
    {
        /// <summary>
        /// 本时区日期时间转时间戳
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns>long=Int64</returns>
        public static long DateTimeToTimestamp(DateTime datetime)
        {
            DateTime dd = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            DateTime timeUTC = DateTime.SpecifyKind(datetime, DateTimeKind.Utc);//本地时间转成UTC时间
            TimeSpan ts = (timeUTC - dd);
            return (Int64)ts.TotalMilliseconds;//精确到毫秒
        }
        /// <summary>
        /// 时间戳转本时区日期时间
        /// </summary>
        /// <param name="timeStamp"></param>
        /// <returns></returns>
        public static DateTime TimestampToDateTime(string timeStamp)
        {
            DateTime dd = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0, 0), DateTimeKind.Local);
            long longTimeStamp = long.Parse(timeStamp + "0000");
            TimeSpan ts = new TimeSpan(longTimeStamp);
            return dd.Add(ts);
        }

        public static String getTimetamp()
        {
            //2.方法二（同一）
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string timestamp = Convert.ToInt64(ts.TotalSeconds).ToString();
            return timestamp;
        }
    }
}

