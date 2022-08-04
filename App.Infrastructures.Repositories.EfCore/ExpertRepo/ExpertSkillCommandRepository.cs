using App.Domain.Core.ExpertAgg.Contracts.IRepositories;
using App.Domain.Core.ExpertAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repositories.EfCore.ExpertRepo
{
    public class ExpertSkillCommandRepository : IExpertSkillCommandRepository
    {
        private readonly AppDbContext _dbConext;
        public ExpertSkillCommandRepository(AppDbContext dbConext)
        {
            _dbConext = dbConext;
        }
        public async Task Add(ExpertSkill model)
        {
            await _dbConext.ExpertSkills.AddAsync(model);
            await _dbConext.SaveChangesAsync();
        }

        public async Task Delete(string expertId,int thirdCategoryId)
        {
            var record = await _dbConext.ExpertSkills.SingleOrDefaultAsync(x => x.ExpertId == expertId && x.ThirdCategoryId == thirdCategoryId);
            _dbConext.Remove(record);
            await _dbConext.SaveChangesAsync();
        }

        public async Task Update(ExpertSkill model)
        {
            var record = await _dbConext.ExpertSkills.SingleOrDefaultAsync(x => x.ExpertId == model.ExpertId && x.ThirdCategoryId == model.ThirdCategoryId);
            _dbConext.ExpertSkills.Update(model);
            await _dbConext.SaveChangesAsync();
        }
    }
}
