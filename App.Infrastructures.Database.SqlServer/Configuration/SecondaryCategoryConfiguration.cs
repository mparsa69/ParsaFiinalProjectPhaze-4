using App.Domain.Core.BaseService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Database.SqlServer.Configuration
{
    public class SecondaryCategoryConfiguration : IEntityTypeConfiguration<SecondaryCategory>
    {
        public void Configure(EntityTypeBuilder<SecondaryCategory> builder)
        {
            builder
                .HasMany<ThirdCategory>(g => g.ThirdCategories)
                .WithOne(s => s.SecondaryCategory)
                .HasForeignKey(s => s.SecondaryCategoryId);

            builder
                .HasOne<MainCategory>(x => x.MainCategory)
                .WithMany(x => x.secondaryCategories)
                .HasForeignKey(x => x.MainCategoryId);
        }
    }
}
