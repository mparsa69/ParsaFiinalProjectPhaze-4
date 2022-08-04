using App.Domain.Core.ExpertAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.ExpertAgg.Contracts.IRepositories
{
    public interface IExpertSkillQueryRepository
    {
        Task<List<ExpertSkill>>? GetAllAsync();
        List<ExpertSkill>? GetAll();
        Task<ExpertSkill>? Get(string id);
    }
}
