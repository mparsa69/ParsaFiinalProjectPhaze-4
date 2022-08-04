using App.Domain.Core.BaseService.Entities;
using App.Domain.Core.ExpertAgg.Entities;
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
    public class ExpertSkillConfiguration : IEntityTypeConfiguration<ExpertSkill>
    {
        public void Configure(EntityTypeBuilder<ExpertSkill> builder)
        {
            builder.HasKey(sc => new { sc.ExpertId, sc.ThirdCategoryId });

            

            builder
                .HasOne<ThirdCategory>(x => x.ThirdCategory)
                .WithMany(x => x.ExpertSkills)
                .HasForeignKey(x => x.ThirdCategoryId);
                
                

        }
    }
}