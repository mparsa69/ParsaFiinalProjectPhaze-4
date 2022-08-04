using App.Domain.Core.OrderAgg.Dtos;
using App.Domain.Core.OrderAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Contracts.IRepositories
{
    public interface IOrderCommandRepository
    {
        Task Add(OrderDto model);
        Task Update(OrderDto model);
        Task Delete(int id);
        public void EnableShow(int id);
        public void DisableShow(int id);
    }
}
