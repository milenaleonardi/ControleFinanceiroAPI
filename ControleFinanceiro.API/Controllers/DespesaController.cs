using AutoMapper;
using ControleFinanceiro.API.Data.Dtos;
using ControleFinanceiro.API.Data;
using ControleFinanceiro.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DespesaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public DespesaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarDespesa([FromBody] CreateDespesaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var despesa = _mapper.Map<Despesa>(dto);

            if (_context.Despesas.Any(x => x.Descricao == despesa.Descricao
            && x.Data.Year == despesa.Data.Year
            && x.Data.Month == despesa.Data.Month))
            {
                return BadRequest("Despesa já cadastrada em menos de 30 dias encontrada.");
            }
            if (string.IsNullOrEmpty(despesa.Categoria))
            {
                dto.Categoria = "Outras";
            } else
            {
                if (!Despesa.ValidarCategoria(despesa.Categoria))
                {
                    return BadRequest("Categoria inválida.");
                }
            }
            _context.Despesas.Add(despesa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarDespesaPorId), new { id = despesa.Id }, despesa);
        }

        [HttpGet]
        public async Task<IEnumerable<DespesaDto>> ListarDespesas()
        {
            return await _context.Despesas.Select(d => _mapper.Map<DespesaDto>(d)).ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarDespesaPorId(int id)
        {
            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null) return NotFound("Despesa não encontrada.");
            var dto = _mapper.Map<DespesaDto>(despesa);
            return Ok(dto);

        }

        [HttpGet("{ano}/{mes}")]
        public async Task<IEnumerable<DespesaDto>> ListarDespesasPorMes(int ano, int mes){

            if (ano < 1 || mes < 1 || mes > 12) throw new ArgumentException("Ano ou mes inválido.");
            DateTime dataInicio = new DateTime(1, mes, ano);
            DateTime dataFim = dataInicio.AddMonths(1).AddDays(-1);

            var despesas = await _context.Despesas.Where(d => d.Data >= dataInicio && d.Data <= dataFim).ToListAsync();
            return _mapper.Map<IEnumerable<DespesaDto>>(despesas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarDespesa([FromBody] DespesaDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null) return NotFound("Despesa não encontrada.");

            _mapper.Map(dto, despesa);
            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirDespesa(int id)
        {
            var despesa = await _context.Despesas.FindAsync(id);
            if (despesa == null) return NotFound("Despesa não encontrada.");

            _context.Despesas.Remove(despesa);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
