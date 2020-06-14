using System;
using System.Threading.Tasks;
using Xunit;

namespace Mobo.AcceptanceTests
{
    public class TimerTests
    {
        [Fact]
        public void StartingTheTimerSetsTheTurnToTheFirstPerson()
        {
            using var mobo = new Mobo();
            mobo.Create("Mob Name");
            mobo.AddMembers("Andy");
            mobo.StartTimer();
            mobo.MemberTurnIs("Andy");
        }
        
        [Fact]
        public async Task StartedTimeCountsDownToNextTurn()
        {
            using var mobo = new Mobo();
            mobo.Create("Mob Name");
            mobo.AddMembers("Andy");
            mobo.StartTimer();
            await Task.Delay(TimeSpan.FromSeconds(1));
            mobo.TimeTillNextTurnIs(new TimeSpan(0, 14, 59));
        }
        
        [Theory]
        [InlineData(3)]
        [InlineData(2)]
        [InlineData(1)]
        public async Task TimerCountsDownEachSecond(int delay)
        {
            using var mobo = new Mobo();
            mobo.Create("Mob Name");
            mobo.AddMembers("Andy");
            mobo.StartTimer();
            await Task.Delay(TimeSpan.FromSeconds(delay));
            mobo.TimeTillNextTurnIs(new TimeSpan(0, 14, 60 - delay));
        }

    }
}