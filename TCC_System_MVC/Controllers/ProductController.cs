using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using TCC_System_Application.ArduinoService;
using TCC_System_Application.Mensageria;

namespace TCC_System_MVC.Controllers
{
    public class ProductController : BaseController
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;

        private readonly IMessageCommandService _messageCommandService;
        private readonly IMessageQueryService _messageQueryService;

        public ProductController(IProductCommandService productCommandService, IProductQueryService productQueryService,
            IMessageCommandService messageCommandService, IMessageQueryService messageQueryService)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _messageCommandService = messageCommandService;
            _messageQueryService = messageQueryService;
        }


        // GET: Product
        public async Task<ActionResult> Index()
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
        public async Task<JsonResult> NewMessage(MessageVM view)
        {
            view.Action = "Add";
            await _messageCommandService.Insert(view, UserLogin());
            
            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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
        [HttpPost]
        public async Task<JsonResult> TryComunication(ProductViewModel view)
        {
            
            MessageVM messageVM = new MessageVM() {ProjectID = view.Id };

            messageVM.Action = "Add";

            messageVM.Type = "System";

            await _messageCommandService.Insert(messageVM, UserLogin());

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public async Task<JsonResult> GetComunication(MessageVM view)
        {



            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}