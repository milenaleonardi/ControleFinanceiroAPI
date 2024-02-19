using ControleFinanceiro.API.Validation;
using System.Text.RegularExpressions;

namespace ControleFinanceiro.API.Models
{
    public class Despesa
    {
        public Despesa(string descricao, double valor, DateTime data, string categoria)
        {
            ValidateDomain(descricao, valor, data, categoria);
        }

        public Despesa(int id, string descricao, double valor, DateTime data, string categoria)
        {
            DomainExceptionValidation.When(id < 0,
                "O Id do cliente deve ser positivo.");
            Id = id;
            ValidateDomain(descricao, valor, data, categoria);
            
        }

        public int Id { get; private set; }
        public string Descricao { get; private set; }
        public double Valor { get; private set; }
        public DateTime Data { get; private set; }
        public string Categoria { get; private set; }

        internal static bool ValidarCategoria(string categoria)
        {
            string[] categorias = { "Alimentação", "Saúde", "Moradia", "Transporte", "Educação", "Lazer", "Imprevistos", "Outras" };
            return categorias.Contains(categoria);
        }

        public void Update(string descricao, double valor, DateTime data, string categoria)
        {
            ValidateDomain(descricao, valor, data, categoria);
        }

        public void ValidateDomain(string descricao, double valor, DateTime data, string categoria)
        {
            DomainExceptionValidation.When(descricao.Length > 300, "A decricao deve conter no máximo 300 caracteres.");

            //Regex para valor monetário
            Regex valorRegex = new Regex(@"^R\$(\d{1,3}(\.\d{3})*|\d+)(,\d{2})?$");
            DomainExceptionValidation.When(!valorRegex.IsMatch(valor.ToString()), "O valor deve estar no formato moeda.");

            //Regex para data válida no formato dd/mm/yyy
            Regex dataRegex = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])\/(0?[1-9]|1[012])\/\d{4}$");
            DomainExceptionValidation.When(!dataRegex.IsMatch(data.ToString()), "A data deve estar no formato dd/MM/yyyy.");

            DomainExceptionValidation.When(!ValidarCategoria(categoria), "A categoria deve ser válida.");

            Descricao = descricao;
            Valor = valor;
            Data = data;
            Categoria = categoria;
        }
    }
}
