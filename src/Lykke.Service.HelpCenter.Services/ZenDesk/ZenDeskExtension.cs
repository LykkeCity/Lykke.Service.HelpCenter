using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.HelpCenter.Core.Domain;
using Lykke.Service.HelpCenter.Services.ZenDesk.Common;
using Refit;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    [DebuggerStepThrough]
    public static class ZenDeskExtensions
    {
        public static async Task<ResponseModel> TryExecute<TApi>(this TApi service, ILog log, Func<TApi, Task> action, Action<ResponseModel> onError = null)
        {
            try
            {
                var task = action(service);
                await task;
                return new ResponseModel {StatusCode = HttpStatusCode.OK};
            }
            catch (ApiException ex)
            {
                return ToErrorResponse(ex, new ResponseModel(), onError);
            }
        }

        public static async Task<ResponseModel<TResult>> TryExecute<TApi,TResult>(this TApi service, ILog log, Func<TApi, Task<TResult>> action, Action<ResponseModel<TResult>> onError = null)
        {
            try
            {
                var task = action(service);
                var result = await task;
                return new ResponseModel<TResult>
                {
                    StatusCode = HttpStatusCode.OK,
                    Result = result
                };
            }
            catch (ApiException ex)
            {
                return ToErrorResponse(ex, new ResponseModel<TResult>(), onError);
            }
        }

        private static T ToErrorResponse<T>(ApiException ex, T response, Action<T> onError)
            where T : ResponseModel
        {
            var error = ex.HasContent
                ? ex.GetContentAs<ZenDeskError>()
                : new ZenDeskError { Error = ex.ReasonPhrase };
            response.StatusCode = ex.StatusCode;
            response.Error = error.ToString();

            onError?.Invoke(response);

            return response;
        }
    }
}
