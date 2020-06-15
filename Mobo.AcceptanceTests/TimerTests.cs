using System;
using System.Threading.Tasks;
using Xunit;

namespace Mobo.AcceptanceTests
{
    public class TimerTests
    {
        [Fact]
        public async Task DefaultTimerStartsAt15mins()
        {
            using var mobo = new Mobo();
            mobo.TimeLeftOnTimerIs(TimeSpan.FromMinutes(15));
            await mobo.CountDownIsPaused();
        }

        [Fact]
        public async Task StartingTimerStartsTheCountdown()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer();
            await mobo.CountDownIsRunning();
        }
        
        [Fact]
        public async Task PausingTheTimerPausesTheCountdown()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer();
            await mobo.CountDownIsRunning();
            mobo.PauseTimer();
            await mobo.CountDownIsPaused();
        }

    }
}