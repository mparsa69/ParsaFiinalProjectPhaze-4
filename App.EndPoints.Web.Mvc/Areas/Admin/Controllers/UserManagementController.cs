using App.Domain.Core.ApplicationUserAgg.Entities;
using App.Domain.Core.FileAgg.Entities;
using App.EndPoints.Web.Mvc.Areas.Admin.Models.ViewModels.Accounts;
using App.EndPoints.Web.Mvc.Areas.Expert.Models.ViewModel;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utility;

namespace App.EndPoints.Web.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = ConstantProperty.Role_Admin)]
    public class UserManagementController : Controller
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly AppDbContext _dbContext;
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserManagementController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager, AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index(string? name)
        {

            List<UserManagementVM> model;
            if (string.IsNullOrEmpty(name))
            {
                model = await _userManager.Users.Select(x => new UserManagementVM
                {
                    Id = x.Id,
                    Name = x.UserName,
                    //FirstName = x.FirstName,
                    //LastName=x.Family,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                }).ToListAsync();
            }
            else
            {
                model = await _userManager.Users
                    .Where(x => x.UserName.Contains(name))
                    .Select(x => new UserManagementVM
                    {
                        Id = x.Id,
                        Name = x.UserName,
                        //FirstName = x.FirstName,
                        //LastName = x.Family,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                    }).ToListAsync();
            }

            foreach (var user in model)
            {
                user.Roles =
                    await _userManager.GetRolesAsync(await _userManager.Users.FirstAsync(x => x.Id == user.Id));
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UserRoles(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            var model = new UserManagementVM()
            {
                Id = user.Id,
                Name = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Roles = await _userManager.GetRolesAsync(user)
            };
            var allRoles = await _roleManager.Roles.Select(x => x.Name).ToListAsync();
            var selectedRoles = allRoles.Where(x => !model.Roles.Contains(x)).ToList();
            
            ViewBag.AllRoles = selectedRoles;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddUserRoles(string userId,string role)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            await _userManager.AddToRoleAsync(user, role);
            return RedirectToAction("UserRoles", new {userId=userId});

        }
        [HttpPost]
        public async Task<IActionResult> RemoveRole(string userId,string role)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            await _userManager.RemoveFromRoleAsync(user, role);
            return RedirectToAction("UserRoles", new { userId = userId });
        }
        [HttpPost]
        public async Task<IActionResult> RemoveUser(string userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateUser(string userId)
        {
            var model = _dbContext.ApplicationUsers.Select(x => new ExpertVM()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.Family,
                PhoneNumber = x.PhoneNumber,
                Address = x.FullAddress,
            }).SingleOrDefault(x => x.Id == userId);
            return View(model);
            
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(ExpertVM model)
        {
            var user = _dbContext.ApplicationUsers.SingleOrDefault(x => x.Id == model.Id);
            user.FirstName = model.FirstName;
            user.Family = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.FullAddress = model.Address;
            _dbContext.ApplicationUsers.Update(user);
            _dbContext.SaveChanges();

            //Start Image Update
            var file = HttpContext.Request.Form.Files;
            if (file.Count > 0)
            {
                var objFromDb = _dbContext.UserFiles.Include(x => x.AppFile).FirstOrDefault(x => x.UserId == model.Id);
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
                        UserId = model.Id,
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
