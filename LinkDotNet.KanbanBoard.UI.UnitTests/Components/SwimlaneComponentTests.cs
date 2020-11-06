using System;
using System.Linq;
using BlazorState;
using Bunit;
using Bunit.TestDoubles;

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
            Services.AddMockJSRuntime();
        }

        [TestMethod]
        public void GivenAbilityToAddGoals_WhenAddingSwimlane_ThenButtonVisible()
        {
            var goalContainer = CreateGoalContainer();
            var parameterBuilder = CreateSwimlaneComponent(goalContainer, GoalStatus.Todo, true);

            var swimlane = RenderComponent<Swimlane>(parameterBuilder.ToArray());
            var addGoalButton = swimlane.Find(".button-add-goal");

            addGoalButton.InnerHtml.ShouldNotBeNullOrEmpty();
        }

        [TestMethod]
        public void GivenNotAbilityToAddGoals_WhenAddingSwimlane_ThenButtonIsInvisible()
        {
            var goalContainer = CreateGoalContainer();
            var parameterBuilder = CreateSwimlaneComponent(goalContainer, GoalStatus.Todo, false);

            var swimlane = RenderComponent<Swimlane>(parameterBuilder.ToArray());
            var addGoalButtonCount = swimlane.FindAll(".button-add-goal").Count;

            addGoalButtonCount.ShouldBe(0);
        }

        [TestMethod]
        [Ignore("Virtualize is currently not supported in bUnit. The component will never be rendered and therefore has no elements")]
        public void GivenDifferentGoalStatus_WhenAddingToSwimlane_ThenOnlyCorrectStatusShown()
        {
            var goalStatus = GoalStatus.InProgress;
            var goalInProgress = CreateGoal(goalStatus);
            var goalInTodo = CreateGoal(GoalStatus.Todo);
            var goalContainer = CreateGoalContainer(goalInProgress, goalInTodo);
            var parameterBuilder = CreateSwimlaneComponent(goalContainer, goalStatus);

            var swimlane = RenderComponent<Swimlane>(parameterBuilder.ToArray());
            var count = swimlane.FindComponents<TodoTile>().Count;

            count.ShouldBe(1);
        }
        private static ComponentParameterCollection CreateSwimlaneComponent(GoalContainer goalContainer, GoalStatus goalStatus, bool canAddGoals = false)
        {
            var parameterBuilder = new ComponentParameterCollectionBuilder<Swimlane>()
                .Add(s => s.CanAddGoals, canAddGoals)
                .Add(s => s.GoalStatus, goalStatus)
                .Add(s => s.GoalContainer, goalContainer)
                .Build();
            return parameterBuilder;
        }

        private GoalContainer CreateGoalContainer(params Goal[] goals)
        {
            var goalContainerComponent = RenderComponent<GoalContainer>(g => g.Add(s => s.Goals, goals ?? Array.Empty<Goal>()));
            return goalContainerComponent.Instance;
        }
        
        private static Goal CreateGoal(GoalStatus status)
        {
            return new GoalBuilder().WithGoalStatus(status).Build();
        }
    }
}