using System.ComponentModel.DataAnnotations;

namespace ControleFinanceiro.API.Data.Dtos
{
    public class CreateReceitaDto
    {
        [Required]
        [StringLength(300)]
        public string Descricao { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double Valor { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Data { get; set; }
    }
}
