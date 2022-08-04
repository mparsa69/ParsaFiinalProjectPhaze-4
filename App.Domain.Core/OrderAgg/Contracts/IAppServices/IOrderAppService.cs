using App.Domain.Core.OrderAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Contracts.IAppServices
{
    public interface IOrderAppService
    {
        Task Add(OrderDto model);
        Task Update(OrderDto model);
        Task Delete(int id);

        Task<List<OrderDto>>? GetAllAsync(string? name);
        List<OrderDto> GetAll(string? name);
        Task<OrderDto>? Get(int id);
        Task<OrderDto>? Get(string name);
        public void EnableShow(int id);
        public void DisableShow(int id);
    }
}
