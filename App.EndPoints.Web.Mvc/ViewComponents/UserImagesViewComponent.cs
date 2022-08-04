using App.Domain.Core.FileAgg.Dtos;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace App.EndPoints.Web.Mvc.ViewComponents
{
    public class UserImagesViewComponent:ViewComponent
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly AppDbContext _dbContext;

        public UserImagesViewComponent(AppDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        public IViewComponentResult Invoke()
        {
            var imageNameDto = _dbContext.UserFiles
                .Include(x => x.AppFile)
                /*.Where(x => x.UserId == _httpContextAccessor.HttpContext.User.Identity.Name)*/
                .Where(x => x.UserId == _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                .Select(x => new UserImageDto()
                {
                    Image = x.AppFile.Name
                })
                .FirstOrDefault();
            if (imageNameDto != null)
            {
                return View("_UserImages", imageNameDto);
            }
            else
            {
                UserImageDto noImageDto = new UserImageDto();
                noImageDto.Image = "No-Image.png";

                return View("_UserImages", noImageDto);
            }
            
        }
    }
}
