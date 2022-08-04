using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.OrderAgg.Enums;
using App.Domain.Core.SuggestionAgg.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.OrderAgg.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "وضعیت سفارش")]
        public OrderStatusCode Status { get; set; } = OrderStatusCode.WaitForSuggestion;
        [Display(Name ="شناسه مشتری")]
        public string CustomerId { get; set; }
        [Display(Name ="شناسه متخصص")]
        public string? ExpertId { get; set; }
        public long? BasePrice { get; set; }
        public bool? IsShow { get; set; } = false;
        //public string Status { get; set; }

        #region Navigation Property


        public int ThirdCategoryId { get; set; }
        public ThirdCategory? ThirdCategory { get; set; }

        public ICollection<Suggestion>? Suggestions  { get; set; }
        public ICollection<Comment>? Comments  { get; set; }

        
        #endregion

    }
}
