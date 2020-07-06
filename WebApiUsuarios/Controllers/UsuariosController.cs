using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApiUsuarios.Model.Entidades;

namespace WebApiUsuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {

        private readonly ILogger<UsuariosController> _logger;

        public UsuariosController(ILogger<UsuariosController> logger)
        {
            _logger = logger;
        }

        [HttpGet("PesquisarUsuarios")]
        public IActionResult PesquisarUsuarios()
        {
            return Ok("PesquisarUsuarios");
        }

        [HttpPost("CadastrarUsuarios")]
        public IActionResult CadastrarUsuarios([FromBody] Usuarios usuarios)
        {
            return Ok("CadastrarUsuarios");
        }

    }
}
