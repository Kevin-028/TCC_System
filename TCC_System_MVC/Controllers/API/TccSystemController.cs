using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Http;
using TCC_System_Application.ArduinoService;
using TCC_System_Application.Mensageria;
using TCC_System_Domain.Core;


namespace TCC_System_API.Controllers
{
    public class TccSystemController : ApiController
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;

        private readonly IMessageCommandService _messageCommandService;
        private readonly IMessageQueryService _messageQueryService;

        public IHandler<DomainNotification> Notifications;

        public TccSystemController(IProductCommandService productCommandService, IProductQueryService productQueryService, IMessageCommandService messageCommandService, IMessageQueryService messageQueryService)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
            _messageCommandService = messageCommandService;
            _messageQueryService = messageQueryService;
            Notifications = DomainEvent.Container.GetInstance<IHandler<DomainNotification>>();

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

        [HttpPost]
        public async Task<bool> PostNewModule(string id, string type,string value )
        {
            ModuleViewModel view = new ModuleViewModel() { ProjectId = Guid.Parse(id), Type = type,Value = value };

            await _productCommandService.InsertModule(view, "Sytem");

            return JsonNotification();
        }

        [HttpGet]
        public async Task<bool> NewModule(string id, string type,string value )
        {
            ModuleViewModel view = new ModuleViewModel() { ProjectId = Guid.Parse(id), Type = type,Value = value };

            await _productCommandService.InsertModule(view, "Sytem");

            return JsonNotification();
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

        /// <summary>
        ///     Nessa API voce ira utilizar para testar a comunicação do seu arduino, para usar vc deve primeiramente dar inicio na pagina de produto
        /// </summary>
        /// <param name="id">Id do Projeto</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageVM> TryMessage(Guid id)
        {
            return await _messageCommandService.GetMessagebyAPI(id);
        }

        [HttpPost]
        public async Task<bool> PostMessage(Guid id) 
        {
            return true;
        }


        public bool JsonNotification()
        {
            if (!Notifications.HasNotifications())
            {
                return true;
            }

            var errors = new List<string>();
            Notifications.GetValues().ForEach(x => errors.Add(x.Value));

            return false;
        }

    }
}
