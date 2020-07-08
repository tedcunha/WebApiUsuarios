using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tapioca.HATEOAS.Utils;
using WebApiUsuarios.DataConverter.VO;
using WebApiUsuarios.Model;
using WebApiUsuarios.Model.Entidades;

namespace WebApiUsuarios.Business.Interface
{
    public interface IUsuariosBusiness
    {
        List<UsuariosVO> Pesquisar();
        UsuariosVO PesquisarPorID(long Id);
        List<UsuariosVO> PesquisarPorNomeSobrenome(string firstname, string lastname);
        Menssagem Cadastrar(UsuariosVO usuarios);
        Menssagem Alterar(UsuariosVO usuarios);
        Menssagem Deletar(long Id);
        PagedSearchDTO<UsuariosVO> PesquisardComPaginacao(string name, string sortDirection, int pageSize, int page);
    }
}
