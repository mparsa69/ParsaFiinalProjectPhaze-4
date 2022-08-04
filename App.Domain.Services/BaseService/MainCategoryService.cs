using App.Domain.Core.BaseService.Contracts.IRepositories;
using App.Domain.Core.BaseService.Contracts.IServices;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.BaseService
{
    public class MainCategoryService : IMainCategoryService
    {
        private readonly IMainCategoryCommandRepository _mainCategoryCommandRepository;
        private readonly IMainCategoryQueryRepository _mainCategoryQueryRepository;
        public MainCategoryService(
            IMainCategoryCommandRepository mainCategoryCommandRepository,
            IMainCategoryQueryRepository mainCategoryQueryRepository
    )
        {
            _mainCategoryCommandRepository = mainCategoryCommandRepository;
            _mainCategoryQueryRepository = mainCategoryQueryRepository;
        }

        public async Task Add(MainCategoryDto model)
        {
            await _mainCategoryCommandRepository.Add(model);
        }

        public async Task Delete(int id)
        {
            await _mainCategoryCommandRepository.Delete(id);
        }

        public async Task<MainCategoryDto>? Get(int id)
        {
            var record = await _mainCategoryQueryRepository.Get(id);
            if (record == null)
            {
                throw new Exception($"Category {id} Doesn't Exists!");
            }
            return record;
        }

        public async Task<MainCategoryDto>? Get(string name)
        {
            var record = await _mainCategoryQueryRepository.Get(name);
            if (record == null)
            {
                throw new Exception($"Category {name} Doesn't Exists!");
            }
            return record;
        }

        public Task<List<MainCategoryDto>>? GetAllAsync()
        {
            return _mainCategoryQueryRepository.GetAllAsync();
        }

        public List<MainCategoryDto>? GetAll()
        {
            return _mainCategoryQueryRepository.GetAll();
        }

        public async Task Update(MainCategoryDto model)
        {
            await _mainCategoryCommandRepository.Update(model);
        }

       
    }
}
