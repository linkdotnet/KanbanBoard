using System;
using System.ComponentModel.DataAnnotations;

namespace LinkDotNet.KanbanBoard.UI.Components
{
    public class AddGoalModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Please enter a valid title")]
        public string Title { get; set; }

        public DateTime Deadline { get; set; }

        [Required]
        public string Rank { get; set; }
    }
}