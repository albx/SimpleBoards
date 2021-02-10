using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Models.Boards
{
    public class BoardModel
    {
        [Required]
        public string Name { get; set; }
    }
}