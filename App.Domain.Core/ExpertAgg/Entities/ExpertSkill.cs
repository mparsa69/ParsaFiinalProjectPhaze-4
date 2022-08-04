using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.ExpertAgg.Entities
{
    public class ExpertSkill
    {
        public int ThirdCategoryId { get; set; }
        public ThirdCategory? ThirdCategory { get; set; }
        public string? ExpertId { get; set; }
        
    }
}
