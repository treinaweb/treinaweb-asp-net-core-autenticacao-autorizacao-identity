using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication4.Models.Entities;

namespace WebApplication4.Models.EntityConfigurations
{
    public class MonitoramentoPacienteConfiguration : IEntityTypeConfiguration<MonitoramentoPaciente>
    {
        public void Configure(EntityTypeBuilder<MonitoramentoPaciente> builder)
        {
            builder.ToTable("MonitoramentoPaciente");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.PressaoArterial)
                   .HasMaxLength(8);

            builder.Property(m => m.SaturacaoOxigenio)
                   .HasColumnType("TINYINT");
            
            builder.Property(m => m.FrequenciaCardiaca)
                   .HasColumnType("TINYINT");

            builder.Property(m => m.Temperatura)
                   .HasColumnType("DECIMAL(3,1)");

            builder.Property(m => m.DataAfericao)
                   .IsRequired();
        }
    }
}
