namespace TesteGestran.Checklist.Domain.Checklist
{
    public class ChecklistItemDto
    {
        public int Id { get; set; }
        public bool Verificado { get; set; }
        public string Observacao { get; set; }
        public string Item { get; set; }
    }
}