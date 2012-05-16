using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy.ErrorHandling;
using Nancy;

namespace NancyBlog
{
    /// <summary>
    /// Cuz we like our errors to blow up in royal yellowish goodness.
    /// </summary>
    public class YsodErrorHandler : IErrorHandler
    {
        public bool HandlesStatusCode(HttpStatusCode statusCode, NancyContext context)
        {
            return statusCode == HttpStatusCode.InternalServerError;
        }

        public void Handle(HttpStatusCode statusCode, NancyContext context)
        {
            object errorObject;
            context.Items.TryGetValue(NancyEngine.ERROR_EXCEPTION, out errorObject);
            var exception = errorObject as Exception;

            if (exception != null)
            {
                throw exception;
            }
        }
    }
}