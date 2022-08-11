using App.Domain.Core.BaseService.Contracts.IAppServices;
using App.Domain.Core.BaseService.Contracts.IServices;
using App.Domain.Core.BaseService.Dtos;
using App.Domain.Core.BaseService.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace App.Domain.AppServices.BaseService
{
    public class MainCategoryAppService: IMainCategoryAppService
    {

        private readonly IMainCategoryService _mainCategoryService;
        private readonly IDistributedCache _distributedCache;
        

        public MainCategoryAppService(IMainCategoryService mainCategoryService, IDistributedCache distributedCache)
        {
            _mainCategoryService = mainCategoryService;
            _distributedCache = distributedCache;
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

        public async Task<List<MainCategoryDto>>? GetAllAsync()
        {
            var categories = new List<MainCategoryDto>();
            if (_distributedCache.Get("Categories") != null)
            {
                var categoryBytes = _distributedCache.Get("Categories");
                var categoryString=Encoding.UTF8.GetString(categoryBytes);
                categories=JsonSerializer.Deserialize<List<MainCategoryDto>>(categoryString);
            }
            else
            {
                categories=await _mainCategoryService.GetAllAsync();
                var categoryString = JsonSerializer.Serialize(categories);
                var categoryBytes=Encoding.UTF8.GetBytes(categoryString);
                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(20),
                };
                _distributedCache.Set("Categories", categoryBytes, options);
            }
            return categories;
            //return _mainCategoryService.GetAllAsync();
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
