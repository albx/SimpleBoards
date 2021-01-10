using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Api.Models.Issues
{
    public class AssignIssueModel
    {
        [Required]
        public string AssigneeId { get; set; }
    }
}