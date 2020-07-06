using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.DataConverter.Interfaces;
using WebApiUsuarios.DataConverter.VO;
using WebApiUsuarios.Model.Entidades;

namespace WebApiUsuarios.DataConverter.Converters
{
    public class UsuariosConverter : IParser<UsuariosVO, Usuarios>, IParser<Usuarios, UsuariosVO>
    {
        public UsuariosVO Parse(Usuarios origem)
        {
            if (origem == null) return null;
            return new UsuariosVO
            {
                Id = origem.Id,
                GuidID = origem.GuidID,
                Nome = origem.Nome,
                Sobrenome = origem.Sobrenome,
                CPF = origem.CPF,
                Telefone = origem.Telefone,
                Email = origem.Email,
                Senha = origem.Senha
            };
        }

        public Usuarios Parse(UsuariosVO origem)
        {
            if (origem == null) return null;
            return new Usuarios
            {
                Id = origem.Id,
                GuidID = origem.GuidID,
                Nome = origem.Nome,
                Sobrenome = origem.Sobrenome,
                CPF = origem.CPF,
                Telefone = origem.Telefone,
                Email = origem.Email,
                Senha = origem.Senha
            };
        }

        // Listas
        public List<UsuariosVO> ParseList(List<Usuarios> origem)
        {
            if (origem == null) return new List<UsuariosVO>();
            return origem.Select(item => Parse(item)).ToList();
        }

        public List<Usuarios> ParseList(List<UsuariosVO> origem)
        {
            if (origem == null) return new List<Usuarios>();
            return origem.Select(item => Parse(item)).ToList();
        }
    }
}
