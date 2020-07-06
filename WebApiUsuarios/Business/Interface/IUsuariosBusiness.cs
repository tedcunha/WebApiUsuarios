using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.DataConverter.VO;
using WebApiUsuarios.Model;

namespace WebApiUsuarios.Business.Interface
{
    public interface IUsuariosBusiness
    {
        List<UsuariosVO> Pesquisar();
        UsuariosVO PesquisarPorID(long Id);
        Menssagem Cadastrar(UsuariosVO usuarios);
        Menssagem Alterar(UsuariosVO usuarios);
        Menssagem Deletar(long Id);
    }
}
