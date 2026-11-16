using System;

namespace MauiAppEventos.Models
{
    public class Evento
    {
        public string Nome { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; } = DateTime.Today;
        public DateTime DataTermino { get; set; } = DateTime.Today.AddDays(1);
        public int NumeroParticipantes { get; set; }
        public string Local { get; set; } = string.Empty;
        public decimal CustoPorParticipante { get; set; }

        public TimeSpan DiferencaDatas => DataTermino.Date - DataInicio.Date;

        public int DuracaoDias => DiferencaDatas.Days == 0 ? 1 : DiferencaDatas.Days;

        public string ResumoTimeSpan => $"{DiferencaDatas.Days} dia(s) de intervalo ({DuracaoDias} dia(s) de evento)";

        public decimal CustoTotal => NumeroParticipantes * CustoPorParticipante;
    }
}
