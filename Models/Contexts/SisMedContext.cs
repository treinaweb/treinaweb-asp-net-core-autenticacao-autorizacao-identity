using Microsoft.EntityFrameworkCore;
using WebApplication4.Models.Entities;
using WebApplication4.Models.EntityConfigurations;

namespace WebApplication4.Models.Contexts
{
    public class SisMedContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SisMedContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Paciente> Pacientes => Set<Paciente>();
        public DbSet<Medico> Medicos => Set<Medico>();
        public DbSet<Consulta> Consultas => Set<Consulta>();
        public DbSet<InformacoesComplementaresPaciente> InformacoesComplementaresPaciente => Set<InformacoesComplementaresPaciente>();
        public DbSet<MonitoramentoPaciente> MonitoramentosPaciente => Set<MonitoramentoPaciente>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("SisMed"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PacienteConfiguration());
            modelBuilder.ApplyConfiguration(new MedicoConfiguration());
            modelBuilder.ApplyConfiguration(new ConsultaConfiguration());
            modelBuilder.ApplyConfiguration(new InformacoesComplementaresPacienteConfiguration());
            modelBuilder.ApplyConfiguration(new MonitoramentoPacienteConfiguration());
        }
    }
}
