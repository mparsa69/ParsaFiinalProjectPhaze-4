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
    public class ThirdCategoryService : IThirdCategoryService
    {
        private readonly IThirdCategoryCommandRepository _thirdCategoryCommandRepository;
        private readonly IThirdCategoryQueryRepository _thirdCategoryQueryRepository;
        public ThirdCategoryService(
            IThirdCategoryCommandRepository thirdCategoryCommandRepository,
            IThirdCategoryQueryRepository thirdCategoryQueryRepository
    )
        {
            _thirdCategoryCommandRepository = thirdCategoryCommandRepository;
            _thirdCategoryQueryRepository = thirdCategoryQueryRepository;
        }

        public async Task<int> Add(ThirdCategoryDto model)
        {
            return await _thirdCategoryCommandRepository.Add(model);
        }

        public async Task Delete(int id)
        {
            await _thirdCategoryCommandRepository.Delete(id);
        }

        public Task<List<ThirdCategoryImageDto>> Details(int id)
        {
            return _thirdCategoryQueryRepository.Details(id);
        }

        public async Task<ThirdCategoryDto>? Get(int id)
        {
            var record = await _thirdCategoryQueryRepository.Get(id);
            if (record == null)
            {
                throw new Exception($"Category {id} Doesn't Exists!");
            }
            return record;
        }

        public async Task<ThirdCategoryDto>? Get(string name)
        {
            var record = await _thirdCategoryQueryRepository.Get(name);
            if (record == null)
            {
                throw new Exception($"Category {name} Doesn't Exists!");
            }
            return record;
        }

        public Task<List<ThirdCategoryDto>>? GetAll()
        {
            return _thirdCategoryQueryRepository.GetAllAsync();
        }

        public async Task Update(ThirdCategoryDto model)
        {
            await _thirdCategoryCommandRepository.Update(model);
        }
    }
}
