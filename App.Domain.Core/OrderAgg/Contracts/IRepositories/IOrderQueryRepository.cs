using App.Domain.Core.OrderAgg.Dtos;
using App.Domain.Core.OrderAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Contracts.IRepositories
{
    public interface IOrderQueryRepository
    {
        Task<List<OrderDto>>? GetAllAsync(string? name);
        List<OrderDto>? GetAll(string? name);
        Task<OrderDto>? Get(int id);
        Task<OrderDto>? Get(string name);

    }
}
