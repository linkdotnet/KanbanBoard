using LinkDotNet.EnumValueObject;

namespace LinkDotNet.KanbanBoard.Domain
{
    public class Rank : EnumValueObject<Rank>
    {
        public static readonly Rank Urgent = new Rank(nameof(Urgent), "U", "Urgent");

        public static readonly Rank Important = new Rank(nameof(Important), "I", "Important");

        public static readonly Rank ImportantAndUrgent = new Rank(nameof(ImportantAndUrgent), "I+U", "Important and urgent");

        public static readonly Rank None = new Rank(nameof(None), "X", "None");
        
        public string ShortRank { get; private set; }

        public string DisplayRank { get; private set; }

        private Rank(string key, string shortRank, string displayRank) : base(key)
        {
            ShortRank = shortRank;
            DisplayRank = displayRank;
        }
     }
}