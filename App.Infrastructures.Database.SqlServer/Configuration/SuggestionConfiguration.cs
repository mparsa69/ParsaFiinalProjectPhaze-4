using App.Domain.Core.ExpertAgg.Entities;
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
    public class SuggestionConfiguration : IEntityTypeConfiguration<Suggestion>
    {
        public void Configure(EntityTypeBuilder<Suggestion> builder)
        {
            

            builder
               .HasOne<Order>(x => x.Order)
               .WithMany(x => x.Suggestions)
               .HasForeignKey(x => x.OrderId);
        }
    }
}
