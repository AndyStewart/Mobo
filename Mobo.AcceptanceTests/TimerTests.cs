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
    }
}