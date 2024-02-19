using ControleFinanceiro.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleFinanceiro.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Receita> Receitas {  get; set; }
        public DbSet<Despesa> Despesas { get; set; }
    }
}
