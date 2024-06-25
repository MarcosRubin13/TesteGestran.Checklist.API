
namespace TesteGestran.Checklist.Domain.Checklist
{
    public class ChecklistItemCommand
    {
        public bool Situacao { get; set; }
        public string Observacao { get; set; }
        public int ChecklistId { get; set; }
    }
}