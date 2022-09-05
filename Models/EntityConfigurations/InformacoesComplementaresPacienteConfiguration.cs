using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication4.Models.Entities;

namespace WebApplication4.Models.EntityConfigurations
{
    public class InformacoesComplementaresPacienteConfiguration : IEntityTypeConfiguration<InformacoesComplementaresPaciente>
    {
        public void Configure(EntityTypeBuilder<InformacoesComplementaresPaciente> builder)
        {
            builder.ToTable("InformacoesComplementaresPaciente");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.Alergias)
                   .HasMaxLength(200);
            
            builder.Property(i => i.MedicamentosEmUso)
                   .HasMaxLength(200);
            
            builder.Property(i => i.CirurgiasRealizadas)
                   .HasMaxLength(200);
        }
    }
}
