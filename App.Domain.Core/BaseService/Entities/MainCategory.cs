using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseService.Entities
{
    public class MainCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="عنوان")]
        public string Title { get; set; }
        public bool IsDeleted { get; set; } = false;

        #region Navigation Property
        public ICollection<SecondaryCategory>? secondaryCategories { get; set; }
        #endregion
    }
}
