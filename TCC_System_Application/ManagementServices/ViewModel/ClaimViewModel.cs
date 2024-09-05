using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TCC_System_Application.ManagementServices
{
    public class ClaimViewModel
    {
        public int ClaimID { get; private set; }

        [Display(Name = "Claim")]
        public string NomeClaim { get; private set; }
    }
}
