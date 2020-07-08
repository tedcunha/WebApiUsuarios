using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tapioca.HATEOAS;
using WebApiUsuarios.Business.Interface;
using WebApiUsuarios.DataConverter.VO;

namespace WebApiUsuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly IUsuariosBusiness _usuariosBusiness;

        public UsuariosController(ILogger<UsuariosController> logger,
                                  IUsuariosBusiness usuariosBusiness)
        {
            _logger = logger;
            _usuariosBusiness = usuariosBusiness;
        }

        [HttpGet("PesquisarUsuarios")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult PesquisarUsuarios()
        {
            var retorno = _usuariosBusiness.Pesquisar();
            if (retorno == null)
            {
                return NotFound();
            }
            return Ok(retorno);
        }

        [HttpGet("PesquisarUsuariosPorID/{Id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult PesquisarUsuariosPorID(long Id)
        {
            try
            {
                var retorno = _usuariosBusiness.PesquisarPorID(Id);
                if (retorno == null)
                {
                    return NotFound();
                }
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("PesquisarPorNomeSobrenome")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult PesquisarPorNomeSobrenome([FromQuery] string firstname, [FromQuery] string lastname)
        {
            return Ok(_usuariosBusiness.PesquisarPorNomeSobrenome(firstname, lastname));
        }

        [HttpGet("PesquisaPaginada/{sortDirection}/{pageSize}/{page}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult PesquisaPaginada([FromQuery] string name, string sortDirection, int pageSize, int page)
        {
            return new ObjectResult(_usuariosBusiness.PesquisardComPaginacao(name, sortDirection, pageSize, page));
        }

        [HttpPost("CadastrarUsuarios")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult CadastrarUsuarios([FromBody] UsuariosVO usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return BadRequest();
                }
                return Ok(_usuariosBusiness.Cadastrar(usuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpPut("AlterarUsuarios")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult AlterarUsuarios([FromBody] UsuariosVO usuarios)
        {
            try
            {
                if (usuarios == null)
                {
                    return BadRequest();
                }
                return Ok(_usuariosBusiness.Alterar(usuarios));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpDelete("DeletarUsuarios/{Id}")]
        [Authorize("Bearer")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult DeletarUsuarios(long Id)
        {
            try
            {
                return Ok(_usuariosBusiness.Deletar(Id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}
