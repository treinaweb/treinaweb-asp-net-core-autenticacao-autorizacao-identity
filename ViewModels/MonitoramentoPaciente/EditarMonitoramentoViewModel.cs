using System.ComponentModel.DataAnnotations;

namespace WebApplication4.ViewModels.MonitoramentoPaciente
{
    public class EditarMonitoramentoViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Pressão arterial")]
        public string PressaoArterial { get; set; } = String.Empty;


        [Display(Name = "Temperatura")]
        public decimal Temperatura { get; set; }


        [Display(Name = "Saturação de oxigênio")]
        public int SaturacaoOxigenio { get; set; }


        [Display(Name = "Frequência cardíaca")]
        public int FrequenciaCardiaca { get; set; }

        [Display(Name = "Data de aferição")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataAfericao { get; set; }
    }
}
