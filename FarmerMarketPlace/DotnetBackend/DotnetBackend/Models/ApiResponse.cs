using System;

namespace DotnetBackend.Models
{
    public class ApiResponse
    {
        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }

        public ApiResponse(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }
    }
}