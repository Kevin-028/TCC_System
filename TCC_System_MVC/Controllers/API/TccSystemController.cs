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

        [HttpGet]
        // GET api/values
        public ProductViewModel Project(string id)
        {
            var a = _productQueryService.Geteste(Guid.Parse(id));          
            return a;
        }
        // GET api/values
        [HttpGet]
        public ProductViewModel ProjectModule(string id)
        {

            var a = _productQueryService.GetProductModel(Guid.Parse(id));
            
            return a;
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        // Estilo de vida assíncrono para suportar requisições Web API e MVC
        //container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();



    }
}
