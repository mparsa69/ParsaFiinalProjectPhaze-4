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
    public class SecondaryCategoryService : ISecondaryCategoryService
    {
        private readonly ISecondaryCategoryCommandRepository _secondaryCategoryCommandRepository;
        private readonly ISecondaryCategoryQueryRepository _secondaryCategoryQueryRepository;
        public SecondaryCategoryService(
            ISecondaryCategoryCommandRepository secondaryCategoryCommandRepository,
            ISecondaryCategoryQueryRepository secondaryCategoryQueryRepository
    )
        {
            _secondaryCategoryCommandRepository = secondaryCategoryCommandRepository;
            _secondaryCategoryQueryRepository = secondaryCategoryQueryRepository;
        }

        public async Task Add(SecondaryCategoryDto model)
        {
            await _secondaryCategoryCommandRepository.Add(model);
        }

        public async Task Delete(int id)
        {
            await _secondaryCategoryCommandRepository.Delete(id);
        }

        public async Task<SecondaryCategoryDto>? Get(int id)
        {
            var record = await _secondaryCategoryQueryRepository.Get(id);
            if (record == null)
            {
                throw new Exception($"Category {id} Doesn't Exists!");
            }
            return record;
        }

        public async Task<SecondaryCategoryDto>? Get(string name)
        {
            var record = await _secondaryCategoryQueryRepository.Get(name);
            if (record == null)
            {
                throw new Exception($"Category {name} Doesn't Exists!");
            }
            return record;
        }

        public Task<List<SecondaryCategoryDto>>? GetAllAsync()
        {
            return _secondaryCategoryQueryRepository.GetAllAsync();
        }
        public List<SecondaryCategoryDto>? GetAll()
        {
            return _secondaryCategoryQueryRepository.GetAll();
        }

        public async Task Update(SecondaryCategoryDto model)
        {
            await _secondaryCategoryCommandRepository.Update(model);
        }
    }
}
