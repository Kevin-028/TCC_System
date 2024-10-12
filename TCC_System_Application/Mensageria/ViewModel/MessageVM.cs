using System;

namespace TCC_System_Application.Mensageria
{
    public class MessageVM
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Action { get; set; }
        public Guid ProjectID { get; set; }
        public bool Active { get; set; }


    }
}
