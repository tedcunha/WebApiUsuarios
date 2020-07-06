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
    public class LoginController : ControllerBase
    {
        private readonly ILogger<UsuariosController> _logger;
        private readonly ILoginBusiness _loginBusiness;

        public LoginController(ILogger<UsuariosController> logger,
                               ILoginBusiness loginBusiness)
        {
            _logger = logger;
            _loginBusiness = loginBusiness;
        }

        [AllowAnonymous]
        [HttpPost("AcessoLogin")]
        public object AcessoLogin([FromBody] LoginVO login)
        {
            if (login == null)
            {
                return BadRequest();
            }
            return _loginBusiness.PesquisarPorEmail(login);
        }
    }
}
