using Joshua.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joshua.Infra.Data.Data.Mapping
{
    public class FuncionarioMap : BaseMap<Funcionario>
    {
        public FuncionarioMap() : base("funcionario")
        {

        }
        public override void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            base.Configure(builder);
            builder.Property(f => f.Nome).HasColumnName("nome").HasMaxLength(200);
        }
    }
}
