using AutoMapper;
using TesteGestran.Checklist.Domain.Checklist;
using TesteGestran.Checklist.Domain.Enums;

namespace TesteGestran.Checklist.Domain.Entities
{
    public class Checklist
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Tipo { get; set; }
        public string Placa { get; set; }
        public string Motorista { get; set; }
        public string Etapa { get; set; }
        public bool? Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public List<ChecklistItem>? Itens { get; set; }
    }

    public static class ChecklistFactory
    {
        public static Checklist Create(ChecklistCreateCommand command)
        {
            return new Checklist
            {
                Etapa = "ABERTO",
                Tipo = command.Tipo,
                Motorista = command.Motorista,
                Placa = command.Placa,
                Situacao = null,
                Usuario = command.Usuario,
                DataCadastro = DateTime.UtcNow,
                DataFinalizacao = null,
                Itens = Enum.GetValues(typeof(ChecklistItemEnum))
                            .Cast<ChecklistItemEnum>()
                            .Select(enumItem => new ChecklistItem
                            {
                                Verificado = false, 
                                Observacao = "", 
                                Item = enumItem.ToString()
                            }).ToList()
            };
        }
    }
}
