using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Api.Models.Comments
{
    public class NewCommentModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string CommentText { get; set; }
    }
}