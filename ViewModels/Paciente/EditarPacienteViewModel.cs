using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels.Paciente
{
    public class EditarPacienteViewModel
    {
        public int Id { get; set; }

        public string CPF { get; set; } = String.Empty;

        public string Nome { get; set; } = String.Empty;

        [Display(Name = "Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        
        public string? Alergias { get; set; }

        [Display(Name = "Medicamentos em uso")]
        public string? MedicamentosEmUso { get; set; }

        [Display(Name = "Cirurgias realizadas")]
        public string? CirurgiasRealizadas { get; set; }
    }
}
