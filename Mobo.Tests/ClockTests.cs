using System;
using System.Threading.Tasks;
using Xunit;

namespace Mobo.Tests
{
    public class ClockTests
    {
        public ClockTests()
        {
            Clock.Reset();
        }
        
        [Fact]
        public void GetCurrentTime()
        {
            Assert.Equal(DateTime.Now.TimeOfDay.Hours, Clock.CurrentTime.Hour);
            Assert.Equal(DateTime.Now.TimeOfDay.Minutes, Clock.CurrentTime.Minute);
            Assert.Equal(DateTime.Now.TimeOfDay.Seconds, Clock.CurrentTime.Second);
        }
        
        [Fact]
        public async Task PauseTime()
        {
            Clock.Pause();
            var time = Clock.CurrentTime;
            await Task.Delay(TimeSpan.FromSeconds(3));
            Assert.Equal(time, Clock.CurrentTime);
        }
        
        [Fact]
        public async Task MoveTimeForwards()
        {
            Clock.Pause();
            var time = Clock.CurrentTime;
            await Clock.MoveForward(TimeSpan.FromSeconds(3));
            Assert.Equal(time.AddSeconds(3), Clock.CurrentTime);
        }

    }
}