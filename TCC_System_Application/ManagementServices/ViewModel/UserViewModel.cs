using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCC_System_Application.ManagementServices
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Grupo")]
        public string GroupName { get; set; }
        public string Login { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
        public string Email { get; set; }

        [Display(Name = "Claims")]
        public List<int> Claims { get; set; }
        public bool Ativo { get; set; }
        public IEnumerable<ClaimViewModel> ClaimViewModel { get; set; }
    }
}
