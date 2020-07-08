using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.Model.Context;
using WebApiUsuarios.Model.Entidades;
using WebApiUsuarios.Repositorio.Generico;
using WebApiUsuarios.Repositorio.Interfaces;

namespace WebApiUsuarios.Repositorio.Implementations
{
    public class UsuariosRepositoryImpl : GenericRepository<Usuarios>, IUsuariosRepository
    {
        public UsuariosRepositoryImpl(MySqlContext context) : base(context) 
        {
        }

        public List<Usuarios> PesquisarPorNomeSobrenome(string firstname, string lastname)
        {
            if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                return _context.usuarios.Where(p => p.Nome.Contains(firstname) && p.Sobrenome.Contains(lastname)).ToList();
            }
            else if (string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname))
            {
                return _context.usuarios.Where(p => p.Sobrenome.Contains(lastname)).ToList();
            }
            else if (!string.IsNullOrEmpty(firstname) && string.IsNullOrEmpty(lastname))
            {
                return _context.usuarios.Where(p => p.Nome.Contains(firstname)).ToList();
            }
            else
            {
                return _context.usuarios.ToList();
            }
        }
    }
}
