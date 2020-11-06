using LinkDotNet.KanbanBoard.Domain;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shouldly;

namespace LinkDotNet.KanbanBoard.UI.UnitTests.Domain
{
    [TestClass]
    public class LabelTests
    {
        [TestMethod]
        public void GivenLabel_WhenCreatingWithValidValues_ThenSuccess()
        {
            var result = Label.Create("Work", "#332211");

            result.IsSuccess.ShouldBeTrue();
            result.Value.LabelName.ShouldBe("Work");
            result.Value.HexColor.ShouldBe("#332211");
        }

        [TestMethod]
        public void GivenLabel_WhenCreatingWithEmptyTitle_ThenFailure()
        {
            var result = Label.Create(string.Empty, "#121212");

            result.IsFailure.ShouldBeTrue();
        }

        [TestMethod]
        [DataRow("121212")]
        [DataRow("green")]
        [DataRow("#1234567")]
        [DataRow("#123")]
        [DataRow("123")]
        public void GivenLabel_WhenCreatingWithInvalidHexColor_ThenFailure(string hexColor)
        {
            var result = Label.Create(string.Empty, hexColor);

            result.IsFailure.ShouldBeTrue();
        }
    }
}
