using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;
using Index = Mobo.Blazor.Pages.Index;

namespace Mobo.AcceptanceTests
{
    class Mobo : IDisposable
    {
        private readonly IRenderedComponent<Index> _index;
        private readonly TestContext _ctx;

        public Mobo()
        {
            _ctx = new TestContext();
            _index = _ctx.RenderComponent<Index>();
            Clock.Reset();
            Clock.Pause();
        }

        public void Dispose() => _ctx?.Dispose();

        public void TimeLeftOnTimerIs(TimeSpan timeLeft)
        {
            _index.Render();
            Assert.Equal(timeLeft, TimeLeft());
        }

        public async Task CountDownIsPaused()
        {
            var timeLeftBefore = TimeLeft();
            await Clock.MoveForward(TimeSpan.FromSeconds(5));
            Assert.Equal(timeLeftBefore, TimeLeft());
        }

        public TimeSpan TimeLeft()
        {
            _index.Render();
            var timeLeft = _index.Find(".timer").Text();
            var timeParts = timeLeft.Split(':').Select(int.Parse).ToArray();
            return new TimeSpan(0, timeParts[0], timeParts[1]);
        }

        internal void CountDownCantBeReset()
        {
            _index.Render();
            Assert.Equal(0, _index.FindAll("#reset-timer").Count);
        }

        public async Task CountDownIsRunning()
        {
            var timeLeftBefore = TimeLeft();
            await Clock.MoveForward(TimeSpan.FromSeconds(3));
            _index.Render();
            var timeLeft = TimeLeft();
            Assert.Equal(timeLeftBefore.Subtract(TimeSpan.FromSeconds(3)),timeLeft);
        }

        public void StartTheTimer(TimeSpan timerLength)
        {
            _index.Render();
            _index.Find("#timer-length")
                  .Change(new ChangeEventArgs{Value = timerLength.Minutes.ToString()});
            _index.Render();
            _index.Find("#start-timer").Click();
        }

        public void PauseTimer()
        {
            _index.Render();
            _index.Find("#pause-timer").Click();
        }

        public void TimerCantBeStarted()
        {
            _index.Render();
            Assert.Empty(_index.FindAll("#start-timer"));
        }

        public void TimerCantBeResumed()
        {
            _index.Render();
            Assert.Empty(_index.FindAll("#resume-timer"));
        }

        public void ResumeTimerResumesFrom(TimeSpan time)
        {
            _index.Find("#resume-timer").Click();
            _index.Render();
            Assert.Equal(time, TimeLeft()); 
        }

        public void TimerCantBePaused()
        {
            _index.Render();
            Assert.Empty(_index.FindAll("#pause-timer"));
        }

        public void TimerCanBeStarted() 
            => Assert.Equal(1, _index.FindAll("#resume-timer").Count);

        internal void ResetTimer()
        {
            _index.Find("#reset-timer").Click();
            _index.Render();
        }

        public void TimerCannotBeChanged()
        {
            _index.Render();
            var isDisabled = _index.Find("#timer-length").HasAttribute("disabled");
            Assert.True(isDisabled, "Timer length not disabled");
        }
    }
}