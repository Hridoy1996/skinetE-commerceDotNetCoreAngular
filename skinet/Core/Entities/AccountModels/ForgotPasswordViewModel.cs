using System.ComponentModel.DataAnnotations;

namespace Core.Entities.AccountModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}


    

