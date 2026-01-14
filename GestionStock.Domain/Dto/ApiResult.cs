using System;
using System.Collections.Generic;
using System.Text;

namespace GestionStock.Domain.Dto
{
    public class ApiResult<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }
    }
}
