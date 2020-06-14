using System;
using Xunit;

namespace Mobo.AcceptanceTests
{
    public class CreatingAMobTests : IDisposable
    {
        private readonly Mobo _mobo;

        public CreatingAMobTests() => _mobo = new Mobo();

        [Fact]
        public void GiveTheMobAName()
        {
            _mobo.Create("Mob Name");
            _mobo.ShouldBeCreatedWithName("Mob Name");
        }

        [Fact]
        public void CantCreateAMobWithAnInvalidName()
        {
            _mobo.Create(string.Empty);
            _mobo.CreationErrorMessageIsShown();
        }

        public void Dispose() => _mobo?.Dispose();
    }
}