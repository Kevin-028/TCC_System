using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Mvc;
using TCC_System_Domain.Core;

namespace TCC_System_API.Controllers
{
    public class ValuesController : ApiController
    {

        // GET api/values/teste
        public JsonResult Get(Guid id)
        {
            var results = new JsonResult();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            }; ;
        }

        // GET api/values/5
        public IEnumerable<string> GeTEste()
        {
            return new string[] { "value1", "value2" };
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
