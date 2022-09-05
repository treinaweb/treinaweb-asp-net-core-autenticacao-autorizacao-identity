namespace WebApplication4.Models.Entities
{
    public class Medico
    {
        public int Id { get; set; }
        public string CRM { get; set; } = String.Empty;
        public string Nome { get; set; } = String.Empty;
        public ICollection<Consulta> Consultas { get; set; } = null!;
    }
}
