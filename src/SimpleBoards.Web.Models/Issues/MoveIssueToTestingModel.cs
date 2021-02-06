using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Models.Issues
{
    public class MoveIssueToTestingModel
    {
        [Required]
        public string TesterId { get; set; }
    }
}