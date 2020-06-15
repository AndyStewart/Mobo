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

        public void Dispose() => _ctx?.Dispose();
    }
}