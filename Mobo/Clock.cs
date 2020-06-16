using System;
using System.Linq.Expressions;
using System.Resources;
using System.Threading.Tasks;

namespace Mobo
{
    public class Clock
    {
        static Func<DateTime> _getDate = () => DateTime.Now;
        public static DateTime CurrentTime => _getDate();

        public static void ReplaceTime(DateTime newTime) => _getDate = () => newTime;
        public static void Reset() => _getDate = () => DateTime.Now;

        public static void Pause() => ReplaceTime(CurrentTime);

        public static async Task MoveForward(TimeSpan time)
        {
            ReplaceTime(CurrentTime.Add(time));
            await Task.CompletedTask;
        }
    }
}