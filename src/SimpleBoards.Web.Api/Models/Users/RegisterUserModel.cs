using System.ComponentModel.DataAnnotations;

namespace SimpleBoards.Web.Api.Models.Users
{
    public class RegisterUserModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}