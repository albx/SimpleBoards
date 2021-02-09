using System.ComponentModel.DataAnnotations;
using SimpleBoards.Core.Models;

namespace SimpleBoards.Web.Models.Issues
{
    public class NewIssueModel
    {
        [Required]
        public int BoardId { get; set; }

        [Required]
        public string ReporterId { get; set; }

        [Required]
        public Issue.IssueType Type { get; set; }

        [Required]
        public string Title { get; set; }
        
        public string Description { get; set; }
    }
}