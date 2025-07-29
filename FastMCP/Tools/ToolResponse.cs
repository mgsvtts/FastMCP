namespace FastMCP.Tools;

public readonly record struct ToolResponse<T> where T:class
{
   public T? Data { get; init; }
   public string? ErrorMessage { get; init; }
   
   public static implicit operator ToolResponse<T>(T data) => new()
   {
      Data = data,
      ErrorMessage = null
   };
}

public static class ToolResponse
{
   public static ToolResponse<T> Error<T>(string error) where T : class
   {
      return new ToolResponse<T>
      {
         Data = null,
         ErrorMessage = error,
      };
   }
}