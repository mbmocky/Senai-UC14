using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using APIUC14.Interfaces;
using APIUC14.Models;

namespace APIUC14.Controllers
{
        [Produces("application/json")]

        [Route("api/[controller]")]

        [ApiController]
        public class UsuariosController : ControllerBase
        {
            private readonly IUsuarioRepository _iUsuarioRepository;

            public UsuariosController(IUsuarioRepository usuarioRepository)
            {
                _iUsuarioRepository = usuarioRepository;
            }

            [HttpGet]
            public IActionResult Listar()
            {
                try
                {
                    return Ok(_iUsuarioRepository.Listar());
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
                    Usuario usuarioEncontrado = _iUsuarioRepository.BuscarPorId(id);

                    if (usuarioEncontrado == null)
                    {
                        return NotFound();
                    }

                    return Ok(usuarioEncontrado);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [HttpPost]

            public IActionResult Cadastrar(Usuario usuario)
            {
                try
                {
                    _iUsuarioRepository.Cadastrar(usuario);

                    return StatusCode(201);
                }
                catch (Exception)
                {
                    throw;
                }
            }

            [HttpPut("{id}")]

            public IActionResult Alterar(int id, Usuario usuario)
            {
                try
                {
                    _iUsuarioRepository.Atualizar(id, usuario);

                    return Ok("Usuário Alterado");
                }
                catch (Exception)
                {

                    throw;
                }
            }

            /// <summary>
            /// exemplo de uso do summary
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpDelete("{id}")]
            public IActionResult Deletar(int id)
            {
                try
                {
                    _iUsuarioRepository.Deletar(id);

                    return Ok("Usuário Apagado");
                }
                catch (Exception)
                {

                    throw;
                }
            }


        }
}