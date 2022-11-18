using Microsoft.EntityFrameworkCore;
using api01.Models;

namespace api01.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Produto> Produtos { get; set; }
    }
}