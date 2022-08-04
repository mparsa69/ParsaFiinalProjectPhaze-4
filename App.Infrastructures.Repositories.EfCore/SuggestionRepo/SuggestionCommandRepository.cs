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
    public class SuggestionCommandRepository : ISuggestionCommandRepository
    {
        private readonly AppDbContext _dbContext;

        public SuggestionCommandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(SuggestionDto model)
        {
            var suggestion = new Suggestion()
            {
                Duration = model.Duration,
                SuggestedPrice = model.SuggestedPrice,
                StartTime = model.StartTime,
                ExpertId = model.ExpertId,
                OrderId = model.OrderId
            };
            await _dbContext.Suggestions.AddAsync(suggestion);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var record = await _dbContext.Suggestions.SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Suggestions.Remove(record!);
            await _dbContext.SaveChangesAsync();
        }

        public void DisableShow(int id)
        {
            var sugg = _dbContext.Suggestions.SingleOrDefault(x => x.Id == id);
            sugg.IsShow = false;
            _dbContext.SaveChanges();
        }

        public void EnableShow(int id)
        {
            var sugg = _dbContext.Suggestions.SingleOrDefault(x => x.Id == id);
            sugg.IsShow = true;
            _dbContext.SaveChanges();
        }

        public async Task Update(SuggestionDto model)
        {
            var record = await _dbContext.Suggestions.SingleOrDefaultAsync(x => x.Id == model.Id);
            record.Duration = model.Duration;
            record.SuggestedPrice = model.SuggestedPrice;
            record.StartTime = model.StartTime;
            record.ExpertId = model.ExpertId;
            record.OrderId = model.OrderId;
            _dbContext.Suggestions.Update(record);
            await _dbContext.SaveChangesAsync();
        }
    }
}
