namespace ControleFinanceiro.API.Data.Dtos
{
    public class ReceitaDto
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
    }
}
