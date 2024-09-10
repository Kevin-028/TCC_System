using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCC_System_Application.ManagementServices
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Claims")]
        public List<int> Claims { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ClaimViewModel> ClaimViewModel { get; set; }


        [Display(Name = "Senha")]
        public string Password { get; set; }
        [Display(Name = "Senha antiga")]
        public string OldPassword { get; set; }
        [Display(Name = "Nova senha")]
        public string NewPassword { get; set; }
    }
}
