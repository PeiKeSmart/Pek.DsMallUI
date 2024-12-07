namespace Pek.DsMallUI.UEditor;

/// <summary>
/// Config 的摘要说明
/// </summary>
/// <remarks>
/// 
/// </remarks>
/// <param name="context"></param>
public class ConfigHandler(HttpContext context) : Handler(context)
{

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override Task<String> Process()
    {
        return Task.FromResult(WriteJson(UeditorConfig.Items!));
    }
}