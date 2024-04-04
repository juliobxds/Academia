using Joshua.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joshua.Infra.Data.Data.Mapping
{
    public class FuncionarioMap : BaseMap<Funcionario>
    {
        public FuncionarioMap() : base("Funcionario")
        {

        }
        public override void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            base.Configure(builder);
            builder.Property(f => f.Nome).HasColumnName("Nome").HasMaxLength(200);
            builder.Property(f => f.Email).HasColumnName("Email").HasMaxLength(200);
            builder.Property(f => f.Celular).HasColumnName("Celular").HasMaxLength(30);

        }
    }
}
