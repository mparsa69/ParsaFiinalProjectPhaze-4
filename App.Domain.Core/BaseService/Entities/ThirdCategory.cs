using App.Domain.Core.ExpertAgg.Entities;
using App.Domain.Core.OrderAgg.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseService.Entities
{
    public class ThirdCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "عنوان")]
        public string Title { get; set; }
        
        [Display(Name = "توضیحات")]
        public string? Description { get; set; }
        [Display(Name = "قیمت")]
        public long? Price  { get; set; }
        public bool IsDeleted { get; set; } = false;

        #region Navigation Property
        public int SecondaryCategoryId { get; set; }
        public SecondaryCategory? SecondaryCategory { get; set; }
        public ICollection<Order>? Orders { get; set; }
        public ICollection<ExpertSkill>? ExpertSkills { get; set; }
        public ICollection<ThirdCategoryFile>? ThirdCategoryFiles { get; set; }
        #endregion
    }
}
