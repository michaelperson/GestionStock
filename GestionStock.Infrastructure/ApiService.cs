using GestionStock.Domain.Dto;
using GestionStock.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace GestionStock.Infrastructure
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ApiResult<T>> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);
                return await HandleResponse<T>(response);
            }
            catch (Exception ex)
            {
                return Error<T>(ex.Message);
            }
        }

        public async Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync(endpoint, data);
                return await HandleResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                return Error<TResponse>(ex.Message);
            }
        }

        public async Task<ApiResult<TResponse>> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync(endpoint, data);
                return await HandleResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                return Error<TResponse>(ex.Message);
            }
        }

        public async Task<ApiResult<TResponse>> PatchAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var response = await _httpClient.PatchAsJsonAsync(endpoint, data);
                return await HandleResponse<TResponse>(response);
            }
            catch (Exception ex)
            {
                return Error<TResponse>(ex.Message);
            }
        }

        public async Task<ApiResult<bool>> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);
                return new ApiResult<bool>
                {
                    IsSuccess = response.IsSuccessStatusCode,
                    StatusCode = (int)response.StatusCode
                };
            }
            catch (Exception ex)
            {
                return Error<bool>(ex.Message);
            }
        }

        private async Task<ApiResult<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var result = new ApiResult<T> { StatusCode = (int)response.StatusCode };

            if (response.IsSuccessStatusCode)
            {
                result.IsSuccess = true;
                result.Data = await response.Content.ReadFromJsonAsync<T>(_options);
            }
            else
            {
                result.IsSuccess = false;
                result.ErrorMessage = await response.Content.ReadAsStringAsync();
            }

            return result;
        }

        private ApiResult<T> Error<T>(string message) =>
            new ApiResult<T> { IsSuccess = false, ErrorMessage = message };

        public async Task<ApiResult<T>> PostMultipartAsync<T>(string uri, MultipartFormDataContent content)
        {
            try
            { 
                // Envoi direct du contenu multipart
                var response = await _httpClient.PostAsync(uri, content); 
                if (response.StatusCode== System.Net.HttpStatusCode.NoContent)
                { 
                    return new ApiResult<T> { IsSuccess = true, Data = default(T) };
                }

                return new ApiResult<T> { IsSuccess = false, ErrorMessage = response.ReasonPhrase };
            }
            catch (Exception ex)
            {
                return new ApiResult<T> { IsSuccess = false, ErrorMessage = ex.Message };
            }
        }
    }
}
