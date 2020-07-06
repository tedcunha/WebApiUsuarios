using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tapioca.HATEOAS;
using WebApiUsuarios.DataConverter.VO;

namespace WebApiUsuarios.HyperMidia
{
    public class UsuariosEnricher : ObjectContentResponseEnricher<UsuariosVO>
    {
        private readonly object _lock = new object();
        protected override Task EnrichModel(UsuariosVO content, IUrlHelper urlHelper)
        {
            var path = "Usuarios/PesquisarUsuarios";
            string link = getLink(urlHelper, path);
            string linkWithId = getLink(content, urlHelper, path);

            // GET Todos
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            // GET Por ID
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.GET,
                Href = linkWithId,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultGet
            });
            // POST
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.POST,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            // PUT
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.PUT,
                Href = link,
                Rel = RelationType.self,
                Type = ResponseTypeFormat.DefaultPost
            });
            // DELETE
            content.Links.Add(new HyperMediaLink()
            {
                Action = HttpActionVerb.DELETE,
                Href = linkWithId,
                Rel = RelationType.self,
                Type = "int",
            });
            return null;
        }

        private string getLink(IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).ToString();
            }
        }

        private string getLink(UsuariosVO content, IUrlHelper urlHelper, string path)
        {
            lock (_lock)
            {
                var url = new { controller = path, id = content.Id };
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).ToString();
            }
        }
    }
}
