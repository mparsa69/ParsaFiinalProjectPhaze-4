using App.Domain.Core.OrderAgg.Contracts.IRepositories;
using App.Domain.Core.OrderAgg.Dtos;
using App.Domain.Core.OrderAgg.Entities;
using App.Infrastructures.Database.SqlServer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Repositories.EfCore.OrderRepo
{
    public class OrderQueryRepository : IOrderQueryRepository
    {
        private readonly AppDbContext _dbContext;
        public OrderQueryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<OrderDto>? Get(int id)
        {
            var result = await _dbContext.Orders.Where(x=>x.Id==id).Select(y=>new OrderDto()
            {
                Id= y.Id,   
                Description=y.Description,
                Status=y.Status,
                Title=y.Title,
                CustomerId=y.CustomerId,
                ThirdCategoryId=y.ThirdCategoryId
            }).SingleOrDefaultAsync();
            return result;
        }

        public async Task<OrderDto>? Get(string name)
        {
            var result = await _dbContext.Orders.Where(x => x.Title.Contains(name)).Select(y => new OrderDto()
            {
                Id = y.Id,
                Description = y.Description,
                Status = y.Status,
                Title = y.Title,
                CustomerId = y.CustomerId,
                ThirdCategoryId = y.ThirdCategoryId
            }).FirstOrDefaultAsync();
            return result;
        }

        public List<OrderDto>? GetAll(string? name)
        {
            List<OrderDto> results;
            if (string.IsNullOrWhiteSpace(name))
            {
                results = _dbContext.Orders.Include(x=>x.ThirdCategory).Select(x => new OrderDto()
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    BasePrice = x.BasePrice,
                    Description = x.Description,
                    ExpertId = x.ExpertId,
                    Status = x.Status,
                    ThirdCategoryId = x.ThirdCategoryId,
                    ThirdCategoryName=x.ThirdCategory.Title,
                    Title = x.Title,
                    IsShow=x.IsShow,
                    CustomerName = _dbContext.Users.Where(y => y.Id == x.CustomerId).Select(z => z.UserName).FirstOrDefault()
                }).ToList();
            }
            else
            {
                results = _dbContext.Orders.Select(x => new OrderDto()
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    BasePrice = x.BasePrice,
                    Description = x.Description,
                    ExpertId = x.ExpertId,
                    Status = x.Status,
                    ThirdCategoryId = x.ThirdCategoryId,
                    ThirdCategoryName = x.ThirdCategory.Title,
                    Title = x.Title,
                    IsShow = x.IsShow,
                    CustomerName = _dbContext.Users.Where(y => y.Id == x.CustomerId).Select(z => z.UserName).FirstOrDefault()
                }).Where(x => x.CustomerName.Contains(name)).ToList();
            }
            
            return results;
        }
        public async Task<List<OrderDto>>? GetAllAsync(string? name)
        {
            List<OrderDto> results;
            if (string.IsNullOrWhiteSpace(name))
            {
                results =await _dbContext.Orders.Select(x => new OrderDto()
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    BasePrice = x.BasePrice,
                    Description = x.Description,
                    ExpertId = x.ExpertId,
                    Status = x.Status,
                    ThirdCategoryId = x.ThirdCategoryId,
                    ThirdCategoryName = x.ThirdCategory.Title,
                    Title = x.Title,
                    IsShow = x.IsShow,
                    CustomerName = _dbContext.Users.Where(y => y.Id == x.CustomerId).Select(z => z.UserName).FirstOrDefault()
                }).ToListAsync();
            }
            else
            {
                results =await _dbContext.Orders.Select(x => new OrderDto()
                {
                    Id = x.Id,
                    CustomerId = x.CustomerId,
                    BasePrice = x.BasePrice,
                    Description = x.Description,
                    ExpertId = x.ExpertId,
                    Status = x.Status,
                    ThirdCategoryId = x.ThirdCategoryId,
                    ThirdCategoryName = x.ThirdCategory.Title,
                    Title = x.Title,
                    IsShow = x.IsShow,
                    CustomerName = _dbContext.Users.Where(y => y.Id == x.CustomerId).Select(z => z.UserName).FirstOrDefault()
                }).Where(x => x.CustomerName.Contains(name)).ToListAsync();
            }
            return results;
        }
    }
}
