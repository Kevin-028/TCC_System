using System;
using System.Web.Http;
using TCC_System_Application.ArduinoService;
using TCC_System_Application.ArduinoService.Query;

namespace TCC_System_API.Controllers
{
    public class TccSystemController : ApiController
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;

        public TccSystemController(IProductCommandService command, IProductQueryService productQueryService)
         {
            _productCommandService = command;
            _productQueryService = productQueryService;
        }

        // GET api/values
        [HttpGet]
        public ProductViewModel ProjectModule(string id)
        {
            return _productQueryService.GetProductModel(Guid.Parse(id));
        }

    }
}
