using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

        public Guid GetModelesType(string type)
        {
            var mod = Modules.Where(x => x.Type == type).FirstOrDefault();

            return mod.ModuleId;

        }
        public string GetActive(ProductViewModel view, string type)
        {
            var modulo = view.Modules.Where(x => x.Type == type).FirstOrDefault();
            if(modulo != null )
                if (modulo.Active.Value)
                    return "checked";
            
            return "";
        }

    }
}
