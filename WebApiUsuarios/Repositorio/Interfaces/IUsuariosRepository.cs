using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.Model.Entidades;
using WebApiUsuarios.Repositorio.Generico.Interface;

namespace WebApiUsuarios.Repositorio.Interfaces
{
    public interface IUsuariosRepository : IRepository<Usuarios>
    {
        List<Usuarios> PesquisarPorNomeSobrenome(string firstname, string lastname);
    }
}
