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
    public class MainCategoryConfiguration : IEntityTypeConfiguration<MainCategory>
    {
        public void Configure(EntityTypeBuilder<MainCategory> builder)
        {
            builder
                .HasMany<SecondaryCategory>(g => g.secondaryCategories)
                .WithOne(s => s.MainCategory)
                .HasForeignKey(s => s.MainCategoryId);
        }
    }
}
