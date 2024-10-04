using System;
using System.Threading.Tasks;
using System.Web.Http;
using TCC_System_Application.ArduinoService;
using TCC_System_Application.Mensageria;

namespace TCC_System_API.Controllers
{
    public class TccSystemController : ApiController
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;
        
        private readonly IMessageCommandService messageCommandService;

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


        [HttpPost]
        public async Task<bool> PostMessage(Guid IdProduto) 
        {
            return true;
        }

    }
}
