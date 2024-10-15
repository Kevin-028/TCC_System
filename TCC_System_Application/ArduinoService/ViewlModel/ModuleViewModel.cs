using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TCC_System_Application.ArduinoService
{
    public class ModuleViewModel
    {
        public Guid ProjectId { get; set; }
        public Guid ModuleId { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }

        [Display(Name = "Codigo do RFID")]
        public string value { get; set; }



    }
}
