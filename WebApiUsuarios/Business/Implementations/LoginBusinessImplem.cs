using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using WebApiUsuarios.Business.Interface;
using WebApiUsuarios.DataConverter.VO;
using WebApiUsuarios.Repositorio.Interfaces;
using WebApiUsuarios.Security.Configuration;

namespace WebApiUsuarios.Business.Implementations
{
    public class LoginBusinessImplem : ILoginBusiness
    {
        private readonly ILoginRepository _loginRepository;
        private readonly SigningConfigurations _signingConfigurations;
        private readonly TokenConfiguration _tokenConfiguration;

        public LoginBusinessImplem(ILoginRepository loginRepository,
                                   SigningConfigurations signingConfigurations,
                                   TokenConfiguration tokenConfiguration)
        {
            _loginRepository = loginRepository;
            _signingConfigurations = signingConfigurations;
            _tokenConfiguration = tokenConfiguration;
        }

        public object PesquisarPorEmail(LoginVO usuario)
        {
            bool credentialsIsValid = false;
            if (usuario != null && !String.IsNullOrWhiteSpace(usuario.Email))
            {
                var baseUser = _loginRepository.PesquisarPorEmail(usuario.Email);
                credentialsIsValid = (baseUser != null &&
                                      usuario.Email == baseUser.Email &&
                                      usuario.Senha == baseUser.Senha);
                if (credentialsIsValid)
                {
                    ClaimsIdentity identity = new ClaimsIdentity(
                        new GenericIdentity(usuario.Email, "Email"),
                        new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Email)
                        });

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfiguration.Seconds);

                    // Criando o Token
                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    // =============================

                    return SucessObject(createDate, expirationDate, token);
                }
                else
                {
                    return ExceptionObject();
                }
            }

            return ExceptionObject();
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = _tokenConfiguration.Issuer,
                Audience = _tokenConfiguration.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Autenticação Falhou."
            };
        }

        private object SucessObject(DateTime createDate, DateTime expiraTionDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expiraTionDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accesToken = token,
                message = "OK"
            };
        }
    }
}
