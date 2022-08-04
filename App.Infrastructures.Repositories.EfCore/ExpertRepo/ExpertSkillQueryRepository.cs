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
    public class ExpertSkillQueryRepository : IExpertSkillQueryRepository
    {
        private readonly AppDbContext _dbConext;
        public ExpertSkillQueryRepository(AppDbContext dbConext)
        {
            _dbConext = dbConext;
        }

        public async Task<ExpertSkill>? Get(string expertId)
        {
            var expert = await _dbConext.ExpertSkills.SingleOrDefaultAsync(x => x.ExpertId == expertId);
            return expert;
        }
        public List<ExpertSkill>? GetAll()
        {
            var experts = _dbConext.ExpertSkills.ToList();
            return experts;
        }

        public async Task<List<ExpertSkill>>? GetAllAsync()
        {
            var experts = await _dbConext.ExpertSkills.ToListAsync();
            return experts;
        }
    }
}
