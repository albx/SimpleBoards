using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Models.Issues
{
    public class AssignIssueModel
    {
        [Required]
        public string AssigneeId { get; set; }
    }
}