using System;
using Xunit;

namespace Mobo.Tests
{
    public class CountDownTimerTests : IClock
    {
        public CountDownTimerTests() 
            => Now = new DateTime(2022, 03, 01, 13, 00, 00);

        [Fact]
        public void CountDownTimerStartsAtSetTime()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            Assert.Equal(TimeSpan.FromMinutes(15), countDownTimer.TimeLeft);
        }
        
        [Fact]
        public void CountDownTimerCalculatesTimeLeft()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            countDownTimer.Start();
            Now = Now.AddMinutes(5);
            Assert.Equal(TimeSpan.FromMinutes(10), countDownTimer.TimeLeft);
        }

        [Fact]
        public void CountDownTimerStopsAt0()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            countDownTimer.Start();
            Now = Now.AddMinutes(20);
            Assert.Equal(TimeSpan.FromMinutes(0), countDownTimer.TimeLeft);
        }
        
        [Fact]
        public void CountDownTimerCanBeStopped()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            countDownTimer.Start();
            Now = Now.AddMinutes(5);
            countDownTimer.Stop();
            Now = Now.AddMinutes(5);
            Assert.Equal(TimeSpan.FromMinutes(10), countDownTimer.TimeLeft);
        }
        
        [Fact]
        public void CountDownTimerIsntRunningWhenInitialised()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            countDownTimer.Start();
            Assert.False(countDownTimer.Running);
        }

        [Fact]
        public void CountDownTimerIsRunningWhenStarted()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            countDownTimer.Start();
            Now = Now.AddMinutes(1);
            Assert.True(countDownTimer.Running);
        }
        
        [Fact]
        public void CountDownTimerIsntRunningWhenStopped()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            countDownTimer.Start();
            Now = Now.AddMinutes(1);
            countDownTimer.Stop();
            Assert.False(countDownTimer.Running);
        }
        
        [Fact]
        public void CountDownToStringIsHHMM()
        {
            var countDownTimer = new CountDownTimer(this, TimeSpan.FromMinutes(15));
            countDownTimer.Start();
            Now = Now.AddSeconds(30);
            Assert.Equal($"14:30", countDownTimer.ToString());
        }

        public DateTime Now { get; private set; }
    }
}