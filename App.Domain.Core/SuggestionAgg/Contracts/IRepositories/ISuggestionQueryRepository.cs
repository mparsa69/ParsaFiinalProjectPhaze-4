using App.Domain.Core.SuggestionAgg.Dtos;
using App.Domain.Core.SuggestionAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.SuggestionAgg.Contracts.IRepositories
{
    public interface ISuggestionQueryRepository
    {
        Task<List<SuggestionDto>>? GetAllAsync();
        List<SuggestionDto>? GetAll();
        Task<SuggestionDto>? Get(int id);
        Task<SuggestionDto>? Get(string name);
        List<SuggestionDto> OrderSuggestions(int orderId, string? name);

    }
}
