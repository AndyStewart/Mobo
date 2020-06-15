using System;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
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
        }

        public void Dispose() => _ctx?.Dispose();

        public void TimeLeftOnTimerIs(TimeSpan timeLeft)
        {
            _index.Render();
            var timeLeftText = _index.Find(".timer").Text();
            Assert.Equal($"{timeLeft:mm}:{timeLeft:ss}", timeLeftText.Trim());
        }

        public async Task CountDownIsPaused()
        {
            _index.Render();
            var timeLeftBefore = TimeLeft();
            await Task.Delay(TimeSpan.FromSeconds(2));
            Assert.Equal(timeLeftBefore, TimeLeft());
        }

        private TimeSpan TimeLeft()
        {
            var timeLeft = _index.Find(".timer").Text();
            var timeParts = timeLeft.Split(':').Select(int.Parse).ToArray();
            return new TimeSpan(0, timeParts[0], timeParts[1]);
        }

        public async Task CountDownIsRunning()
        {
            _index.Render();
            var timeLeftBefore = TimeLeft();
            await Task.Delay(TimeSpan.FromSeconds(2));
            Assert.True(TimeLeft() < timeLeftBefore);
        }

        public void StartTheTimer()
        {
            _index.Render();
            _index.Find("button.start-timer").Click();
        }

        public void PauseTimer()
        {
            _index.Render();
            _index.Find("button.pause-timer").Click();
        }
    }
}