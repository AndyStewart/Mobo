using System;
using Xunit;

namespace Mobo.Tests
{
    public class MoboTests
    {
        [Fact]
        public void GiveTheMobAName()
        {
            var mob = new Mob("Mob 1");
            Assert.Equal("Mob 1", mob.Name);
        }

        [Fact]
        public void UnableToCreateMobWithoutName()
        {
            Assert.Throws<ArgumentException>(() => new Mob(""));
        }

        [Fact]
        public void MobCanHaveMultipleMembers()
        {
            var mob = new Mob("Mob 1");
            var memberName1 = "Andy";
            mob.AddMember(memberName1);
            var memberName2 = "Rob";
            mob.AddMember(memberName2);

            Assert.Equal(mob.Members[0], memberName1);
            Assert.Equal(mob.Members[1], memberName2);
        }
        
        [Fact]
        public void UnableToAddMemberWithoutName()
        {
            var mob = new Mob("Mob 1");
            Assert.Throws<ArgumentException>(() => mob.AddMember(""));
        }


        [Fact]
        public void StartingTheTimerSetsPlayer1ToCurrentTurn()
        {
            var mob = new Mob("Mob 1");
            var player1 = "Andy";
            mob.AddMember(player1);
            var startTime = DateTime.Now;
            mob.StartTimer(startTime);
            
            Assert.Equal(player1, mob.WhosTurnIsIt(startTime));
        }

        [Fact]
        public void After15MinutesTheTurnChangesToPlayer2()
        {
            var mob = new Mob("Mob 1");
            var player1 = "Andy";
            mob.AddMember(player1);
            var player2 = "Rob";
            mob.AddMember(player2);
            mob.StartTimer(DateTime.Now);
            
            Assert.Equal(player2, mob.WhosTurnIsIt(DateTime.Now.AddMinutes(16)));
        }
        
        [Fact]
        public void PlayReturnsToFirstMemberAfter10Minutes()
        {
            var mob = new Mob("Mob 1");
            var player1 = "Andy";
            mob.AddMember(player1);
            var player2 = "Rob";
            mob.AddMember(player2);
            mob.StartTimer(DateTime.Now);
            Assert.Equal(player1, mob.WhosTurnIsIt(DateTime.Now.AddMinutes(11)));
        }
        
        [Fact]
        public void PlayRotatesThroughPlayersEvery15Minutes()
        {
            var mob = new Mob("Mob 1");
            mob.AddMember("Andy");
            mob.AddMember("Rob");
            mob.AddMember("Kev");
            mob.StartTimer(DateTime.Now);
            Assert.Equal("Andy", mob.WhosTurnIsIt(DateTime.Now.AddMinutes(0)));
            Assert.Equal("Rob", mob.WhosTurnIsIt(DateTime.Now.AddMinutes(15)));
            Assert.Equal("Kev", mob.WhosTurnIsIt(DateTime.Now.AddMinutes(30)));
            Assert.Equal("Andy", mob.WhosTurnIsIt(DateTime.Now.AddMinutes(45)));
        }
    }
}