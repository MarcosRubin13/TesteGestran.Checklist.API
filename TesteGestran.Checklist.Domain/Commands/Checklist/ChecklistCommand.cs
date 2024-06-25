namespace TesteGestran.Checklist.Domain.Checklist
{
    public class ChecklistCommand
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public bool Situacao { get; set; }
        public List<ChecklistItemCommand> Itens { get; set; }
    }
}