using App.Domain.Core.ApplicationUserAgg.Entities;
using App.Domain.Core.ExpertAgg.Dtos;
using App.Domain.Core.ExpertAgg.Entities;
using App.Domain.Core.FileAgg.Dtos;
using App.Domain.Core.FileAgg.Entities;
using App.Domain.Core.OrderAgg.Dtos;
using App.Domain.Core.SuggestionAgg.Dtos;
using App.Domain.Core.SuggestionAgg.Entities;
using App.EndPoints.Web.Mvc.Areas.Expert.Models.ViewModel;
using App.EndPoints.Web.Mvc.Models;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Expert.Controllers
{
    [Area("Expert")]
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(AppDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _webHostEnvironment = webHostEnvironment;   
        }
        [HttpPost]
        public IActionResult WorkStatus(int statusId, int orderId)
        {
            var order = _dbContext.Orders.Where(x => x.Id == orderId).SingleOrDefault();
            order.Status = (Domain.Core.OrderAgg.Enums.OrderStatusCode)statusId;
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
            return RedirectToAction("GetSuggestionByExpertId", "Home", new {expertId= User.FindFirstValue(ClaimTypes.NameIdentifier) });

        }

        public IActionResult GetAllOrder(string? name)
        {
            var orders = _dbContext.Orders
                .Include(x => x.ThirdCategory)
                .ThenInclude(y => y.ExpertSkills.Where(x =>x.ExpertId == User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .ToList();
            var s=orders.Where(x => x.ThirdCategory.ExpertSkills.Count > 0).Select(m => new OrderExpertDto()
            {
                Id = m.Id,
                BasePrice = m.BasePrice,
                CustomerName = _dbContext.Users.Where(n => n.Id == m.CustomerId)
                        .Select(p => p.UserName).FirstOrDefault(),
                Title = m.Title,
                ThirdCategoryName = m.ThirdCategory.Title,
            }).ToList();

            return View(s);
        }
        public IActionResult OrderDetails(int id)
        {
            var order = _dbContext.Orders.Where(x => x.Id == id).Include(x => x.ThirdCategory).Select(x => new OrderExpertDto()
            {
                Id = x.Id,
                BasePrice = x.BasePrice,
                CustomerName = _dbContext.Users
                      .Where(y => y.Id == x.CustomerId)
                      .Select(z => z.UserName).FirstOrDefault(),
                Title = x.Title,
                Description = x.Description,
                ThirdCategoryName = x.ThirdCategory.Title,
                FirstName= _dbContext.ApplicationUsers
                  .Where(y => y.Id == x.CustomerId)
                  .Select(z => z.FirstName).FirstOrDefault(),

                LastName = _dbContext.ApplicationUsers
                  .Where(y => y.Id == x.CustomerId)
                  .Select(z => z.Family).FirstOrDefault(),
                FullAddress = _dbContext.ApplicationUsers
                  .Where(y => y.Id == x.CustomerId)
                  .Select(z => z.FullAddress).FirstOrDefault(),

                Phone= _dbContext.ApplicationUsers
                  .Where(y => y.Id == x.CustomerId)
                  .Select(z => z.PhoneNumber).FirstOrDefault(),
            }).FirstOrDefault();


            return View(order);
        }

        [HttpGet]
        public IActionResult CreateOrderSuggestions(int orderId)
        {
            SuggestionDto model = new SuggestionDto()
            {
                OrderId = orderId,  
            };
            return View();
        }
        
        [HttpPost]
        public IActionResult CreateOrderSuggestions(SuggestionDto dto)
        {
            dto.ExpertId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            dto.SuggestionDate = DateTime.Now;
            var model = new Suggestion() { 
                ExpertId=dto.ExpertId,
                Duration=dto.Duration,
                OrderId=dto.OrderId,
                SuggestedPrice=dto.SuggestedPrice,
                StartTime=dto.StartTime,
                SuggestionDate=DateTime.Now
            };
            _dbContext.Suggestions.Add(model);
            _dbContext.SaveChanges();
            return RedirectToAction("GetAllOrder","Home");
        }

        public IActionResult GetSuggestionByExpertId(string expertId)
        {
            var model=_dbContext.Suggestions.Where(x => x.ExpertId == expertId).Include(x=>x.Order).Select(x => new SuggestionDto()
            {
                ExpertId = x.ExpertId,
                Duration = x.Duration,
                OrderId = x.OrderId,
                SuggestedPrice = x.SuggestedPrice,
                StartTime = x.StartTime,
                SuggestionDate = x.SuggestionDate,
                IsApproved=x.IsApproved,
                CustomerFirstName=_dbContext.ApplicationUsers.Where(y=>y.Id==x.Order.CustomerId).Select(x=>x.FirstName).FirstOrDefault(),
                CustomerLastName=_dbContext.ApplicationUsers.Where(y=>y.Id==x.Order.CustomerId).Select(x=>x.Family).FirstOrDefault(),
                ThirdCategoryName=_dbContext.ThirdCategories.Where(y=>y.Id==x.Order.ThirdCategoryId).Select(x=>x.Title).FirstOrDefault(),
            }).ToList();
            return View(model);
        }

        public async Task<IActionResult> ExpertProfile()
        {
            UserProfileViewModel userProfile = new UserProfileViewModel();
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            userProfile.Roles = roles;
            
            return View(userProfile);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(string userRole)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.AddToRoleAsync(user, userRole);
            return RedirectToAction("ExpertProfile");
        }

        [HttpGet]
        public IActionResult AddSkill()
        {
            var ThirdCategoryDropDown = _dbContext.ThirdCategories.Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.ThirdCategoryDropDown = ThirdCategoryDropDown;
            var dto = new ExpertSkillDto();
            dto.CategoryList = _dbContext.ExpertSkills
                .Include(x => x.ThirdCategory)
                .Where(y => y.ExpertId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Select(z => z.ThirdCategory.Title)
                .ToList();
            if (dto.CategoryList.Count() == 0)
            {
                dto.CategoryList.Add("مهارتی برای شما ثبت نشده است");
            }

            return View(dto);
        }

        [HttpPost]
        public IActionResult AddSkill(ExpertSkillDto dto)
        {
            var ThirdCategoryDropDown = _dbContext.ThirdCategories.Select(i => new SelectListItem
            {
                Text = i.Title,
                Value = i.Id.ToString()
            }).ToList();
            ViewBag.ThirdCategoryDropDown = ThirdCategoryDropDown;
            dto.ExpertId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var expertSkill = new ExpertSkill()
            {
                ExpertId = dto.ExpertId,
                ThirdCategoryId=dto.ThirdCategoryId,
                
            };
           
            _dbContext.ExpertSkills.Add(expertSkill);
            _dbContext.SaveChanges();
            return RedirectToAction("AddSkill","Home");
        }
        [HttpGet]
        public IActionResult Edit(string userId)
        {
            var expert = _dbContext.ApplicationUsers.Select(x=>new ExpertVM()
            {
                Id=x.Id,
                FirstName=x.FirstName,
                LastName=x.Family,
                PhoneNumber=x.PhoneNumber,
                Address=x.FullAddress,
            }).SingleOrDefault(x => x.Id == userId);
            return View(expert);
        }
        [HttpPost]
        public IActionResult Edit(ExpertVM expert)
        {
            var user = _dbContext.ApplicationUsers.SingleOrDefault(x => x.Id == expert.Id);
            user.FirstName = expert.FirstName;
            user.Family = expert.LastName;
            user.PhoneNumber = expert.PhoneNumber;
            user.FullAddress = expert.Address;
            _dbContext.ApplicationUsers.Update(user);
            _dbContext.SaveChanges();

            //Start Image Update
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0) 
            {
                var objFromDb = _dbContext.UserFiles.Include(x => x.AppFile).FirstOrDefault(x => x.UserId == expert.Id);
                string webRootPath = _webHostEnvironment.WebRootPath;
                string location = webRootPath + ConstantProperty.ImageUserPath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file[0].FileName);
                if (objFromDb != null)
                {
                    var oldFile = Path.Combine(location, objFromDb.AppFile.Name);
                    if (System.IO.File.Exists(oldFile))
                    {
                        System.IO.File.Delete(oldFile);
                    }
                    using (var fileStream = new FileStream(Path.Combine(location, fileName + extension), FileMode.Create))
                    {
                        file[0].CopyTo(fileStream);
                    }
                    objFromDb.AppFile.Name = fileName + extension;
                    _dbContext.SaveChanges();

                }
                else
                {  
                        using (var fileStream = new FileStream(Path.Combine(location, fileName + extension), FileMode.Create))
                        {
                            file[0].CopyTo(fileStream);
                        }
                        AppFile f = new AppFile()
                        {
                            Name = fileName + extension
                        };
                         _dbContext.AppFiles.Add(f);
                         _dbContext.SaveChanges();
                        var fileId = f.Id;

                        UserFile userFiles = new UserFile()
                        {
                            AppFileId = fileId,
                            UserId = expert.Id,
                            CreatedAt = DateTime.Now
                        };
                        _dbContext.UserFiles.Add(userFiles);
                        _dbContext.SaveChanges();
                    
                }
            }
            return RedirectToAction(); 
        }
    }
}
