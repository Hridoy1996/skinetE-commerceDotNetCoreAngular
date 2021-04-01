using System.ComponentModel.DataAnnotations;

namespace Core.Entities.AccountModels
{
    public class UseRecoveryCodeViewModel
    {
        [Required]
        public string Code { get; set; }

        public string ReturnUrl { get; set; }
    }
}


    

