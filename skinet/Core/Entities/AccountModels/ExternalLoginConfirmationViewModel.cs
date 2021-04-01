using System.ComponentModel.DataAnnotations;

namespace Core.Entities.AccountModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}


    

