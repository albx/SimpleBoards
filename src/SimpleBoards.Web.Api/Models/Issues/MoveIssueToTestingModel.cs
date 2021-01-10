using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Api.Models.Issues
{
    public class MoveIssueToTestingModel
    {
        [Required]
        public string TesterId { get; set; }
    }
}