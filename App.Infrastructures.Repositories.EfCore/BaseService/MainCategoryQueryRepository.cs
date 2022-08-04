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
    public class MainCategoryQueryRepository : IMainCategoryQueryRepository
    {
        private readonly AppDbContext _dbConext;

        public MainCategoryQueryRepository(AppDbContext dbConext)
        {
            _dbConext = dbConext;
        }
        public async Task<MainCategoryDto>? Get(int id)
        {
            var result = await _dbConext.MainCategories.Where(x=>x.Id==id).Select(x=>new MainCategoryDto()
            {
                Id = x.Id,
                Title=x.Title
            }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<MainCategoryDto>? Get(string name)
        {
            var result = await _dbConext.MainCategories.Where(x => x.Title.Contains(name)).Select(x => new MainCategoryDto()
            {
                Id = x.Id,
                Title = x.Title
            }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<MainCategoryDto>>? GetAllAsync()
        {
            var results =await _dbConext.MainCategories.Select(x => new MainCategoryDto()
            {
                Id = x.Id,
                Title = x.Title
            }).ToListAsync();
            return results;
        }

        public List<MainCategoryDto>? GetAll()
        {
            var results =  _dbConext.MainCategories.Select(x => new MainCategoryDto()
            {
                Id = x.Id,
                Title = x.Title
            }).ToList();
            return results;
        }
    }
}
