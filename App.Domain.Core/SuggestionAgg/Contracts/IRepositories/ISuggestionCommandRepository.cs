using App.Domain.Core.SuggestionAgg.Dtos;
using App.Domain.Core.SuggestionAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.SuggestionAgg.Contracts.IRepositories
{
    public interface ISuggestionCommandRepository
    {
        Task Add(SuggestionDto model);
        Task Update(SuggestionDto model);
        Task Delete(int id);
        void EnableShow(int id);
        void DisableShow(int id);
    }
}
