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
    public class MainCategoryAppService: IMainCategoryAppService
    {

        private readonly IMainCategoryService _mainCategoryService;

        public MainCategoryAppService(IMainCategoryService mainCategoryService)
        {
            _mainCategoryService = mainCategoryService;
        }

        public async Task Add(MainCategoryDto model)
        {
            await _mainCategoryService.Add(model);
        }

        public async Task Delete(int id)
        {
            await _mainCategoryService.Delete(id);
        }

        public async Task<MainCategoryDto>? Get(int id)
        {
            var record = await _mainCategoryService.Get(id);
            
            return record;
        }

        public async Task<MainCategoryDto>? Get(string name)
        {
            var record = await _mainCategoryService.Get(name);
            
            return record;
        }

        public Task<List<MainCategoryDto>>? GetAllAsync()
        {
            return _mainCategoryService.GetAllAsync();
        }
        public List<MainCategoryDto>? GetAll()
        {
            return _mainCategoryService.GetAll();
        }

        public async Task Update(MainCategoryDto model)
        {
            await _mainCategoryService.Update(model);
        }
    }
}
