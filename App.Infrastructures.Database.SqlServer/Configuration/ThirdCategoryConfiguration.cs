using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.ExpertAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Database.SqlServer.Configuration
{
    public class ThirdCategoryConfiguration : IEntityTypeConfiguration<ThirdCategory>
    {
        public void Configure(EntityTypeBuilder<ThirdCategory> builder)
        {
            builder
                .HasMany<ExpertSkill>(x => x.ExpertSkills)
                .WithOne(x => x.ThirdCategory)
                .HasForeignKey(x => x.ThirdCategoryId);

            builder
                .HasOne<SecondaryCategory>(x => x.SecondaryCategory)
                .WithMany(x => x.ThirdCategories)
                .HasForeignKey(x => x.SecondaryCategoryId);
        }
    }
}
