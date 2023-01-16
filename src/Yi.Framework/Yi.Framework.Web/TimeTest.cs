using System.Diagnostics;

namespace Yi.Framework.Web
{
    public class TimeTest
    {
        public static Stopwatch Stopwatch { get; set; }

        public static void Start()
        {
            Stopwatch=new Stopwatch();
            Stopwatch.Start();
        }
        public static void Result()
        {

            Stopwatch.Stop();
            string time = Stopwatch.ElapsedMilliseconds.ToString();
            Console.WriteLine($"本次运行启动时间为：{time}毫秒");
            Stopwatch.Restart();
        }
    }
}
