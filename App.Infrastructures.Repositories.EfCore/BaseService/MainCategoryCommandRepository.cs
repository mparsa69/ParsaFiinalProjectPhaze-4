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
    public class MainCategoryCommandRepository : IMainCategoryCommandRepository
    {
        private readonly AppDbContext _dbConext;

        public MainCategoryCommandRepository(AppDbContext dbConext)
        {
            _dbConext = dbConext;
        }
        public async Task Add(MainCategoryDto model)
        {
            MainCategory mainCategory = new MainCategory()
            {
                Title = model.Title
            };
            await _dbConext.MainCategories.AddAsync(mainCategory);
            await _dbConext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var record = await _dbConext.MainCategories.SingleOrDefaultAsync(x => x.Id == id);
            _dbConext.MainCategories.Remove(record!);
            await _dbConext.SaveChangesAsync();
        }
        public async Task Update(MainCategoryDto model)
        {
            var record = await _dbConext.MainCategories.SingleOrDefaultAsync(x => x.Id == model.Id);
            record.Title = model.Title;
            _dbConext.MainCategories.Update(record);
            await _dbConext.SaveChangesAsync();
        }
    }
}
