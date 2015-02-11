using System;
using System.Net;
using System.Net.Http;

namespace Wikia
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