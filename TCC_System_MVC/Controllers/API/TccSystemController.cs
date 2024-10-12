using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using TCC_System_Application.ArduinoService;
using TCC_System_Application.Mensageria;
using System.Linq;


namespace TCC_System_API.Controllers
{
    public class TccSystemController : ApiController
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;

        private readonly IMessageCommandService _messageCommandService;
        private readonly IMessageQueryService _messageQueryService;

        public TccSystemController(IProductCommandService productCommandService, IProductQueryService productQueryService, IMessageCommandService messageCommandService, IMessageQueryService messageQueryService)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _messageCommandService = messageCommandService;
            _messageQueryService = messageQueryService;
        }

        /// <summary>
        ///  Pegar todos os modulos ativos pelo projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ProductViewModel ProjectModule(string id)
        {
            return _productQueryService.GetProductModel(Guid.Parse(id));
        }

        /// <summary>
        ///  Pegar todas as notificações que estao ativas para esse projeto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageVM> MessagebyProject(Guid id)
        {
            return await _messageCommandService.GetMessagebyAPI(id);
        }

        /// <summary>
        ///     Pega as informações de um modulo especifico.
        /// </summary>
        /// <param name="id"> Id do Projeto</param>
        /// <param name="module"> RFID, FacialReader, FingerprintReader, System </param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MessageVM>> MessagebyProjectModule(Guid id, string module)
        {
            return await _messageQueryService.GetMessagebyProjectModule(id, module);
        }


        [HttpPost]
        public async Task<bool> PostMessage(Guid id) 
        {
            return true;
        }

    }
}
