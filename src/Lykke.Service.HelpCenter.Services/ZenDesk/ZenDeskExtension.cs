using System;
using System.Net;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Lykke.Service.HelpCenter.Core.Domain;
using Lykke.Service.HelpCenter.Services.ZenDesk.Common;
using Refit;

namespace Lykke.Service.HelpCenter.Services.ZenDesk
{
    [UsedImplicitly]
    public static class ZenDeskExtensions
    {
        public static async Task<ResponseModel> TryExecute<TApi>(this TApi service, Func<TApi, Task> action, Action<ResponseModel> onError = null)
        {
            try
            {
                var task = action(service);
                await task;
                return new ResponseModel {StatusCode = HttpStatusCode.OK};
            }
            catch (ApiException ex)
            {
                return ToErrorResponse(ex, onError);
            }
        }

        public static async Task<ResponseModel<TResult>> TryExecute<TApi,TResult>(this TApi service, Func<TApi, Task<TResult>> action, Action<ResponseModel<TResult>> onError = null)
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
                return ToErrorResponse(ex, onError);
            }
        }

        private static T ToErrorResponse<T>(ApiException ex, Action<T> onError)
            where T : ResponseModel, new()
        {
            var error = ex.HasContent
                ? ex.GetContentAs<ZenDeskError>()
                : new ZenDeskError { Error = ex.ReasonPhrase };
            var response = new T
            {
                StatusCode = ex.StatusCode,
                Error = error.ToString()
            };

            onError?.Invoke(response);

            return response;
        }
    }
}
