namespace sharelock_desktop.Models;

public class ApiResponse
{
    public bool Success { get; set; }
    public string? Error { get; set; }
    public string? Details { get; set; }
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}
