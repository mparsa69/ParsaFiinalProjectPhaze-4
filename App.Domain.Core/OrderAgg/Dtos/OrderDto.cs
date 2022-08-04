using App.Domain.Core.OrderAgg.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public OrderStatusCode Status { get; set; } = OrderStatusCode.WaitForSuggestion;
        //public string Status { get; set; }
        public string CustomerId { get; set; }
        public string? ExpertId { get; set; }
        public int ThirdCategoryId { get; set; }
        public string ThirdCategoryName { get; set; }
        public long? BasePrice { get; set; }
        public string? CustomerName { get; set; }
        public bool? IsShow { get; set; } = false;
        //public UserManager<IdentityUser> CustomerName { get; set; }

    }
}
