using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.FileAgg.Entities
{
    public class UserFile
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public int AppFileId { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppFile? AppFile { get; set; }
    }
}
