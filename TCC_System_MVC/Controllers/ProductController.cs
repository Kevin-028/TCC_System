using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TCC_System_Application.ArduinoService;

namespace TCC_System_MVC.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;

        public ProductController(IProductCommandService command, IProductQueryService productQueryService)
        {
            _productCommandService = command;
            _productQueryService = productQueryService;
        }

        // GET: Product
        public async  Task<ActionResult> Index()
        {

            List<ProductViewModel> products = await _productCommandService.GetProductByLogin(UserLogin());

            return View(products);
        }
        [HttpPost]
        public async Task<JsonResult> Product(ProductViewModel view)
        {

            await _productCommandService.Insert(view, UserLogin());

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpGet]
        public async Task<PartialViewResult> GetProductView()
        {
            return PartialView("_Product");
        }


        [HttpGet]
        public async Task<PartialViewResult> GetModuleVM(ModuleViewModel view) 
        {

            if (view.Type == "RFID")
            {
                if (view.ModuleId == null)
                {
                    return PartialView("_ModuleRF", view);
                }
                else
                {
                    var restult = await _productQueryService.GetModelById(view.ModuleId);


                    return PartialView("_ModuleRF", restult);
                }


            }
            else if (view.Type == "FingerprintReader")
            {
                return PartialView("", view);

            }
            else if (view.Type == "FacialReader")
            {
                return PartialView("", view);

            }
            return null;
        }

        [HttpPost]
        public async Task<JsonResult> PostModule(ModuleViewModel view)
        {

            await _productCommandService.InsertModule(view, UserLogin());

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}