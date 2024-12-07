using System.Text.Json;

namespace Pek.DsMallUI.UEditor;

/// <summary>
/// Handler 的摘要说明
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="context"></param>
public abstract class Handler(HttpContext context)
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public abstract Task<String> Process();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    protected String WriteJson(Object response)
    {
        var jsonpCallback = Request.Query["callback"];
        var json = JsonSerializer.Serialize(response);
        return String.IsNullOrWhiteSpace(jsonpCallback) ? json : $"{jsonpCallback}({json});";
    }

    /// <summary>
    /// 
    /// </summary>
    public HttpRequest Request { get; } = context.Request;

    /// <summary>
    /// 
    /// </summary>
    public HttpResponse Response { get; } = context.Response;

    /// <summary>
    /// 
    /// </summary>
    public HttpContext Context { get; } = context;
    //public HttpServerUtility Server { get; private set; }
}