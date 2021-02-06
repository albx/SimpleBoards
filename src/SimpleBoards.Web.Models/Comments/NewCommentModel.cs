using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Models.Comments
{
    public class NewCommentModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public string CommentText { get; set; }
    }
}