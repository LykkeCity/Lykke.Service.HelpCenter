using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Lykke.Service.HelpCenter.Core.Domain
{
    public class ResponseModel
    {
        public string Error { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public bool Success => (int)StatusCode >= 200 && (int)StatusCode < 300;

        public virtual IActionResult ToActionResult()
        {
            if (string.IsNullOrWhiteSpace(Error))
            {
                return new StatusCodeResult((int)StatusCode);
            }

            return new ObjectResult(new { Error })
            {
                StatusCode = (int)StatusCode
            };
        }
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Result { get; set; }

        public override IActionResult ToActionResult()
            => ToActionResult(default);

        public IActionResult ToActionResult(Func<T, object> transform)
        {
            if (Success)
            {
                var response = transform?.Invoke(Result) ?? Result;
                return new ObjectResult(response)
                {
                    StatusCode = (int)StatusCode
                };
            }

            if (string.IsNullOrWhiteSpace(Error))
            {
                return new StatusCodeResult((int)StatusCode);
            }

            return new ObjectResult(new { Error })
            {
                StatusCode = (int)StatusCode
            };
        }
    }
}
