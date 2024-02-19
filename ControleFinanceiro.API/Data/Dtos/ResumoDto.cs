namespace ControleFinanceiro.API.Data.Dtos
{
    public class ResumoDto
    {
        public int Id { get; set; }
        public int ReceitaId { get; set; }
        public int DespesaId { get; set; }
        public ReceitaDto ReceitaDto { get; set; }
        public DespesaDto DespesaDto { get; set; }
    }
}
