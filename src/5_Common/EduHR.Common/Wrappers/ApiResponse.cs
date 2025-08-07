using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EduHR.Common.Wrappers;

/// <summary>
/// Veri içermeyen, sadece işlem sonucunu bildiren standart API yanıtı.
/// </summary>
public class ApiResponse
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string>? Errors { get; set; }

    public ApiResponse(bool success, string message)
    {
        Success = success;
        Message = message;
    }
    
    // Başarılı yanıtlar için yardımcı metotlar
    public static ApiResponse SuccessResponse(string message) => new(true, message);
    
    // Başarısız yanıtlar için yardımcı metotlar
    public static ApiResponse FailResponse(string message) => new(false, message);
    public static ApiResponse FailResponse(List<string> errors) => new(false, "Validation Errors") { Errors = errors };
}


/// <summary>
/// Veri içeren standart API yanıtı.
/// </summary>
/// <typeparam name="T">Döndürülecek verinin tipi.</typeparam>
public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }

    public ApiResponse(T data, string message) : base(true, message)
    {
        Data = data;
    }
    
    // Başarılı veri yanıtları için yardımcı metot
    public static ApiResponse<T> SuccessResponse(T data, string message = "Success") => new(data, message);
}