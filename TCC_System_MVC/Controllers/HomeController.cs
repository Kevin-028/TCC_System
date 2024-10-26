using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using TCC_System_Application.ArduinoService;
using TCC_System_MVC.Core;

namespace TCC_System_MVC.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProductCommandService _productCommandService;
        private readonly IProductQueryService _productQueryService;

        public HomeController(IProductCommandService productCommandService, IProductQueryService productQueryService)
        {
            _productCommandService = productCommandService;
            _productQueryService = productQueryService;
        }

        [HttpGet]
        [ClaimsAuthorize(Claims = "PADRAO")]
        public async Task<ActionResult> Index()
        {
            var posts = await _productQueryService.GetAllPosts();

            // Remove todas as tags HTML dos corpos dos posts
            foreach (var post in posts)
            {
                post.Body = StripHtmlTags(post.Body);
            }

            return View(posts);
        }

        [HttpGet]
        [ClaimsAuthorize(Claims = "PADRAO")]
        public async Task<ActionResult> Post(Guid id)
        {
            return View(await _productQueryService.GetPostbyId(id));
        }
        [HttpGet]
        public async Task<ActionResult> NewPost()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> InsertPost(PostVM view)
        {
            await _productCommandService.NewPost(view, UserLogin());

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        [HttpPut]
        public async Task<JsonResult> PutPost(PostVM view)
        {
            await _productCommandService.UpdadtePost(view, UserLogin());

            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpDelete]
        public async Task<JsonResult> DeletePost(Guid id)
        {
            await _productCommandService.RemovePost(id);


            var results = JsonNotification();

            return new JsonResult
            {
                Data = new { data = results.Data },

                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        // Método para remover tags HTML
        private string StripHtmlTags(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            // Usa Regex para remover todas as tags HTML
            return Regex.Replace(input, "<.*?>", string.Empty);
        }

    }
}