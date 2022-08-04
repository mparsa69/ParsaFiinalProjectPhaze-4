using App.Domain.Core.SuggestionAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.SuggestionAgg.Contracts.IServices
{
    public interface ISuggestionService
    {
        Task Add(SuggestionDto model);
        Task Update(SuggestionDto model);
        Task Delete(int id);

        Task<List<SuggestionDto>>? GetAllAsync();
        List<SuggestionDto> GetAll();
        Task<SuggestionDto>? Get(int id);
        Task<SuggestionDto>? Get(string name);
        List<SuggestionDto> OrderSuggestions(int orderId, string? name);

        void EnableShow(int id);
        void DisableShow(int id);


    }
}
