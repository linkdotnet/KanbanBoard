using Microsoft.VisualStudio.TestTools.UnitTesting;

using LinkDotNet.KanbanBoard.UI.Components;
using Bunit;
using AngleSharp.Dom;
using LinkDotNet.KanbanBoard.Domain;
using System;
using Shouldly;

namespace LinkDotNet.KanbanBoard.UI.UnitTests.Components
{
    [TestClass]
    public class AddGoalDialogTests : ComponentTestBase
    {
        [TestMethod]
        public void GivenParameters_WhenAddingGoal_ThenGoalShouldBeReturned()
        {
            // Assert
            Goal goalReturned = null;
            var addDialog = RenderComponent<AddGoalDialog>(ComponentParameterFactory.EventCallback(nameof(AddGoalDialog.GoalAddedCallback), (Goal goal) => { goalReturned = goal; }));
            GetTitle(addDialog).Change("Title");
            GetRank(addDialog).Change(Rank.Important.Key);

            // Act
            SubmitForm(addDialog);

            // Assert
            goalReturned.ShouldNotBeNull();
            goalReturned.Title.ShouldBe("Title");
            goalReturned.Rank.ShouldBe(Rank.Important);
            goalReturned.GoalStatus.ShouldBe(GoalStatus.Todo);
        }

        private IElement GetTitle(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("#add-goal-title-input");
        private IElement GetRank(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("#add-goal-rank");
        private IElement GetDeadline(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("#add-goal-deadline");
        private void SubmitForm(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("form").Submit();
    }
}
