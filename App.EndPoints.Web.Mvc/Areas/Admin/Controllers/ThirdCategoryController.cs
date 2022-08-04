using App.Domain.Core.BaseService.Contracts.IAppServices;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.FileAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstantProperty.Role_Admin)]
    public class ThirdCategoryController : Controller
    {
        private readonly ISecondaryCategoryAppService _secondaryCategoryAppService;
        private readonly IThirdCategoryAppService _thirdCategoryAppService;
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ThirdCategoryController(ISecondaryCategoryAppService secondaryCategoryAppService,
                IThirdCategoryAppService thirdCategoryAppService, AppDbContext dbContext,
                IWebHostEnvironment webHostEnvironment)
        {
            _secondaryCategoryAppService = secondaryCategoryAppService;
            _thirdCategoryAppService = thirdCategoryAppService;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var results =await _thirdCategoryAppService.GetAll();
            return View(results);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var ThirdCategoryDropDown = _secondaryCategoryAppService.GetAll().Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.ThirdCategoryDropDown = ThirdCategoryDropDown;
            return View();
        }
        [HttpPost]
        /*public async Task<IActionResult> Create(ThirdCategoryDto model, IList<IFormFile>? uploadFiles)*/
        public async Task<IActionResult> Create(ThirdCategoryDto model, IList<IFormFile>? uploadFiles)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string webRootPath = _webHostEnvironment.WebRootPath;
            var loginUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _thirdCategoryAppService.Add(model, webRootPath, uploadFiles, loginUserId);
            /*var serviceId=await _thirdCategoryAppService.Add(model);*/
            await _dbContext.SaveChangesAsync();
            
            //Start Files
            /*foreach (var file in uploadFiles)
            {    
                string NewLocaton = webRootPath + ConstantProperty.ImageServicePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);
                using (var fileStream = new FileStream(Path.Combine(NewLocaton, fileName + extension), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                AppFile f = new AppFile()
                {
                    Name = fileName + extension
                };
                await _dbContext.AppFiles.AddAsync(f);
                await _dbContext.SaveChangesAsync();
                var fileId = f.Id;
                var loginUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                ThirdCategoryFile juctionFileAndThirdCategory = new ThirdCategoryFile()
                {
                    AppFileId = fileId,
                    ThirdCategoryId = serviceId,
                    CreatedUserId= loginUserId,
                    CreatedAt = DateTime.Now
                };
                await _dbContext.ThirdCategoryFiles.AddAsync(juctionFileAndThirdCategory);
                await _dbContext.SaveChangesAsync();

            }*/
            //End Files
            
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var ThirdCategoryDropDown = _secondaryCategoryAppService.GetAll().Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.ThirdCategoryDropDown = ThirdCategoryDropDown;
            var thirdCategory =await _thirdCategoryAppService.Get(id);
            
            return View(thirdCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ThirdCategoryDto model)
        {
            if (!ModelState.IsValid)
            {
                var ThirdCategoryDropDown = _secondaryCategoryAppService.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Title,
                    Value = i.Id.ToString()
                }).ToList();
                ViewBag.ThirdCategoryDropDown = ThirdCategoryDropDown;
                return View(model);
            }
            await _thirdCategoryAppService.Update(model);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _thirdCategoryAppService.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteThirdCategory(int id)
        {
            await _thirdCategoryAppService.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            /*var service = _dbContext.ThirdCategoryFiles
                .Include(x => x.AppFile).Where(x => x.ThirdCategoryId == id).ToList();*/
            var service =await _thirdCategoryAppService.Details(id);
            return View(service);
                
        }

    }
}
