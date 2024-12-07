namespace Pek.DsMallUI.UEditor;

/// <summary>
/// NotSupportedHandler 的摘要说明
/// </summary>
public class NotSupportedHandler(HttpContext context) : Handler(context)
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override Task<String> Process()
    {
        return Task.FromResult(WriteJson(new
        {
            state = "action 参数为空或者 action 不被支持。"
        }));
    }
}