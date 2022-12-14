using App.Domain.Core.BaseService.Contracts.IAppServices;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstantProperty.Role_Admin)]
    public class MainCategoryController : Controller
    {
        private readonly IMainCategoryAppService _mainCategoryAppService;
        private readonly ILogger<IMainCategoryAppService> _logger;

        public MainCategoryController(IMainCategoryAppService mainCategoryAppService, ILogger<IMainCategoryAppService> logger)
        {
            _mainCategoryAppService = mainCategoryAppService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var results =await _mainCategoryAppService.GetAllAsync();
            return View(results);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(MainCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("MainCategory is Not Valid");
                return View(model);
            }
            await _mainCategoryAppService.Add(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var MainCategory =await _mainCategoryAppService.Get(id);
            
            return View(MainCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MainCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("MainCategory is Not Valid");
                return View(model);
            }
            await _mainCategoryAppService.Update(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _mainCategoryAppService.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMainCategory(int id)
        {
            if (id == null)
            {
                _logger.LogWarning("MainCategory Id is Null ");
                return RedirectToAction("Index");
            }
            var model = await _mainCategoryAppService.Get(id);
            if (model == null)
            {
                _logger.LogWarning("MainCategory Model is Null ");
                return RedirectToAction("Index");
            }
            await _mainCategoryAppService.Delete(model.Id);
            return RedirectToAction("Index");
        }

    }
}
