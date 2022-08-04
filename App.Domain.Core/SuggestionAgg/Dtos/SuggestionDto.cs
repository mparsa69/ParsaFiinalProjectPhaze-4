using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.SuggestionAgg.Dtos
{
    public class SuggestionDto
    {
        public int Id { get; set; }
        public long SuggestedPrice { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public string ExpertId { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? ThirdCategoryName { get; set; }
        public string ExpertUserName { get; set; }
        public int OrderId { get; set; }
        public bool? IsShow { get; set; } = false;
        public DateTime? SuggestionDate { get; set; }
        public bool IsApproved { get; set; } = false;
    }
}
