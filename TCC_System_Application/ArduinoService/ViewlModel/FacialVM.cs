
using System;

namespace TCC_System_Application.ArduinoService
{
    public class FacialVM
    {
        public Guid ProjectId { get; set; }
        public Guid ModuleId { get; set; }
        public string Type { get; set; }
        public bool? Active { get; set; }

        public byte[] Image { get; set; }

    }
}
