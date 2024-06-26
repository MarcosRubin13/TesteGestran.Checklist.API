using System.Text.Json.Serialization;

namespace TesteGestran.Checklist.Domain.Checklist
{
    public class ChecklistUpdateStatusCommand
    {
        public int Id { get; set; }
        public bool Situacao { get; set; }
        public string Etapa { get; set; }
    }
}