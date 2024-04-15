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
            builder.Property(f => f.Nome).HasColumnName("nome").HasMaxLength(200);
            builder.Property(f => f.Email).HasColumnName("email").HasMaxLength(200);
            builder.Property(f => f.Celular).HasColumnName("celular").HasMaxLength(30);
            builder.Property(f => f.CriadoEm).HasColumnName("criadoEm").HasMaxLength(30);
            builder.Property(f => f.ModificadoEm).HasColumnName("modificadoEm").HasMaxLength(30);
            builder.HasMany(x => x.Enderecos).WithOne(x => x.Funcionario).HasForeignKey(e => e.IdFuncionario);

        }
    }
}
                                             