namespace ControleFinanceiro.API.Data.Dtos
{
    public class DespesaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public string Categoria { get; set; }
    }
}
