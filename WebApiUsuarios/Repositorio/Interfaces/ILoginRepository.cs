using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.Model.Entidades;

namespace WebApiUsuarios.Repositorio.Interfaces
{
    public interface ILoginRepository
    {
        Usuarios PesquisarPorLogin(string login);
        Usuarios PesquisarPorEmail(string Email);
        Usuarios PesquisarPorCPF(string CPF);
    }
}
