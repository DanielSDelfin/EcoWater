using Microsoft.EntityFrameworkCore;
using EcoWater.Models;

namespace EcoWater.Persistence
{
    public class OracleDbContext : DbContext
    {
        public DbSet<Embarcacoes> Embarcacoes { get; set; }
        public DbSet<Incidentes> Incidentes { get; set; }
        public DbSet<Monitoramentos> Monitoramentos { get; set; }
        public DbSet<Proprietarios> Proprietarios { get; set; }
        public DbSet<RegistrosPoluicao> RegistrosPoluicao { get; set; }
        public DbSet<Sensores> Sensores { get; set; }

        public OracleDbContext(DbContextOptions<OracleDbContext> options) : base(options)
        {
        }

    }

}
