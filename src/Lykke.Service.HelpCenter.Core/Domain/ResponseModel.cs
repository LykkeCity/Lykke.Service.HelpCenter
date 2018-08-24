using System.Net;

namespace Lykke.Service.HelpCenter.Core.Domain
{
    public class ResponseModel
    {
        public string Error { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool Success => (int)StatusCode >= 200 && (int)StatusCode < 300;
    }

    public class ResponseModel<T> : ResponseModel
    {
        public T Result { get; set; }
    }
}
