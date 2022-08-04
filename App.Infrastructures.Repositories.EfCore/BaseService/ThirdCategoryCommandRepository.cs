using App.Domain.Core.BaseService.Contracts.IRepositories;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repositories.EfCore.BaseService
{
    public class ThirdCategoryCommandRepository : IThirdCategoryCommandRepository
    {
        private readonly AppDbContext _dbConext;

        public ThirdCategoryCommandRepository(AppDbContext dbConext)
        {
            _dbConext = dbConext;
        }
        public async Task<int> Add(ThirdCategoryDto model)
        {
            var thirdCategory = new ThirdCategory()
            {
                Title = model.Title,
                SecondaryCategoryId=model.SecondaryCategoryId,
                Description = model.Description,
                Price=model.Price
            };
            await _dbConext.ThirdCategories.AddAsync(thirdCategory);
            await _dbConext.SaveChangesAsync();
            return thirdCategory.Id;
        }

        public async Task Delete(int id)
        {
            var record = await _dbConext.ThirdCategories.SingleOrDefaultAsync(x => x.Id == id);
            _dbConext.ThirdCategories.Remove(record!);
            await _dbConext.SaveChangesAsync();
        }

        public async Task Update(ThirdCategoryDto model)
        {
            var record = await _dbConext.ThirdCategories.SingleOrDefaultAsync(x => x.Id == model.Id);
            record.Title = model.Title;
            record.SecondaryCategoryId = model.SecondaryCategoryId;
            _dbConext.ThirdCategories.Update(record);
            await _dbConext.SaveChangesAsync();
        }
    }
}
