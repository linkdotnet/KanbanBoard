using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace LinkDotNet.KanbanBoard.Domain
{
    public class Label : ValueObject
    {
        private static readonly Regex HexColorRegex = new Regex("^#(?:[0-9a-fA-F]{3}){1,2}$", RegexOptions.Compiled);

        private Label()
        {
        }

        private Label(string labelName, string hexColor)
        {
            LabelName = labelName;
            HexColor = hexColor;
        }

        public string LabelName { get; private set; }

        public string HexColor { get; private set; }

        public static Result<Label> Create(string labelName, string hexColor)
        {
            if (string.IsNullOrEmpty(labelName))
            {
                return Result.Failure<Label>("No label name was added");
            }

            if (!HexColorRegex.IsMatch(hexColor))
            {
                return Result.Failure<Label>("The given color is not a valid hex color");
            }

            return new Label(labelName, hexColor);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LabelName;
            yield return HexColor;
        }
    }
}