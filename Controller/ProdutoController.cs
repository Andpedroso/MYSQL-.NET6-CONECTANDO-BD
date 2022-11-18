using Microsoft.AspNetCore.Mvc;
using api01.Repositories;

namespace api01.Controller
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoRepository _produtoRepository;

        public ProdutoController(ProdutoRepository produtoRepository){
            _produtoRepository = produtoRepository;
        }

        [HttpGet]
        public IActionResult Listar(){
            try {
                return Ok(_produtoRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}