using System;

namespace SimpleBoards.Core.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public virtual User User { get; set; }

        public DateTime CommentedAt { get; set; }
    }
}