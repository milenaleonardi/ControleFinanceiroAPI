using ControleFinanceiro.API.Validation;
using System.Text.RegularExpressions;

namespace ControleFinanceiro.API.Models
{
    public class Receita
    {
        public Receita(string descricao, double valor, DateTime data)
        {
            ValidateDomain(descricao, valor, data);
        }

        public Receita(int id, string descricao, double valor, DateTime data)
        {
            DomainExceptionValidation.When(id < 0,
                "O Id do cliente deve ser positivo.");
            Id = id;
            ValidateDomain(descricao, valor, data);
        }

        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Valor{ get; set;}
        public DateTime Data {  get; set; }

        public void Update(string descricao, double valor, DateTime data)
        {
            ValidateDomain(descricao, valor, data);
        }

        public void ValidateDomain(string descricao, double valor, DateTime data)
        {
            DomainExceptionValidation.When(descricao.Length > 300, "A decricao deve conter no máximo 300 caracteres.");

            //Regex para valor monetário
            Regex valorRegex = new Regex(@"^R\$(\d{1,3}(\.\d{3})*|\d+)(,\d{2})?$");
            DomainExceptionValidation.When(!valorRegex.IsMatch(valor.ToString()), "O valor deve estar no formato moeda.");

            //Regex para data válida no formato dd/mm/yyy
            Regex dataRegex = new Regex(@"^(0?[1-9]|[12][0-9]|3[01])\/(0?[1-9]|1[012])\/\d{4}$");
            DomainExceptionValidation.When(!dataRegex.IsMatch(data.ToString()), "A data deve estar no formato dd/MM/yyyy.");

           
            Descricao = descricao;
            Valor = valor;
            Data = data;
        }
    }
}
