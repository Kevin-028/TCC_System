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

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
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

    }
}
