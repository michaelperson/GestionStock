using GestionStock.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionStock.Domain.Interfaces
{
    public interface IApiService
    {
        Task<ApiResult<T>> GetAsync<T>(string endpoint);
        Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<ApiResult<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<ApiResult<TResponse>> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data);
        Task<ApiResult<bool>> DeleteAsync(string endpoint);

        /*Pour les fichiers*/
        Task<ApiResult<T>> PostMultipartAsync<T>(string endpoint, MultipartFormDataContent content);
    }
}
