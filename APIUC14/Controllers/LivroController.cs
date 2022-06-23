using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIUC14.Models;
using APIUC14.Repositories;

namespace APIUC14.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]

    [Authorize(Roles = "1")]
    public class LivroController : ControllerBase
    {
        private readonly LivroRepository _LivroRepository;

        public LivroController(LivroRepository LivroRepository)
        {
            _LivroRepository = LivroRepository;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                return Ok(_LivroRepository.Listar());
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpGet("{id}")]

        public IActionResult BuscarPorId(int id)
        {
            try
            {
                Livro livro = _LivroRepository.BuscarPorId(id);
                
                if (livro == null)
                {
                    return NotFound();
                }

                return Ok(livro);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]

        public IActionResult Cadastrar(Livro livro)
        {
            try
            {
               _LivroRepository.Cadastrar(livro);

                return StatusCode(201);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, Livro livro)
        {
            try
            {
                _LivroRepository.Atualizar(id, livro);

                return StatusCode(204);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _LivroRepository.Deletar(id);

                return StatusCode(200);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
