using System;
using Xunit;

namespace Mobo.Tests
{
    public class CountDownTimerTests : IClock
    {
        private readonly CountDownTimer _countDownTimer;
        private readonly TimeSpan _defaultTimerLength = TimeSpan.FromMinutes(15);

        public CountDownTimerTests() 
            => _countDownTimer = new CountDownTimer(this, _defaultTimerLength);

        [Fact]
        public void CountDownTimerStartsAtSetTime()
        {
            var initialTimerLength = TimeSpan.FromMinutes(15);
            Assert.Equal(initialTimerLength, _countDownTimer.TimeLeft);
        }
        
        [Fact]
        public void CountDownTimerCalculatesTimeLeft()
        {
            var startedTimer = _countDownTimer.Start();
            Now = Now.AddMinutes(5);
            Assert.Equal(TimeSpan.FromMinutes(10), startedTimer.TimeLeft);
            Assert.IsType<InProgressTimer>(startedTimer);
        }

        [Fact]
        public void CountDownTimerStopsAt0()
        {
            var inProgressTimer = _countDownTimer.Start();
            Now = Now.AddMinutes(_defaultTimerLength.Minutes + 5);
            Assert.Equal(TimeSpan.Zero, inProgressTimer.TimeLeft);
        }
        
        [Fact]
        public void CountDownTimerCanBeStopped()
        {
            var startedTimer = _countDownTimer.Start();
            Now = Now.AddMinutes(5);
            var stoppedTimer = startedTimer.Stop();
            Now = Now.AddMinutes(5);
            var fromMinutes = TimeSpan.FromMinutes(10);
            Assert.Equal(fromMinutes, stoppedTimer.TimeLeft);
            Assert.IsType<PausedTimer>(stoppedTimer);
        }
        
        [Fact]
        public void CountDownTimerIsRunningWhenStarted()
        {
            var startTimer = _countDownTimer.Start();
            Assert.IsType<InProgressTimer>(startTimer);
        }

        [Fact]
        public void CountDownToStringIsHHMM()
        {
            var startedTimer = _countDownTimer.Start();
            Now = Now.AddSeconds(30);
            Assert.Equal($"14:30", startedTimer.ToString());
        }

        public DateTime Now { get; private set; } = new DateTime(2022, 03, 01, 13, 00, 00);
    }
}