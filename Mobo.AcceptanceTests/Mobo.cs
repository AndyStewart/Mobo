using System;
using System.Linq;
using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Xunit;
using Index = Mobo.Blazor.Pages.Index;

namespace Mobo.AcceptanceTests
{
    class Mobo : IDisposable
    {
        private IRenderedComponent<Index> _index;
        private TestContext _ctx;

        public void Create(string mobName)
        {
            _ctx = new TestContext();
            _index = _ctx.RenderComponent<Index>();
            _index.Find("#mobName").Change(new ChangeEventArgs()
            {
                Value = mobName
            });
            
            _index.Find("button").Click();
            _index.Render();
        }

        public void ShouldBeCreatedWithName(string mobName)
        {
            Assert.Equal(0, _index.FindAll(".error").Count);
            Assert.Equal($"Welcome to the mob: {mobName}", _index.Find("h1").Text());
        }

        public void CreationErrorMessageIsShown() 
            => Assert.NotNull(_index.Find(".error"));

        public void Dispose() => _ctx?.Dispose();

        public void AddMembers(string memberName)
        {
            _index.Find(".member-name")
                .Change(new ChangeEventArgs(){ Value=memberName});
            _index.Find(".add-member").Click();
            _index.Render();
        }

        public void ShouldHaveMembers(params string[] memberNames)
        {
            var members = _index
                .FindAll(".members li")
                .Select(q => q.Text())
                .ToArray();

            foreach (var memberName in memberNames)
            {
                Assert.Contains(memberName, members);
            }
        }


        public void NoMembersAreInTheMob()
        {
            var members = _index
                .FindAll(".members li")
                .Select(q => q.Text())
                .ToArray();
            Assert.Empty(members);
        }

        public void InvalidMemberNameErrorShown()
        {
            var error = _index.Find(".error").Text();
            Assert.Equal("Invalid member name", error);
        }

        public void StartTimer()
        {
            _index.Find(".start-timer").Click();
            _index.Render();
        }

        public void MemberTurnIs(string memberName)
        {
            var currentTurn = _index.Find(".current-turn").Text();
            Assert.Equal(memberName, currentTurn);
        }

        public void TimeTillNextTurnIs(TimeSpan timeSpan)
        {
            var currentTurn = _index.Find(".time-left").Text().Trim();
            Assert.Equal($"{timeSpan.Minutes}:{timeSpan.Seconds}", currentTurn);
        }
    }
}