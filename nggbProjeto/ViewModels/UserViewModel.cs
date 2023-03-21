using System.ComponentModel.DataAnnotations;

namespace nggbProjeto.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "Nickname")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha confirmada")]
        [Compare("Password", ErrorMessage = "A senha e a senha de confirmação não são iguais.")]
        public string ConfirmPassword { get; set; }
    }

}
