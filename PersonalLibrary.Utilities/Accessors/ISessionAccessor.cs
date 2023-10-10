namespace PersonalLibrary.Utilities.Accessors;

public interface ISessionAccessor
{
    int AccessUserId();
    Task<int> AccessUserIdAsync();
    Task<T?> GetOrAddAsync<T>(Func<string, Task<T?>> func) where T : class?;
    string? GetAccessToken();
}