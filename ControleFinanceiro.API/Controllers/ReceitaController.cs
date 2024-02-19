using AutoMapper;
using ControleFinanceiro.API.Data;
using ControleFinanceiro.API.Data.Dtos;
using ControleFinanceiro.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReceitaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReceitaController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult> CadastrarReceita([FromBody] CreateReceitaDto dto)
        {
            //Validando os dados
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var receita = _mapper.Map<Receita>(dto);

            //Verificando se a receita ja foi cadastrada no mes
            if (_context.Receitas.Any(x => x.Descricao == receita.Descricao
            && x.Data.Year == receita.Data.Year
            && x.Data.Month == receita.Data.Month))
            {
                return BadRequest("Receita já cadastrada em menos de 30 dias encontrada.");
            }

            _context.Receitas.Add(receita);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(ListarReceitaPorId), new { id = receita.Id }, receita);
        }

        [HttpGet]
        public async Task<IEnumerable<ReceitaDto>> ListarReceitas([FromQuery] string descricao = null)
        {
            if(!string.IsNullOrEmpty(descricao))
            {
                return _mapper.Map<IEnumerable<ReceitaDto>>(_context.Receitas.Where(x => x.Descricao.ToLower().Contains(descricao.ToLower())));
            }
            return await _context.Receitas.Select(r => _mapper.Map<ReceitaDto>(r)).ToListAsync();
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<IEnumerable<ReceitaDto>> ListarReceitasPorMes(int ano, int mes)
        {
            if (ano < 1 || mes < 1 || mes > 12) throw new ArgumentException("Ano ou mês inválido.");

            DateTime dataInicio = new DateTime(1, mes, ano);
            DateTime dataFim = dataInicio.AddMonths(1).AddDays(-1);
            var receitas = await _context.Receitas.Where(r => r.Data >= dataInicio && r.Data <= dataFim).ToListAsync();

            return _mapper.Map<IEnumerable<ReceitaDto>>(receitas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarReceitaPorId(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null) return NotFound("Receita não encontrada.");
            var dto = _mapper.Map<ReceitaDto>(receita);
            return Ok(dto);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarReceita([FromBody] ReceitaDto dto, int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null) return NotFound("Receita não encontrada.");

            _mapper.Map(dto, receita);
            await _context.SaveChangesAsync();
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirReceita(int id)
        {
            var receita = await _context.Receitas.FindAsync(id);
            if (receita == null) return NotFound("Receita não encontrada.");

            _context.Receitas.Remove(receita);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
