using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.SuggestionAgg.Dtos
{
    public class SuggestionForCustomerDto
    {
        public int Id { get; set; }
        public string ExpertId { get; set; }
        public string ExpertUserName { get; set; }
        public string ExpertFirstName { get; set; }
        public string ExpertLastName { get; set; }
        public long SuggestedPrice { get; set; }
        public string StartTime { get; set; }
        public string Duration { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? SuggestionDate { get; set; }
        
    }
}
