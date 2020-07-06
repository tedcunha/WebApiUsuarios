using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiUsuarios.Business.Interface;
using WebApiUsuarios.DataConverter.VO;
using WebApiUsuarios.Model.Entidades;

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

        [HttpPost("CadastrarUsuarios")]
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
