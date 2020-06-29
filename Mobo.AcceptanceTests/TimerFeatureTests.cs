using System;
using System.Threading.Tasks;
using Xunit;

namespace Mobo.AcceptanceTests
{
    public class TimerTests
    {
        private readonly TimeSpan _defaultTimeLength = TimeSpan.FromMinutes(15);

        [Fact]
        public async Task DefaultTimerStartsAt15mins()
        {
            using var mobo = new Mobo();
            mobo.TimeLeftOnTimerIs(_defaultTimeLength); 
            await mobo.CountDownIsPaused();
        }

        [Fact]
        public async Task StartingTimerStartsTheCountdown()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer(CountDownTimer.Default);
            await mobo.CountDownIsRunning();
            mobo.TimerCantBeStarted();
            mobo.TimerCantBeResumed();
        }
        
        [Fact]
        public async Task CountdownStopsAt0()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer(CountDownTimer.Default);
            await Clock.MoveForward(_defaultTimeLength);
            await mobo.CountDownIsPaused();
            mobo.CountDownCantBeReset();
        }
        
        [Fact]
        public async Task PausingTheTimerPausesTheCountdown()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer(CountDownTimer.Default);
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
            mobo.StartTheTimer(CountDownTimer.Default);
            await Clock.MoveForward(TimeSpan.FromSeconds(5));
            var time = mobo.TimeLeft();
            mobo.PauseTimer();
            await Clock.MoveForward(TimeSpan.FromSeconds(5));
            mobo.ResumeTimerResumesFrom(time);
            await mobo.CountDownIsRunning();
        }

        [Fact]
        public async Task TimerCanBeSetToACustomLength()
        {
            using var mobo = new Mobo();
            var customTimerLength = TimeSpan.FromMinutes(5);
            mobo.StartTheTimer(customTimerLength);
            mobo.TimeLeftOnTimerIs(customTimerLength);
            await mobo.CountDownIsRunning();
        }
        
        [Fact]
        public void TimerCanNotBeSetToACustomLengthWhileTimerIsRunning()
        {
            using var mobo = new Mobo();
            mobo.StartTheTimer(TimeSpan.FromMinutes(5));
            mobo.TimerCannotBeChanged();
        }

                
        [Fact]
        public async Task ResetingTimerSetsTimeBackToOriginalTime()
        {
            using var mobo = new Mobo();
            var customTimerLength = TimeSpan.FromMinutes(5);
            mobo.StartTheTimer(customTimerLength);
            await Clock.MoveForward(TimeSpan.FromMinutes(3));
            mobo.ResetTimer();
            mobo.TimeLeftOnTimerIs(customTimerLength);
        }

    }
}