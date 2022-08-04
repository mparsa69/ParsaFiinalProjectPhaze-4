using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseService.Contracts.IServices
{
    public interface ISecondaryCategoryService
    {
        Task Add(SecondaryCategoryDto model);
        Task Update(SecondaryCategoryDto model);
        Task Delete(int id);

        Task<List<SecondaryCategoryDto>>? GetAllAsync();
        List<SecondaryCategoryDto>? GetAll();
        Task<SecondaryCategoryDto>? Get(int id);
        Task<SecondaryCategoryDto>? Get(string name);
    }
}
