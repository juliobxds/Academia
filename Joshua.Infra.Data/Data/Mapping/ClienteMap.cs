using Joshua.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joshua.Infra.Data.Data.Mapping
{
    public class ClienteMap : BaseMap<Cliente>
    {
        public ClienteMap() : base("Cliente")
        {

        }
        public override void Configure(EntityTypeBuilder<Cliente> builder)
        {
            base.Configure(builder);
            builder.Property(c => c.Nome).HasColumnName("nome").HasMaxLength(200);
            builder.Property(c => c.Email).HasColumnName("email").HasMaxLength(200);
            builder.Property(c => c.Celular).HasColumnName("celular").HasMaxLength(30);
            builder.Property(c => c.CriadoEm).HasColumnName("criadoEm").HasMaxLength(30);
            builder.Property(c => c.ModificadoEm).HasColumnName("modificadoEm").HasMaxLength(30);
            builder.HasMany(c => c.Enderecos).WithOne(e => e.Cliente).HasForeignKey(e => e.idCliente);

        }
    }
}
                                             