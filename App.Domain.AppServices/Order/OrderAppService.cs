using App.Domain.Core.OrderAgg.Contracts.IAppServices;
using App.Domain.Core.OrderAgg.Contracts.IServices;
using App.Domain.Core.OrderAgg.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.Order
{
    public class OrderAppService : IOrderAppService
    {
        private readonly IOrderService _orderService;
        public OrderAppService(IOrderService orderService)
        {
            _orderService = orderService; 
        }

        public async Task Add(OrderDto model)
        {
            await _orderService.Add(model); ;
        }

        public async Task Delete(int id)
        {
            await _orderService.Delete(id);
        }

        public void DisableShow(int id)
        {
            _orderService.DisableShow(id);
        }

        public void EnableShow(int id)
        {
            _orderService.EnableShow(id);   
        }

        public async Task<OrderDto>? Get(int id)
        {
            return await _orderService.Get(id);
        }

        public async Task<OrderDto>? Get(string name)
        {
            return await _orderService.Get(name);
        }

        public List<OrderDto> GetAll(string? name)
        {
            return _orderService.GetAll(name);
        }

        public async Task<List<OrderDto>>? GetAllAsync(string? name)
        {
            return await _orderService.GetAllAsync(name);
        }

        public async Task Update(OrderDto model)
        {
            await _orderService.Update(model);
        }
    }
}
