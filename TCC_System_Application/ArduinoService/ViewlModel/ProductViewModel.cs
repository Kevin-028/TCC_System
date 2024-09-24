﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TCC_System_Application.ArduinoService
{
    public class ProductViewModel
    {

        [Display(Name = "Codigo do Produto")]
        public Guid Id { get; set; }

        [Display(Name = "Nome")]
        public string Name { get; set; }
        public int UserId { get; set; }


        public List<ModuleViewModel> Modules { get; set; }
    }
}