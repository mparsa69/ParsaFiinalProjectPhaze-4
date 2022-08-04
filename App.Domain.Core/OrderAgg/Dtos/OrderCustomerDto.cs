using App.Domain.Core.OrderAgg.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Dtos
{
    public class OrderCustomerDto
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Title { get; set; }
        
        public OrderStatusCode Status { get; set; } = OrderStatusCode.WaitForSuggestion;
        public string? ExpertId { get; set; }
       /* public string? ExpertFirstName { get; set; }
        public string? ExpertLastName { get; set; }
        
        public long? BasePrice { get; set; }*/
        
        
    }
}
