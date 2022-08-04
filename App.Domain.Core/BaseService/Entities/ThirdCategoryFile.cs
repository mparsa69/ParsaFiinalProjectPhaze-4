using App.Domain.Core.FileAgg.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.BaseService.Entities
{
    public class ThirdCategoryFile
    {
        [Key]
        public int Id { get; set; }
        public string CreatedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ThirdCategoryId { get; set; }
        public int AppFileId { get; set; }

        public ThirdCategory? ThirdCategory { get; set; }
        public AppFile? AppFile { get; set; }
    }
}
