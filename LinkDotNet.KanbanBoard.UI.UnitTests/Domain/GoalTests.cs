using LinkDotNet.KanbanBoard.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shouldly;

using System;

namespace LinkDotNet.KanbanBoard.UI.UnitTests.Domain
{
    [TestClass]
    public class GoalTests
    {
        [TestMethod]
        public void GivenValidInput_WhenCreatingGoal_ThenSuccess()
        {
            var result = Goal.Create("Title", Rank.ImportantAndUrgent, GoalStatus.Todo, null, null);

            result.IsSuccess.ShouldBeTrue();
            var goal = result.Value;
            goal.Title.ShouldBe("Title");
            goal.Rank.ShouldBe(Rank.ImportantAndUrgent);
            goal.GoalStatus.ShouldBe(GoalStatus.Todo);
            goal.HasDeadline.ShouldBeFalse();
        }

        [TestMethod]
        public void GivenGoal_WhenCreatingWithEmptyTitle_ThenFailure()
        {
            var result = Goal.Create(string.Empty, Rank.Important, GoalStatus.Completed, null, null);

            result.IsFailure.ShouldBeTrue();
        }

        [TestMethod]
        public void GivenGoal_WhenCreatingWithNullRank_ThenFailure()
        {
            var result = Goal.Create("Title", null, GoalStatus.Completed, null, null);

            result.IsFailure.ShouldBeTrue();
        }

        [TestMethod]
        public void GivenGoal_WhenCreatingWithNullGoalStatus_ThenFailure()
        {
            var result = Goal.Create("Title", Rank.Important, null, null, null);

            result.IsFailure.ShouldBeTrue();
        }

        [TestMethod]
        public void GivenDeadlineInPast_WhenGettingIsOverdue_ThenTrze()
        {
            var goal = new GoalBuilder().WithDeadline(DateTime.Now.AddDays(-1)).Build();

            goal.IsOverdue.ShouldBeTrue();
        }

        [TestMethod]
        public void GivenDeadlineInFuture_WhenGettingIsOverdue_ThenTrze()
        {
            var goal = new GoalBuilder().WithDeadline(DateTime.Now.AddDays(1)).Build();

            goal.IsOverdue.ShouldBeFalse();
        }
    }
}
