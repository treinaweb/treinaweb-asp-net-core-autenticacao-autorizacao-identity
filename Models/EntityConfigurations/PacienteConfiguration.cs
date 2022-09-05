using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication4.Models.Entities;

namespace WebApplication4.Models.EntityConfigurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacientes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.CPF)
                   .IsRequired()
                   .HasMaxLength(11);

            builder.HasIndex(p => p.CPF)
                   .IsUnique();

            builder.Property(p => p.Nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasOne(p => p.InformacoesComplementares)
                   .WithOne(i => i.Paciente)
                   .HasForeignKey<InformacoesComplementaresPaciente>(i => i.IdPaciente);

            builder.HasMany(p => p.Monitoramento)
                   .WithOne(m => m.Paciente)
                   .HasForeignKey(m => m.IdPaciente);
        }
    }
}
