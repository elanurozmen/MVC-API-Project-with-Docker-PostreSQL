namespace MyMvcApp.Models;

public class ErrorViewModel
{
    public string RequestId { get; set; }
   //     public string? ErrorMessage { get; set; } // Yeni eklenen özellik
    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
