using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Base.Response
{
    public interface IResponse<T>
    {
        public HttpStatusCode Code { get; set; }
        T Data { get; set; }
    }
}
