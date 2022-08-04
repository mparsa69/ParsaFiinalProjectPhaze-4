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
    public class SecondaryCategoryQueryRepository : ISecondaryCategoryQueryRepository
    {
        private readonly AppDbContext _dbConext;

        public SecondaryCategoryQueryRepository(AppDbContext dbConext)
        {
            _dbConext = dbConext;
        }
        public async Task<SecondaryCategoryDto>? Get(int id)
        {
            var result = await _dbConext.SecondaryCategories.Where(x=>x.Id==id).Select(x=>new SecondaryCategoryDto()
            {
                Id=x.Id,
                Title=x.Title,
                MainCategoryId=x.MainCategoryId
            }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<SecondaryCategoryDto>? Get(string name)
        {
            var result = await _dbConext.SecondaryCategories.Where(x => x.Title.Contains(name)).Select(x => new SecondaryCategoryDto()
            {
                Id = x.Id,
                Title = x.Title,
                MainCategoryId = x.MainCategoryId
            }).FirstOrDefaultAsync(x => x.Title.Contains(name));
            return result;
        }

        public async Task<List<SecondaryCategoryDto>>? GetAllAsync()
        {
            var results =await _dbConext.SecondaryCategories.Select(x => new SecondaryCategoryDto()
            {
                Id = x.Id,
                Title = x.Title,
                MainCategoryId = x.MainCategoryId
            }).ToListAsync();
            return results;
        }
        public  List<SecondaryCategoryDto>? GetAll()
        {
            var results =_dbConext.SecondaryCategories.Select(x => new SecondaryCategoryDto()
            {
                Id = x.Id,
                Title = x.Title,
                MainCategoryId = x.MainCategoryId
            }).ToList();
            return results;
        }
    }
}
