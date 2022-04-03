using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CleanArch.WebApi.Common.Model
{
    public class ExceptionDetails : ProblemDetails
    {
        public int ErrorCode { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
