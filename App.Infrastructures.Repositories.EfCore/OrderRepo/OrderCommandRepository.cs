using App.Domain.Core.OrderAgg.Contracts.IRepositories;
using App.Domain.Core.OrderAgg.Dtos;
using App.Domain.Core.OrderAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructures.Repositories.EfCore.OrderRepo
{
    public class OrderCommandRepository : IOrderCommandRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderCommandRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Add(OrderDto model)
        {
            var order = new Order()
            {
                Description = model.Description,
                Status = model.Status,
                Title = model.Title,
                CustomerId = model.CustomerId,
                ThirdCategoryId = model.ThirdCategoryId
            };
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var record = await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == id);
            _dbContext.Orders.Remove(record);
            await _dbContext.SaveChangesAsync();

            
        }

        public void DisableShow(int id)
        {
            var order = _dbContext.Orders.SingleOrDefault(x => x.Id == id);
            order.IsShow = false;
            _dbContext.SaveChanges();
        }

        public void EnableShow(int id)
        {
            var order = _dbContext.Orders.SingleOrDefault(x => x.Id == id);
            order.IsShow = true;
            _dbContext.SaveChanges();
        }

        public async Task Update(OrderDto model)
        {
            var record = await _dbContext.Orders.SingleOrDefaultAsync(x => x.Id == model.Id);
            record.Description = model.Description;
            record.Status = model.Status;
            record.Title = model.Title;
            record.CustomerId = model.CustomerId;
            record.ThirdCategoryId = model.ThirdCategoryId;
            _dbContext.Orders.Update(record);
            await _dbContext.SaveChangesAsync();
        }
    }
}
