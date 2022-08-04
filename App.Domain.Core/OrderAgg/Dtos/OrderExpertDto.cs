using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Dtos
{
    public class OrderExpertDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullAddress { get; set; }

        public string? CustomerId { get; set; }
        public string? ExpertId { get; set; }
        public int ThirdCategoryId { get; set; }
        public string? ThirdCategoryName { get; set; }
        public long? BasePrice { get; set; }
        public string? CustomerName { get; set; }
        public string? Phone { get; set; }
    }
}
