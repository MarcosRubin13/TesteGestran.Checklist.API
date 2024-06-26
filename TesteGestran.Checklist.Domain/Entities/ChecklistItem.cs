using TesteGestran.Checklist.Domain.Checklist;

namespace TesteGestran.Checklist.Domain.Entities
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public bool Verificado { get; set; }
        public string Observacao { get; set; }
        public int ChecklistId { get; set; }
        public Checklist Checklist { get; set; }
        public string Item { get; set; }
    }

    public static class ChecklistItemFactory
    {
        public static ChecklistItem Create(ChecklistItemCommand command)
        {
            return new ChecklistItem
            {
                Verificado = command.Verificado,
                Observacao = command.Observacao,
                ChecklistId = command.ChecklistId,
                Item = command.Item
            };
        }
    }
}