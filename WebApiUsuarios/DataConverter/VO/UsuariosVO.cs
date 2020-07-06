using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Tapioca.HATEOAS;

namespace WebApiUsuarios.DataConverter.VO
{
    [DataContract]
    public class UsuariosVO : ISupportsHyperMedia
    {
        [DataMember(Order = 1)]
        public long? Id { get; set; }
        [DataMember(Order = 2)]
        public string GuidID { get; set; }
        [DataMember(Order = 3)]
        public string Nome { get; set; }
        [DataMember(Order = 4)]
        public string Sobrenome { get; set; }
        [DataMember(Order = 5)]
        public string CPF { get; set; }
        [DataMember(Order = 6)]
        public string Telefone { get; set; }
        [DataMember(Order = 7)]
        public string Email { get; set; }
        [DataMember(Order = 8)]
        public string Senha { get; set; }
        [DataMember(Order = 9)]
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
