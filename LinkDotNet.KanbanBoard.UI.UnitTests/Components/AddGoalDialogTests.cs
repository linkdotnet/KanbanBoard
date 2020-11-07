using AngleSharp.Dom;
using Bunit;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.UI.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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

        private static IElement GetTitle(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("#add-goal-title-input");
        private static IElement GetRank(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("#add-goal-rank");
        private static IElement GetDeadline(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("#add-goal-deadline");
        private static void SubmitForm(IRenderedComponent<AddGoalDialog> addGoalDialog) => addGoalDialog.Find("form").Submit();
    }
}
