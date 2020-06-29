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

            var a = _index.Find("#timer-length").GetAttribute("value");
            _index.Find("button.start-timer").Click();
        }

        public void PauseTimer()
        {
            _index.Render();
            _index.Find("button.pause-timer").Click();
        }

        public void TimerCantBeStarted()
        {
            _index.Render();
            Assert.Empty(_index.FindAll("button.start-timer"));
        }

        public void TimerCantBeResumed()
        {
            _index.Render();
            Assert.Empty(_index.FindAll("button.resume-timer"));
        }

        public void ResumeTimerResumesFrom(TimeSpan time)
        {
            _index.Find("button.resume-timer").Click();
            _index.Render();
            Assert.Equal(time, TimeLeft()); 
        }

        public void TimerCantBePaused()
        {
            _index.Render();
            Assert.Empty(_index.FindAll("button.pause-timer"));
        }

        public void TimerCanBeStarted() 
            => Assert.Equal(1, _index.FindAll("button.resume-timer").Count);

        public void TimerCannotBeChanged()
        {
            _index.Render();
            var isDisabled = _index.Find("#timer-length").HasAttribute("disabled");
            Assert.True(isDisabled, "Timer length not disabled");
        }
    }
}