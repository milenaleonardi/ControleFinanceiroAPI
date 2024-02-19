using AutoMapper;
using ControleFinanceiro.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResumoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ResumoController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{ano}/{mes}")]
        public async Task<IActionResult> ResumoDoMes(int ano, int mes)
        {
            if (ano < 1 || mes < 1 || mes > 12) throw new ArgumentException("Ano ou mês inválido.");

            DateTime dataInicio = new DateTime(1, mes, ano);
            DateTime dataFim = dataInicio.AddMonths(1).AddDays(-1);

            var despesas = await _context.Despesas.Where(d => d.Data >= dataInicio && d.Data <= dataFim).ToListAsync();
            var receitas = await _context.Receitas.Where(d => d.Data >= dataInicio && d.Data <= dataFim).ToListAsync();

            var totalDespesas = despesas.Sum(d => d.Valor);
            var totalReceiras = receitas.Sum(r => r.Valor);
            var saldoMes = totalReceiras - totalDespesas;

            var despesasPorCategoria = despesas.GroupBy(d => d.Categoria)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(d => d.Valor) })
                .ToDictionary(x => x.Categoria, x => x.Total);

            var resumoDoMes = new
            {
                TotalReceiras = totalReceiras,
                TotalDespesas = totalDespesas,
                SaldoMes = saldoMes,
                DespesasPorCategoria = despesasPorCategoria
            };

            return Ok(resumoDoMes);
        }
    }
}
