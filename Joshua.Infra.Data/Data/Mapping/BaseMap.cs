using Joshua.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joshua.Infra.Data.Data.Mapping
{
    public class BaseMap<T> : IEntityTypeConfiguration<T> where T : Base
    {
        private readonly string _tableName;

        public BaseMap(string tableName)
        {
            _tableName = tableName;
        }

        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            if (!string.IsNullOrEmpty(_tableName)) builder.ToTable(_tableName);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        }
    }
}
