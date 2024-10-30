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
        public async Task<ActionResult> CompareFace(FacialVM view)
        {

            // Compara a imagem usando o serviço de reconhecimento facial
            var (isMatch, confidence) = _productCommandService.CompareImages(view.ProjectId,view.Image);

            return Content(isMatch ? $"Rosto reconhecido com confiança: {confidence}" : "Rosto não reconhecido.");
        }
    }
}