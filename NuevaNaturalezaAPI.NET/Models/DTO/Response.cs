﻿enum NumberResponses
{
    Correct, Warning, Error
}

namespace NuevaNaturalezaAPI.NET.Models.DTO
{
    public class Response
    {
        public int NumberResponse { get; set; } = (int)NumberResponses.Error;
        public string Message { get; set; } = string.Empty;
        public dynamic? Data { get; set; }
    }
}
