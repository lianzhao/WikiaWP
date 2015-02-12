using System;
using System.Net;
using System.Net.Http;

using LianZhao.Patterns.Func;

using Void = LianZhao.Void;

namespace Wikia
{
    public class WikiHttpException : Exception, IFunc<Void, HttpStatusCode>
    {
        public WikiHttpException(HttpStatusCode statusCode)
            : this(new HttpResponseMessage(statusCode))
        {
        }

        public WikiHttpException(HttpResponseMessage response)
        {
            Response = response;
        }

        public HttpResponseMessage Response { get; private set; }

        HttpStatusCode IFunc<Void, HttpStatusCode>.Invoke(Void @from)
        {
            return Response.StatusCode;
        }
    }
}