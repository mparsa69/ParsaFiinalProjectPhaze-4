using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseService.Contracts.IServices
{
    public interface IThirdCategoryService
    {
        Task<int> Add(ThirdCategoryDto model);
        Task Update(ThirdCategoryDto model);
        Task Delete(int id);

        Task<List<ThirdCategoryDto>>? GetAll();
        Task<ThirdCategoryDto>? Get(int id);
        Task<ThirdCategoryDto>? Get(string name);
        Task<List<ThirdCategoryImageDto>> Details(int id);
    }
}
