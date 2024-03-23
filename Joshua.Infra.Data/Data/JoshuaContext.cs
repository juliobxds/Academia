using Joshua.Domain.Models;
using Joshua.Infra.Data.Data.Mapping;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Joshua.Infra.Data.Data
{
    public  class JoshuaContext : IdentityDbContext
    {
        public virtual DbSet<Funcionario> Funcionarios { get; set; }

        public JoshuaContext(DbContextOptions<JoshuaContext> options) : base(options)
        {
        }
        public JoshuaContext()
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    "Data Source=DESKTOP-DIFT32I;Integrated Security=True; Initial Catalog=MyLocal;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new FuncionarioMap());

        }
    }
}
