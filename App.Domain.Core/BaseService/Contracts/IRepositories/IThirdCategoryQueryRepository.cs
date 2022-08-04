using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseService.Contracts.IRepositories
{
    public interface IThirdCategoryQueryRepository
    {
        Task<List<ThirdCategoryDto>>? GetAllAsync();
        List<ThirdCategoryDto>? GetAll();
        Task<ThirdCategoryDto>? Get(int id);
        Task<ThirdCategoryDto>? Get(string name);
        Task<List<ThirdCategoryImageDto>> Details(int id);
        
    }
}
