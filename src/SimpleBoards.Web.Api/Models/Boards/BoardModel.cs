using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Api.Models.Boards
{
    public class BoardModel
    {
        [Required]
        public string Name { get; set; }
    }
}