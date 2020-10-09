using System;
using Bunit;
using Bunit.Rendering;
using LinkDotNet.KanbanBoard.UI.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestContext = Bunit.TestContext;

namespace LinkDotNet.KanbanBoard.UI.UnitTests
{
    public class ComponentTestBase : ITestContext, IDisposable
    {
        private TestContext _context;

        public ITestRenderer Renderer => _context?.Renderer ?? throw new InvalidOperationException("MSTest has not started executing tests yet");

        public TestServiceProvider Services => _context?.Services ?? throw new InvalidOperationException("MSTest has not started executing tests yet");

        public IRenderedComponent<TComponent> RenderComponent<TComponent>(params ComponentParameter[] parameters) where TComponent : IComponent
            => _context?.RenderComponent<TComponent>(parameters) ?? throw new InvalidOperationException("MSTest has not started executing tests yet");

        public IRenderedComponent<TComponent> RenderComponent<TComponent>(Action<ComponentParameterBuilder<TComponent>> parameterBuilder) where TComponent : IComponent
            => _context?.RenderComponent<TComponent>(parameterBuilder) ?? throw new InvalidOperationException("MSTest has not started executing tests yet");

        [TestInitialize]
        public virtual void Setup()
        {
            _context = new Bunit.TestContext();
        }

        [TestCleanup]
        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}