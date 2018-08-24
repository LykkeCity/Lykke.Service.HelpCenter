using System;
using System.Net;
using System.Threading.Tasks;
using Common.Log;
using Lykke.Service.HelpCenter.Core.Domain;
using Lykke.Service.HelpCenter.Services.ZenDesk.Common;
using Refit;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    public static class ZenDeskExtensions
    {
        public static async Task<ResponseModel> TryExecute<TApi>(this TApi service, ILog log, Func<TApi, Task> action)
        {
            try
            {
                var task = action(service);
                await task;
                return new ResponseModel {StatusCode = HttpStatusCode.OK};
            }
            catch (ApiException ex)
            {
                return ToErrorResponse(ex, new ResponseModel());
            }
        }

        public static async Task<ResponseModel<TResult>> TryExecute<TApi,TResult>(this TApi service, ILog log, Func<TApi, Task<TResult>> action)
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
                return ToErrorResponse(ex, new ResponseModel<TResult>());
            }
        }

        private static T ToErrorResponse<T>(ApiException ex, T response)
            where T : ResponseModel
        {
            var error = ex.HasContent
                ? ex.GetContentAs<ZenDeskError>()
                : new ZenDeskError { Error = ex.ReasonPhrase };
            response.StatusCode = ex.StatusCode;
            response.Error = error.Error;

            return response;
        }
    }
}
