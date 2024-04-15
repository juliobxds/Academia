using Joshua.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joshua.Infra.Data.Data.Mapping
{
    public class EnderecoMap : BaseMap<Endereco>
    {
        public EnderecoMap() : base("Endereco")
        {

        }
        public override void Configure(EntityTypeBuilder<Endereco> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Logradouro).HasColumnName("logradouro").HasMaxLength(200);
            builder.Property(e => e.Rua).HasColumnName("rua").HasMaxLength(200);
            builder.Property(e => e.Cep).HasColumnName("cep").HasMaxLength(8);
            builder.Property(e => e.Cidade).HasColumnName("cidade").HasMaxLength(100);
            builder.Property(e => e.Estado).HasColumnName("estado").HasMaxLength(100);
            builder.Property(e => e.IdFuncionario).HasColumnName("idFuncionario");
        }
    }
}
