using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Base.Response
{
    public abstract class BaseResponse
    {
        public string Message { get; set; }
        public bool Error { get; set; }
        public HttpStatusCode Code { get; set; }

    }
    public class Response<T> : BaseResponse, IResponse<T>
    {
        public T Data { get; set; }

        public void AddMessage(string message)
        {
            Message += Code == 0 ? message : $", {message}";
        }

        public static Response<T> CreateResponse(bool error)
        {
            return new Response<T>
            {
                Error = error,
                Message = null
            };
        }

        public static Response<T> CreateSuccessResponse(T data, HttpStatusCode code, string message)
        {
            return new Response<T>
            {
                Code = code,
                Data = data,
                Error = false,
                Message = message
            };
        }


        public static Response<T> CreateFailResponse(Exception e, HttpStatusCode code, string message)
        {
            return new Response<T>
            {
                Message = $"message : {message} Exception: {e}",
                Code = code,
                Error = true
            };
        }

    }
}
