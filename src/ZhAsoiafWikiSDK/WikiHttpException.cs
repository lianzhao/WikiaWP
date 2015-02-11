using System;
using System.Net;
using System.Net.Http;

namespace ZhAsoiafWiki
{
    public class WikiHttpException : Exception
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
    }
}