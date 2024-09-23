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

        public ProductController(IProductCommandService command) 
        {
            _productCommandService = command;
        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> Produto(ProductViewModel view)
        {
            await _productCommandService.Insert(view, UserName().ToString());

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        



    }
}