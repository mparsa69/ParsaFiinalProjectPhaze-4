using App.Domain.Core.ApplicationUserAgg.Entities;
using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.OrderAgg.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.FileAgg.Entities
{
    public class AppFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ThirdCategoryFile>? ThirdCategoryFiles { get; set; }

    }
}
