using System;
using System.Collections.Generic;
using Tapioca.HATEOAS.Utils;
using WebApiUsuarios.Business.Interface;
using WebApiUsuarios.DataConverter.Converters;
using WebApiUsuarios.DataConverter.VO;
using WebApiUsuarios.Model;
using WebApiUsuarios.Repositorio.Interfaces;

namespace WebApiUsuarios.Business.Implementations
{
    public class UsuariosBusinessImpl : IUsuariosBusiness
    {
        private readonly IUsuariosRepository _repository;
        private readonly UsuariosConverter _converter;

        public UsuariosBusinessImpl(IUsuariosRepository repository)
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

        public List<UsuariosVO> PesquisarPorNomeSobrenome(string firstname, string lastname)
        {
            return _converter.ParseList(_repository.PesquisarPorNomeSobrenome(firstname, lastname));
        }

        public PagedSearchDTO<UsuariosVO> PesquisardComPaginacao(string name, string sortDirection, int pageSize, int page)
        {
            page = page > 0 ? page - 1 : 0;

            // Select da Paginação
            string query = "Select * From usuarios p ";
            query += "Where 1 = 1 ";
            if (!string.IsNullOrEmpty(name))
            {
                query += $"and p.Nome like '%{name}%' ";
            }
            query += $"Order By p.Nome {sortDirection} limit {pageSize} offset {page}";
            var pessoa = _converter.ParseList(_repository.PesquisardComPaginacao(query));

            // Pegando o Total de Registros
            string countQuery = "Select Count(*) as Qtde From usuarios p Where 1 = 1 ";
            if (!string.IsNullOrEmpty(name))
            {
                countQuery += $"and p.Nome like '%{name}%'";
            }
            var totalRegistros = _repository.RetornaQtdeRegistros(countQuery);

            return new PagedSearchDTO<UsuariosVO>
            {
                CurrentPage = page + 1,
                List = pessoa,
                PageSize = pageSize,
                SortDirections = sortDirection,
                TotalResults = totalRegistros
            };
        }
    }
}
