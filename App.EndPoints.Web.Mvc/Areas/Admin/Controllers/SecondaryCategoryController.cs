using App.Domain.Core.BaseService.Contracts.IAppServices;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstantProperty.Role_Admin)]
    public class SecondaryCategoryController : Controller
    {
        private readonly ISecondaryCategoryAppService _secondaryCategoryAppService;
        private readonly IMainCategoryAppService _mainCategoryAppService;
        

        public SecondaryCategoryController(ISecondaryCategoryAppService secondaryCategoryAppService,
                IMainCategoryAppService mainCategoryAppService)
        {
            _secondaryCategoryAppService = secondaryCategoryAppService;
            _mainCategoryAppService = mainCategoryAppService;
            
        }

        public async Task<IActionResult> Index()
        {
            var results =await _secondaryCategoryAppService.GetAllAsync();
            return View(results);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var MainCategoryDropDown = _mainCategoryAppService.GetAll().Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }).ToList();
            /*ViewData["MainCategoryDropDown"] = MainCategoryDropDown;*/
            ViewBag.MainCategoryDropDown = MainCategoryDropDown;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(SecondaryCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _secondaryCategoryAppService.Add(model);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var MainCategoryDropDown = _mainCategoryAppService.GetAll().Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.MainCategoryDropDown = MainCategoryDropDown;
            var secondaryCategory =await _secondaryCategoryAppService.Get(id);
            
            return View(secondaryCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(SecondaryCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                var MainCategoryDropDown = _mainCategoryAppService.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }).ToList();
                ViewBag.MainCategoryDropDown = MainCategoryDropDown;
                return View(model);
            }
            await _secondaryCategoryAppService.Update(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
           
            var model = await _secondaryCategoryAppService.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSecondaryCategory(int id)
        {
            await _secondaryCategoryAppService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
