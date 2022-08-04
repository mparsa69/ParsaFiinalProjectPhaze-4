using App.Domain.Core.FileAgg.Dtos;
using App.EndPoints.Web.Mvc.Models;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using Utility;

namespace App.EndPoints.Web.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _dbContext;

        

        public HomeController(ILogger<HomeController> logger, AppDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        /*public string GetUserImage()
        {
            *//*var imageNameDto = _dbContext.UserFiles
                .Include(x => x.AppFile)
                .Where(x => x.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Select(x => new UserImageDto()
                {
                    Image = x.AppFile.Name
                })
                .FirstOrDefault();
            if (imageNameDto != null)
            {
                return View("_Layout", imageNameDto.Image);
            }
            else
            {
                UserImageDto noImageDto = new UserImageDto();
                noImageDto.Image = "No-Image.png";

                return View("_Layout", noImageDto.Image);
            }*//*
            
        }*/


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}