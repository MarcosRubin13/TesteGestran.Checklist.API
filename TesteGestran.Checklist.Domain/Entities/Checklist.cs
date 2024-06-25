using AutoMapper;
using TesteGestran.Checklist.Domain.Checklist;

namespace TesteGestran.Checklist.Domain.Entities
{
    public class Checklist
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public bool Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<ChecklistItem> Itens { get; set; }
    }

    public static class ChecklistFactory
    {
        public static Checklist Create(ChecklistCommand command, IMapper mapper)
        {
            return new Checklist
            {
                Situacao = command.Situacao,
                Usuario = command.Usuario,
                DataCadastro = DateTime.UtcNow,
                Itens = command.Itens.Select(item => mapper.Map<ChecklistItem>(item)).ToList()
            };
        }
    }
}
