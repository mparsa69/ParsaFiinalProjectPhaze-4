using App.Domain.Core.BaseService.Contracts.IAppServices;
using App.Domain.Core.BaseService.Contracts.IServices;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.BaseService
{
    public class SecondaryCategoryAppService: ISecondaryCategoryAppService
    {

        private readonly ISecondaryCategoryService _secondaryCategoryService;

        public SecondaryCategoryAppService(ISecondaryCategoryService secondaryCategoryService)
        {
            _secondaryCategoryService = secondaryCategoryService;
        }

        public async Task Add(SecondaryCategoryDto model)
        {
            await _secondaryCategoryService.Add(model);
        }

        public async Task Delete(int id)
        {
            await _secondaryCategoryService.Delete(id);
        }

        public async Task<SecondaryCategoryDto>? Get(int id)
        {
            var record = await _secondaryCategoryService.Get(id);
            
            return record;
        }

        public async Task<SecondaryCategoryDto>? Get(string name)
        {
            var record = await _secondaryCategoryService.Get(name);
            
            return record;
        }

        public Task<List<SecondaryCategoryDto>>? GetAllAsync()
        {
            return _secondaryCategoryService.GetAllAsync();
        }

        public List<SecondaryCategoryDto>? GetAll()
        {
            return _secondaryCategoryService.GetAll();
        }

        public async Task Update(SecondaryCategoryDto model)
        {
            await _secondaryCategoryService.Update(model);
        }
    }
}
