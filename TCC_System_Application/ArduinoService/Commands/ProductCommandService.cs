using System;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Collections.Generic;
using Emgu.CV;
using Emgu.CV.Util; 
using Emgu.CV.Face;
using Emgu.CV.Structure;
using TCC_System_Application.ManagementServices.Query;
using TCC_System_Domain.Arduino;
using TCC_System_Domain.Arduino.Repositories;
using TCC_System_Domain.Blog;
using TCC_System_Domain.Core;
using Emgu.CV.CvEnum;

namespace TCC_System_Application.ArduinoService
{
    public interface IProductCommandService
    {
        Task Insert(ProductViewModel view, string user);
        Task Update(ProductViewModel view, string user);
        Task Delete(Guid id);
        Task<List<ProductViewModel>> GetProductByLogin(string loginId);

        Task InsertModule(ModuleViewModel view, string user);
        Task UpdateModule(ModuleViewModel view, string user);

        Task NewPost(PostVM view, string user);
        Task UpdadtePost(PostVM view, string user);
        Task RemovePost(Guid id);

        (bool isMatch, double confidence) CompareImages(Guid id, byte[] inputImageBytes);
    }

    public class ProductCommandService : ApplicationService, IProductCommandService
    {
        private readonly IProductRepository _productRepository;
        private readonly IUserQueryService _userQueryService;

        private readonly IPostRepository _postRepository;


        public ProductCommandService(IUnitOfWork unitOfWork
            , IProductRepository repository
            , IUserQueryService userQuery
            , IPostRepository postRepository)
        : base(unitOfWork)
        {
            _productRepository = repository;
            _userQueryService = userQuery;
            _postRepository = postRepository;
        }
        #region Projeto

        public async Task Insert(ProductViewModel view, string user)
        {
            view.UserId = _userQueryService.GetUsersByLogin(user).Id;

            Product obj = Adapter.ToProduct(view);

            obj.SetId();

            _productRepository.Insert(obj);

            Commit(user);
        }
        public async Task InsertModule(ModuleViewModel view, string user)
        {
            Product obj = _productRepository.GetProductModules(view.ProjectId);

            obj.AddModule(Adapter.ToModule(view));

            _productRepository.Update(obj);

            if (!Commit(user))
            {
                AssertionConcern.AssertNotification("Erro no cadastro no novo modulo");
            }
        }
        public async Task UpdateModule(ModuleViewModel view, string user)
        {
            Product obj = _productRepository.GetProductModules(view.ProjectId);


            if (view.Active.HasValue)
            {
                obj.SetModuleStatus(view.ModuleId, view.Active.Value);
            }

            _productRepository.Update(obj);

            if (!Commit(user))
            {
                AssertionConcern.AssertNotification("Erro em desativar o modulo");
            }

        }
        public async Task Update(ProductViewModel view, string user)
        {
            Product obj = _productRepository.FindByID(view.Id);

            if (!String.IsNullOrEmpty(view.Name))
            {
                obj.SetName(view.Name);
            }

            _productRepository.Update(obj);

            Commit(user);
        }
        public async Task Delete(Guid id)
        {
            Product obj = _productRepository.FindByID(id);

            _productRepository.Delete(obj);

            Commit();
        }
        public async Task<List<ProductViewModel>> GetProductByLogin(string loginId)
        {

            var obj = await _productRepository.GetProductByLogin(_userQueryService.GetUsersByLogin(loginId).Id);

            var list = await Task.WhenAll(obj.Select(p => Adapter.ToProductVM(p)));

            return list.ToList();
        }
        #endregion

        #region Post

        public async Task NewPost(PostVM view, string user)
        {

            view.UserId = _userQueryService.GetUsersByLogin(user).Id;

            Post obj = Adapter.ToPost(view);

            _postRepository.Insert(obj);

            Commit(user);
        }
        public async Task UpdadtePost(PostVM view, string user)
        {

            view.UserId = _userQueryService.GetUsersByLogin(user).Id;



            Post obj = _postRepository.FindByID(view.Id);

            obj.SetBody(view.Body);
            obj.SetTitle(view.Title);

            _postRepository.Update(obj);

            Commit(user);

        }
        public async Task RemovePost(Guid id)
        {
            _postRepository.Delete(id);

            Commit();
        }

        #endregion

        #region Facial
        public (bool isMatch, double confidence) CompareImages(Guid id, byte[] inputImageBytes)
        {
            LBPHFaceRecognizer recognizer = new LBPHFaceRecognizer();
            bool isRecognizerTrained = false;
            double confidenceThreshold = 80.0;

            Product obj = _productRepository.GetProductModules(id);

            Module module = obj.ProductModeles.Where(x => x.Type == TypeModule.FacialReader).FirstOrDefault();
            byte[] storedImageBytes = module.Image;

            try
            {
                // Convert byte arrays to Mat objects
                using (var inputImage = new Mat())
                using (var storedImage = new Mat())
                {
                    CvInvoke.Imdecode(inputImageBytes, ImreadModes.Grayscale, inputImage);
                    CvInvoke.Imdecode(storedImageBytes, ImreadModes.Grayscale, storedImage);

                    // Train the recognizer only once
                    if (!isRecognizerTrained)
                    {
                        // Convert images to VectorOfMat
                        var images = new VectorOfMat(storedImage);
                        var labels = new VectorOfInt(new[] { 0 });
                        recognizer.Train(images, labels);
                        isRecognizerTrained = true;
                    }

                    // Predict the input image
                    var result = recognizer.Predict(inputImage);

                    // Determine match based on confidence
                    bool isMatch = result.Distance <= confidenceThreshold;

                    return (isMatch, result.Distance);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log the error)
                Console.WriteLine($"Error: {ex.Message}");
                return (false, double.MaxValue);
            }
        }
        #endregion

    }
}
