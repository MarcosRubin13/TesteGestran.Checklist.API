namespace TesteGestran.Checklist.Domain.Checklist
{
    public class VerificarItemChecklistCommand
    {
        public int ChecklistId { get; set; }
        public int ItemId { get; set; }
        public string Observacao { get; set; }
    }
}