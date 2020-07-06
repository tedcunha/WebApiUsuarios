using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiUsuarios.DataConverter.Interfaces
{
    public interface IParser<D,O>
    {
        D Parse(O origem);
        List<D> ParseList(List<O> origem);
    }
}
