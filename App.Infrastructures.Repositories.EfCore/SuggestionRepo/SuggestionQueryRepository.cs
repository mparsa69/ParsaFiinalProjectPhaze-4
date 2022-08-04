using App.Domain.Core.SuggestionAgg.Contracts.IRepositories;
using App.Domain.Core.SuggestionAgg.Dtos;
using App.Domain.Core.SuggestionAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repositories.EfCore.SuggestionRepo
{
    public class SuggestionQueryRepository : ISuggestionQueryRepository
    {
        private readonly AppDbContext _dbContext;
        public SuggestionQueryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<SuggestionDto>? Get(int id)
        {
            var result = await _dbContext.Suggestions.Where(x => x.Id == id).Select(y=>new SuggestionDto()
            {
                Id = y.Id,
                Duration=y.Duration,
                SuggestedPrice = y.SuggestedPrice,
                StartTime=y.StartTime,
                ExpertId=y.ExpertId,
                OrderId=y.OrderId
            }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<SuggestionDto>? Get(string name)
        {
            throw new NotImplementedException();
        }

        public  List<SuggestionDto>? GetAll()
        {
            var results = _dbContext.Suggestions.Select(y => new SuggestionDto()
            {
                Id = y.Id,
                Duration = y.Duration,
                SuggestedPrice = y.SuggestedPrice,
                StartTime = y.StartTime,
                ExpertId = y.ExpertId,
                ExpertUserName= _dbContext.Users.Where(x => x.Id == y.ExpertId).Select(z => z.UserName).SingleOrDefault(),
                OrderId = y.OrderId
            }).ToList();
            return results;
        }

        public async Task<List<SuggestionDto>>? GetAllAsync()
        {
            var results = await _dbContext.Suggestions.Select(y => new SuggestionDto()
            {
                Id = y.Id,
                Duration = y.Duration,
                SuggestedPrice = y.SuggestedPrice,
                StartTime = y.StartTime,
                ExpertId = y.ExpertId,
                ExpertUserName = _dbContext.Users.Where(x => x.Id == y.ExpertId).Select(z => z.UserName).SingleOrDefault(),
                OrderId = y.OrderId
            }).ToListAsync();
            return results;
        }

        public List<SuggestionDto> OrderSuggestions(int orderId, string? name)
        {
            var suggestions = _dbContext.Suggestions.Select(y=>new SuggestionDto()
            {
                Id = y.Id,
                Duration = y.Duration,
                SuggestedPrice = y.SuggestedPrice,
                StartTime = y.StartTime,
                ExpertId = y.ExpertId,
                ExpertUserName = _dbContext.Users.Where(x => x.Id == y.ExpertId).Select(z => z.UserName).SingleOrDefault(),
                OrderId = y.OrderId,
                IsApproved=y.IsApproved,
                IsShow=y.IsShow,
                SuggestionDate=y.SuggestionDate
            }).Where(x => x.OrderId == orderId).AsNoTracking().ToList();
            return suggestions;
        }
    }
}
