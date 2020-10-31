using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LinkDotNet.KanbanBoard.UI.Components
{
    public class AddGoalModel
    {
        [Required]
        [MinLength(1, ErrorMessage = "Please enter a valid title")]
        public string Title { get; set; }

        public DateTime? Deadline { get; set; }

        [Required]
        public string Rank { get; set; }

        public ICollection<SubtaskModel> Subtasks { get; set; } = new List<SubtaskModel>();
    }

    public class SubtaskModel
    {
        public string Title { get; set; } = string.Empty;
    }
}