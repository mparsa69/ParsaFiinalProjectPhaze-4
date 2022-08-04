using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.OrderAgg.Entities;
using App.Domain.Core.SuggestionAgg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infrastructures.Database.SqlServer.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
           

            builder
                .HasMany<Suggestion>(x => x.Suggestions)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId);

            builder
                .HasOne<ThirdCategory>(x => x.ThirdCategory)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ThirdCategoryId);


        }
    }
}
