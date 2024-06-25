using System.Text;
using TesteGestran.Checklist.Domain.Checklist;

namespace TesteGestran.Checklist.Domain.Entities
{
    public class ChecklistItem
    {
        public int Id { get; set; }
        public bool Situacao { get; set; }
        public string Observacao { get; set; }
        public int ChecklistId { get; set; }
        public Checklist Checklist { get; set; }
    }

    public static class ChecklistItemFactory
    {
        public static ChecklistItem Create(ChecklistItemCommand command)
        {
            return new ChecklistItem
            {
                Situacao = command.Situacao,
                Observacao = command.Observacao,
                ChecklistId = command.ChecklistId,
            };
        }
    }
}