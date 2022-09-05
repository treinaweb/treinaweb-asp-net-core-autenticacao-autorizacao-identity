namespace WebApplication4.ViewModels.Consulta
{
    public class ListarConsultaViewModel
    {
        public int Id { get; set; }
        public string Paciente { get; set; } = String.Empty;
        public string Medico { get; set; } = String.Empty;
        public string Tipo { get; set; } = String.Empty;
        public DateTime Data { get; set; }
    }
}
