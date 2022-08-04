using App.Domain.Core.BaseService.Contracts.IAppServices;
using App.Domain.Core.BaseService.Contracts.IServices;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.FileAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace App.Domain.AppServices.BaseService
{
    public class ThirdCategoryAppService: IThirdCategoryAppService
    {

        private readonly IThirdCategoryService _thirdCategoryService;
        private readonly AppDbContext _dbContext;

        public ThirdCategoryAppService(IThirdCategoryService thirdCategoryService, AppDbContext dbContext)
        {
            _thirdCategoryService = thirdCategoryService;
            _dbContext=dbContext;
    }

        public async Task<int> Add(ThirdCategoryDto model)
        {
            var serviceId= await _thirdCategoryService.Add(model);
            return serviceId;
        }

        public async Task Add(ThirdCategoryDto model, string webRootPath,
            IList<IFormFile>? uploadFiles,string loginUserId)
        {
            var serviceId=await _thirdCategoryService.Add(model);


            foreach (var file in uploadFiles)
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
                var UserId = loginUserId;
                ThirdCategoryFile juctionFileAndThirdCategory = new ThirdCategoryFile()
                {
                    AppFileId = fileId,
                    ThirdCategoryId = serviceId,
                    CreatedUserId = UserId,
                    CreatedAt = DateTime.Now
                };
                await _dbContext.ThirdCategoryFiles.AddAsync(juctionFileAndThirdCategory);
                await _dbContext.SaveChangesAsync();

            }


        }

        public async Task Delete(int id)
        {
            await _thirdCategoryService.Delete(id);
        }

        public async Task<List<ThirdCategoryImageDto>> Details(int id)
        {
            return await _thirdCategoryService.Details(id);
        }

        public async Task<ThirdCategoryDto>? Get(int id)
        {
            var record = await _thirdCategoryService.Get(id);
            
            return record;
        }

        public async Task<ThirdCategoryDto>? Get(string name)
        {
            var record = await _thirdCategoryService.Get(name);
            
            return record;
        }

        public Task<List<ThirdCategoryDto>>? GetAll()
        {
            return _thirdCategoryService.GetAll();
        }

        public async Task Update(ThirdCategoryDto model)
        {
            await _thirdCategoryService.Update(model);
        }
    }
}
