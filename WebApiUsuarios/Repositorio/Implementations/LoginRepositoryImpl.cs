using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.Model.Context;
using WebApiUsuarios.Model.Entidades;
using WebApiUsuarios.Repositorio.Interfaces;

namespace WebApiUsuarios.Repositorio.Implementations
{
    public class LoginRepositoryImpl : ILoginRepository
    {
        private readonly MySqlContext _context;

        public LoginRepositoryImpl(MySqlContext context)
        {
            _context = context;
        }

        public Usuarios PesquisarPorCPF(string cpf)
        {
            return _context.usuarios.SingleOrDefault(u => u.CPF == cpf);
        }

        public Usuarios PesquisarPorEmail(string email)
        {
            return _context.usuarios.SingleOrDefault(u => u.Email == email);
        }

        public Usuarios PesquisarPorLogin(string login)
        {
            return _context.usuarios.SingleOrDefault(u => u.Nome == login);
        }
    }
}
