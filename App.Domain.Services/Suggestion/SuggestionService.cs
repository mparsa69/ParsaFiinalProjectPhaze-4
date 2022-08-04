using App.Domain.Core.SuggestionAgg.Contracts.IRepositories;
using App.Domain.Core.SuggestionAgg.Contracts.IServices;
using App.Domain.Core.SuggestionAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.Suggestion
{
    public class SuggestionService: ISuggestionService
    {
        private readonly ISuggestionCommandRepository _suggestionCommandRepository;
        private readonly ISuggestionQueryRepository _suggestionQueryRepository;
        public SuggestionService(ISuggestionCommandRepository suggestionCommandRepository, ISuggestionQueryRepository suggestionQueryRepository)
        {

            _suggestionCommandRepository = suggestionCommandRepository;
            _suggestionQueryRepository = suggestionQueryRepository;
        }
        public async Task Add(SuggestionDto model)
        {
            await _suggestionCommandRepository.Add(model);
        }

        public async Task Delete(int id)
        {
            await _suggestionCommandRepository.Delete(id);
        }

        public void DisableShow(int id)
        {
            _suggestionCommandRepository.DisableShow(id);
        }

        public void EnableShow(int id)
        {
            _suggestionCommandRepository.EnableShow(id);
        }

        public async Task<SuggestionDto>? Get(int id)
        {
            return await _suggestionQueryRepository.Get(id);
        }

        public async Task<SuggestionDto>? Get(string name)
        {
            return await _suggestionQueryRepository.Get(name);
        }

        public List<SuggestionDto> GetAll()
        {
            return _suggestionQueryRepository.GetAll();
        }

        public async Task<List<SuggestionDto>>? GetAllAsync()
        {
            return await _suggestionQueryRepository.GetAllAsync();
        }

        public List<SuggestionDto> OrderSuggestions(int orderId, string? name)
        {
            return _suggestionQueryRepository.OrderSuggestions(orderId, name);
        }

        public async Task Update(SuggestionDto model)
        {
            await _suggestionCommandRepository.Update(model);
        }
    }
}
