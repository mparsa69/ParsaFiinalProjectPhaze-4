using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int? ThirdCategoryId { get; set; }
        public string? CommentText { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? IsShow { get; set; } = false;

        public int OrderId { get; set; }
        public  Order? Order { get; set; } = null!;
       /* public  ThirdCategory ThirdCategory { get; set; } = null!;*/
    }
}
