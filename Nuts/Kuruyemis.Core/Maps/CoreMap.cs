
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nuts.Core.Entities;

namespace Nuts.Core.Maps
{
    public class CoreMap<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class,IEntity,new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

        }
    }
}
