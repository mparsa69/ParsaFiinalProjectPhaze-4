using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using App.EndPoints.Web.Mvc.Models;
using System.Security.Claims;
using Utility;
using App.Domain.Core.FileAgg.Entities;
using App.EndPoints.Web.Mvc.Areas.Customer.Models.ViewModel;

namespace App.EndPoints.Web.Mvc.Areas.Customer.Controllers
{
    [Area("Customer")]
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

        
        public IActionResult IndexSecondaryCategory()
        {
            var data = _dbContext.SecondaryCategories.AsNoTracking().ToList();
            return View(data);
        }
        public IActionResult IndexThirdCategory(int id)
        {
            var data = _dbContext.ThirdCategories
                .Where(x=>x.SecondaryCategoryId==id)
                .AsNoTracking().ToList();
            return View(data);
        }

        public async Task<IActionResult> CustomerProfile()
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
            return RedirectToAction("CustomerProfile");
        }

        [HttpGet]
        public IActionResult Edit(string userId)
        {
            var customer = _dbContext.ApplicationUsers.Select(x => new CustomerVM()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.Family,
                PhoneNumber = x.PhoneNumber,
                Address = x.FullAddress,
            }).SingleOrDefault(x => x.Id == userId);
            return View(customer);
        }

        public IActionResult Edit(CustomerVM customer)
        {
            var user = _dbContext.ApplicationUsers.SingleOrDefault(x => x.Id == customer.Id);
            user.FirstName = customer.FirstName;
            user.Family = customer.LastName;
            user.PhoneNumber = customer.PhoneNumber;
            user.FullAddress = customer.Address;
            _dbContext.ApplicationUsers.Update(user);
            _dbContext.SaveChanges();

            //Start Image Update
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                var objFromDb = _dbContext.UserFiles.Include(x => x.AppFile).FirstOrDefault(x => x.UserId == customer.Id);
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
                        UserId = customer.Id,
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
