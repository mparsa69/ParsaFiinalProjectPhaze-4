using App.Domain.Core.BaseService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.ExpertAgg.Dtos
{
    public class ExpertSkillDto
    {
        public int ThirdCategoryId { get; set; }
        public string? ExpertId { get; set; }
        public List<string>? CategoryList { get; set; } 

    }
}
