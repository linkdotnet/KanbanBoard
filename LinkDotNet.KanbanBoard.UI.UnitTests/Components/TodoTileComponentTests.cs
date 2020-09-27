using System;
using Bunit;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.UI.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LinkDotNet.KanbanBoard.UI.UnitTests.Components
{
    [TestClass]
    public class TodoTileComponentTests
    {
        [TestMethod]
        public void GivenDeadlineThisYear_WhenRendering_ThenYearShouldBeOmitted()
        {
            var date = new DateTime(DateTime.Now.Year, 1, 1);
            using var context = new Bunit.TestContext();
            var goal = new Goal("Some-Title", date, Array.Empty<Subtask>(),  Rank.Important);

            var todoTile = context.RenderComponent<TodoTile>(("Goal", goal));
            var element = todoTile.Find("#deadline-text").InnerHtml;

            element.MarkupMatches("1. Jan.");
        }

        [TestMethod]
        public void GivenDeadlineNextYear_WhenRendering_ThenYearShouldNotBeOmitted()
        {
            var date = new DateTime(2100, 1, 1);
            using var context = new Bunit.TestContext();
            var goal = new Goal("Some-Title", date, Array.Empty<Subtask>(), Rank.Important);

            var todoTile = context.RenderComponent<TodoTile>(("Goal", goal));
            var element = todoTile.Find("#deadline-text").InnerHtml;

            element.MarkupMatches("1. Jan. 2100");
        }
    }
}
