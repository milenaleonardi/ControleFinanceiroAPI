using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace ControleFinanceiro.API.Data.Dtos
{
    public class CreateDespesaDto
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
        public string Categoria {  get; set; }
    }
}
