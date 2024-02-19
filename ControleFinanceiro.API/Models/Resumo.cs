namespace ControleFinanceiro.API.Models
{
    public class Resumo
    {
        public int Id { get; set; }
        public int ReceitaId { get; set; }
        public int DespesaId { get; set; }
        public Receita Receita { get; set; }
        public Despesa Despesa { get; set; }

    }
}
