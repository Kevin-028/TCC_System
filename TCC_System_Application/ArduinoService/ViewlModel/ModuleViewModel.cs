using System;
using System.ComponentModel.DataAnnotations;

namespace TCC_System_Application.ArduinoService
{
    public class ModuleViewModel
    {
        public Guid ProjectId { get; set; }
        public Guid ModuleId { get; set; }
        public string Type { get; set; }
        public bool? Active { get; set; }

        [Display(Name = "Valor para cadastro:")]
        public string Value { get; set; }

        public string ImageName { get; set; }
        public byte[] imageBytes { get; set; }


        public bool isMatch { get; set; }
        public double confidence { get; set; }

    }
}
