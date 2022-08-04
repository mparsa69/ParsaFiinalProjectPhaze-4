using App.Domain.Core.ExpertAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.ExpertAgg.Contracts.IRepositories
{
    public interface IExpertSkillCommandRepository
    {
        Task Add(ExpertSkill model);
        Task Update(ExpertSkill model);
        Task Delete(string expertId, int thirdCategoryId);
    }
}
