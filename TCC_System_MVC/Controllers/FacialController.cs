using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using TCC_System_Application.ArduinoService;

namespace TCC_System_MVC.Controllers
{
    public class FacialController : BaseController
    {
        private readonly IProductCommandService _productCommandService;

        public FacialController(IProductCommandService productCommandService)
        {
            _productCommandService = productCommandService;
        }


        // GET: Facial
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> CompareFace(ModuleViewModel view)
        {
            if (!string.IsNullOrEmpty(view.ImageName))
                view.imageBytes = Convert.FromBase64String(view.ImageName);


            // Compara a imagem usando o serviço de reconhecimento facial
            try
            {
                var mod = await _productCommandService.CompareImages(view);
            
                var results = JsonNotification();
                string a = mod.isMatch ? $"Rosto reconhecido com confiança: {mod.confidence}" : "Rosto não reconhecido.";
                return new JsonResult
                {
                    Data = new { data = results.Data, teste = a },

                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}