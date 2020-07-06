using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.Model.Base;

namespace WebApiUsuarios.Repositorio.Generico.Interface
{
    public interface IRepository<T> where T : BaseEntity
    {
        List<T> Pesquisar();
        T PesquisarPorID(long Id);
        T Cadastrar(T item);
        T Alterar(T item);
        void Deletar(long Id);
    }
}
