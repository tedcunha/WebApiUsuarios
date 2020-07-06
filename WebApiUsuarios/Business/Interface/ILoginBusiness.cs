using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiUsuarios.DataConverter.VO;

namespace WebApiUsuarios.Business.Interface
{
    public interface ILoginBusiness
    {
        object PesquisarPorEmail(LoginVO usuario);
    }
}
