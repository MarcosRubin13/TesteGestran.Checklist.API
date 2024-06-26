using System.Text.Json.Serialization;

namespace TesteGestran.Checklist.Domain.Checklist
{
    public class ChecklistCreateCommand
    {
        public string Usuario { get; set; }
        public string Tipo { get; set; }
        public string Placa { get; set; }
        public string Motorista { get; set; }
    }
}