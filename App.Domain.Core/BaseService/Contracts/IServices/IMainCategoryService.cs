using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseService.Contracts.IServices
{
    public interface IMainCategoryService
    {
        Task Add(MainCategoryDto model);
        Task Update(MainCategoryDto model);
        Task Delete(int id);

        Task<List<MainCategoryDto>>? GetAllAsync();
        List<MainCategoryDto> GetAll();
        Task<MainCategoryDto>? Get(int id);
        Task<MainCategoryDto>? Get(string name);
    }
}
