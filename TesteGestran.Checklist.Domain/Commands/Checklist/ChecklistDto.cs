namespace TesteGestran.Checklist.Domain.Checklist
{
    public class ChecklistDto
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Etapa { get; set; }
        public bool? Situacao { get; set; }
        public DateTime DataCadastro { get; set; }
        public List<ChecklistItemDto> Itens { get; set; }
    }
}