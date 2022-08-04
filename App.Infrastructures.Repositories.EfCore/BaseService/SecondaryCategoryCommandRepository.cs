using App.Domain.Core.BaseService.Contracts.IRepositories;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repositories.EfCore.BaseService
{
    public class SecondaryCategoryCommandRepository : ISecondaryCategoryCommandRepository
    {
        private readonly AppDbContext _dbConext;
       

        public SecondaryCategoryCommandRepository(AppDbContext dbConext)
        {
            _dbConext = dbConext;

        }
        public async Task Add(SecondaryCategoryDto model)
        {
            
            SecondaryCategory secondaryCategory = new SecondaryCategory()
            {
                Title = model.Title,
                MainCategoryId = model.MainCategoryId
            };
            await _dbConext.SecondaryCategories.AddAsync(secondaryCategory);
            await _dbConext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            
            var record = await _dbConext.SecondaryCategories.SingleOrDefaultAsync(x => x.Id == id);
            
            _dbConext.SecondaryCategories.Remove(record!);
            
            await _dbConext.SaveChangesAsync();
            
        }

        public async Task Update(SecondaryCategoryDto model)
        {
            
            var record = await _dbConext.SecondaryCategories.SingleOrDefaultAsync(x => x.Id == model.Id);
            record.Title = model.Title;
            record.MainCategoryId = model.MainCategoryId;
            _dbConext.SecondaryCategories.Update(record);
            await _dbConext.SaveChangesAsync();
        }
    }
}
