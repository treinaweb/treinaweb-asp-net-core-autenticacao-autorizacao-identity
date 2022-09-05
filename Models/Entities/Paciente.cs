namespace WebApplication4.Models.Entities
{
    public class Paciente
    {
        public Paciente()
        {
        }

        public int Id { get; set; }
        public string CPF { get; set; } = String.Empty;
        public string Nome { get; set; } = String.Empty;
        public DateTime DataNascimento { get; set; }
        public InformacoesComplementaresPaciente? InformacoesComplementares { get; set; }
        public ICollection<MonitoramentoPaciente> Monitoramento { get; set; } = null!;
        public ICollection<Consulta> Consultas { get; set; } = null!;
    }
}
