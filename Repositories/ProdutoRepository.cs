using api01.Context;
using api01.Models;

namespace api01.Repositories
{
    public class ProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context){
            _context = context;
        }

        public List<Produto> Listar(){
            return _context.Produtos.ToList();
        }
    }
}