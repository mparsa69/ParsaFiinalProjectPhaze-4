using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Dtos
{
    public class CommentDto
    {
        public int Id { get; set; }
        public int? ThirdCategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CommentText { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int OrderId { get; set; }
        public bool? IsShow { get; set; } = false;

    }
}
