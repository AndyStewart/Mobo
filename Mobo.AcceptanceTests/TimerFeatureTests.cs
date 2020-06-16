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
            mobo.TimerCantBeStarted();
            mobo.TimerCantBeResumed();
        }
        
        [Fact]
        public async Task CountdownStopsAt0()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer();
            await Clock.MoveForward(TimeSpan.FromMinutes(15));
            await mobo.CountDownIsPaused();
        }
        
        [Fact]
        public async Task PausingTheTimerPausesTheCountdown()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer();
            await Clock.MoveForward(TimeSpan.FromSeconds(5));
            mobo.PauseTimer();
            await mobo.CountDownIsPaused();
            mobo.TimerCantBeStarted();
            mobo.TimerCantBePaused();
            mobo.TimerCanBeStarted();
        }
        
        [Fact]
        public async Task ResumeAPausedTimer()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer();
            await Clock.MoveForward(TimeSpan.FromSeconds(5));
            var time = mobo.TimeLeft();
            mobo.PauseTimer();
            await Clock.MoveForward(TimeSpan.FromSeconds(5));
            mobo.ResumeTimerResumesFrom(time);
            await mobo.CountDownIsRunning();
        }

    }
}