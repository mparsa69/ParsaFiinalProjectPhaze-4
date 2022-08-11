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
    public class MainCategoryCommandRepository : IMainCategoryCommandRepository
    {
        private readonly AppDbContext _dbConext;
        private readonly ILogger<MainCategoryCommandRepository> _logger;

        public MainCategoryCommandRepository(AppDbContext dbConext, ILogger<MainCategoryCommandRepository> logger)
        {
            _dbConext = dbConext;
            _logger = logger;
        }
        public async Task Add(MainCategoryDto model)
        {
            MainCategory mainCategory = new MainCategory()
            {
                Title = model.Title
            };
            await _dbConext.MainCategories.AddAsync(mainCategory);
            await _dbConext.SaveChangesAsync();
            _logger.LogInformation("New Main Category Added"+":"+mainCategory.Title);
        }

        public async Task Delete(int id)
        {
            if (id == null)
            {
                _logger.LogWarning(" Main Category Id Is Null");
            }
            var record = await _dbConext.MainCategories.SingleOrDefaultAsync(x => x.Id == id);
            if (record == null)
            {
                // _logger.LogWarning(" Main Category Model Is Null");
                throw new Exception("Main Category Model Is Null");
            }
            _dbConext.MainCategories.Remove(record!);
            await _dbConext.SaveChangesAsync();
            _logger.LogInformation(" Main Category Deleted"+":"+record.Title);
        }
        public async Task Update(MainCategoryDto model)
        {
            if (model == null)
            {
                _logger.LogError(" Main Category Is Null");
            }
            var record = await _dbConext.MainCategories.SingleOrDefaultAsync(x => x.Id == model.Id);
            record.Title = model.Title;
            _dbConext.MainCategories.Update(record);
            await _dbConext.SaveChangesAsync();
            _logger.LogInformation(" Main Category Updated" + ":" + record.Title);
        }
    }
}
