using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.Business.Interface;
using WebApiUsuarios.DataConverter.Converters;
using WebApiUsuarios.DataConverter.VO;
using WebApiUsuarios.Model;
using WebApiUsuarios.Model.Entidades;
using WebApiUsuarios.Repositorio.Generico.Interface;

namespace WebApiUsuarios.Business.Implementations
{
    public class UsuariosBusinessImpl : IUsuariosBusiness
    {
        private readonly IRepository<Usuarios> _repository;
        private readonly UsuariosConverter _converter;

        public UsuariosBusinessImpl(IRepository<Usuarios> repository)
        {
            _repository = repository;
            _converter = new UsuariosConverter();
        }

        public Menssagem Alterar(UsuariosVO usuarios)
        {
            try
            {
                var usuariosEntity = _converter.Parse(usuarios);
                usuariosEntity = _repository.Alterar(usuariosEntity);
                return new Menssagem
                {
                    Status = "200",
                    Descricao = "Filme alterado com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new Menssagem
                {
                    Status = "404",
                    Descricao = ex.Message.ToString()
                };
            }
        }

        public Menssagem Cadastrar(UsuariosVO usuarios)
        {
            try
            {
                usuarios.GuidID = Guid.NewGuid().ToString();
                var usuariosEntity = _converter.Parse(usuarios);
                usuariosEntity = _repository.Cadastrar(usuariosEntity);
                return new Menssagem
                {
                    Status = "200",
                    Descricao = "Filme cadastrado com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new Menssagem
                {
                    Status = "404",
                    Descricao = ex.Message.ToString()
                };
            }
        }

        public Menssagem Deletar(long Id)
        {
            try
            {
                _repository.Deletar(Id);
                return new Menssagem
                {
                    Status = "200",
                    Descricao = "Filme excluido com sucesso."
                };
            }
            catch (Exception ex)
            {
                return new Menssagem
                {
                    Status = "404",
                    Descricao = ex.Message.ToString()
                };
            }
        }

        public List<UsuariosVO> Pesquisar()
        {
            return _converter.ParseList(_repository.Pesquisar());
        }

        public UsuariosVO PesquisarPorID(long Id)
        {
            return _converter.Parse(_repository.PesquisarPorID(Id));
        }
    }
}
