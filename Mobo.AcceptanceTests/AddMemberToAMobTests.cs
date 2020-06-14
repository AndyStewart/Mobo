using System;
using Xunit;

namespace Mobo.AcceptanceTests
{
    public class AddingMembersToAMobTests : IDisposable
    {
        private readonly Mobo _mobo;

        public AddingMembersToAMobTests() => _mobo = new Mobo();

        [Fact]
        public void AMobCanHaveAMember()
        {
            _mobo.Create("Mob Name");
            _mobo.AddMembers("Andy");
            _mobo.ShouldHaveMembers("Andy");
        }

        [Fact]
        public void AMobCanHaveAMultipleMembers()
        {
            _mobo.Create("Mob Name");
            _mobo.AddMembers("Andy");
            _mobo.AddMembers("Rob");
            _mobo.ShouldHaveMembers("Andy", "Rob");
        }

        [Fact]
        public void CantAddMembersWithAnInvalidName()
        {
            _mobo.Create("Mob Name");
            _mobo.AddMembers(string.Empty);
            _mobo.NoMembersAreInTheMob();
            _mobo.InvalidMemberNameErrorShown();
        }

        public void Dispose() => _mobo?.Dispose();
    }
}