using App.Domain.Core.SuggestionAgg.Contracts.IAppServices;
using App.Domain.Core.SuggestionAgg.Contracts.IServices;
using App.Domain.Core.SuggestionAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Suggestion
{
    public class SuggestionAppService: ISuggestionAppService
    {
        private readonly ISuggestionService _suggestionService;
        public SuggestionAppService(ISuggestionService suggestionService)
        {
            _suggestionService = suggestionService;
        }

        public async Task Add(SuggestionDto model)
        {
            await _suggestionService.Add(model); ;
        }

        public async Task Delete(int id)
        {
            await _suggestionService.Delete(id);
        }

        public void DisableShow(int id)
        {
            _suggestionService.DisableShow(id);
        }

        public void EnableShow(int id)
        {
            _suggestionService.EnableShow(id);
        }

        public async Task<SuggestionDto>? Get(int id)
        {
            return await _suggestionService.Get(id);
        }

        public async Task<SuggestionDto>? Get(string name)
        {
            return await _suggestionService.Get(name);
        }

        public List<SuggestionDto> GetAll()
        {
            return _suggestionService.GetAll();
        }

        public async Task<List<SuggestionDto>>? GetAllAsync()
        {
            return await _suggestionService.GetAllAsync();
        }

        public List<SuggestionDto> OrderSuggestions(int orderId, string? name)
        {
            return _suggestionService.OrderSuggestions(orderId, name);
        }

        public async Task Update(SuggestionDto model)
        {
            await _suggestionService.Update(model);
        }
    }
}
