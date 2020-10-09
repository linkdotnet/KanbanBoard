using System;
using System.Linq;
using BlazorState;
using Bunit;
using LinkDotNet.KanbanBoard.Domain;
using LinkDotNet.KanbanBoard.UI.Components;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shouldly;

namespace LinkDotNet.KanbanBoard.UI.UnitTests.Components
{
    [TestClass]
    public class SwimlaneComponentTests : ComponentTestBase
    {
        [TestInitialize]
        public override void Setup()
        {
            base.Setup();
            Services.AddBlazorState(a => a.Assemblies = new[]
            {
                typeof(Swimlane).Assembly
            });
        }

        [TestMethod]
        public void GivenAbilityToAddGoals_WhenAddingSwimlane_ThenButtonVisible()
        {
            var goalContainer = CreateGoalContainer();
            var parameterBuilder = new ComponentParameterBuilder<Swimlane>().Add(s => s.CanAddGoals, true)
                .Add(s => s.GoalStatus, GoalStatus.Completed)
                .Add(goalContainer)
                .Build();

            var swimlane = RenderComponent<Swimlane>(parameterBuilder.ToArray());
            var addGoalButton = swimlane.Find(".button-add-goal");

            addGoalButton.InnerHtml.ShouldNotBeNullOrEmpty();
        }

        [TestMethod]
        public void GivenNotAbilityToAddGoals_WhenAddingSwimlane_ThenButtonIsInvisible()
        {
            var goalContainer = CreateGoalContainer();
            var parameterBuilder = new ComponentParameterBuilder<Swimlane>().Add(s => s.CanAddGoals, false)
                .Add(s => s.GoalStatus, GoalStatus.Completed)
                .Add(goalContainer)
                .Build();

            var swimlane = RenderComponent<Swimlane>(parameterBuilder.ToArray());
            var addGoalButtonCount = swimlane.FindAll(".button-add-goal").Count;

            addGoalButtonCount.ShouldBe(0);
        }

        [TestMethod]
        public void GivenDifferentGoalStatus_WhenAddingToSwimlane_ThenOnlyCorrectStatusShown()
        {
            var goalStatus = GoalStatus.InProgress;
            var goalInProgress = CreateGoal(goalStatus);
            var goalInTodo = CreateGoal(GoalStatus.Todo);
            var goalContainer = CreateGoalContainer(goalInProgress, goalInTodo);
            var parameterBuilder = new ComponentParameterBuilder<Swimlane>().Add(s => s.CanAddGoals, false)
                .Add(s => s.GoalStatus, goalStatus)
                .Add(goalContainer)
                .Build();

            var swimlane = RenderComponent<Swimlane>(parameterBuilder.ToArray());
            var count = swimlane.FindComponents<TodoTile>().Count;

            count.ShouldBe(1);
        }

        private GoalContainer CreateGoalContainer(params Goal[] goals)
        {
            var goalContainerComponent = RenderComponent<GoalContainer>(g => g.Add(s => s.Goals, goals ?? Array.Empty<Goal>()));
            return goalContainerComponent.Instance;
        }

        private Goal CreateGoal(GoalStatus status)
        {
            return new Goal("Test", default, Array.Empty<Subtask>(), Rank.Important, status);
        }
    }
}