using System;
using System.Collections.Generic;
using System.Text;

namespace TCC_System_Application.ArduinoService
{
    public class PostVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string RecordCreatedBy { get; set; }
        public DateTime RecordCreationDate {  get; set; }
        public int UserId { get; set; }

    }
}
