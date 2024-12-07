using System.Text;

using NewLife.Log;
using NewLife.Serialization;

using Pek.VirtualFileSystem;

namespace Pek.DsMallUI.UEditor;

/// <summary>
/// Config 的摘要说明
/// </summary>
public static class UeditorConfig
{
    private readonly static IVirtualFileProvider? _virtualFileProvider;

    static UeditorConfig()
    {
        _virtualFileProvider = Pek.Webs.HttpContext.Current.RequestServices.GetService<IVirtualFileProvider>();
    }

    //public static JObject Items => JObject.Parse(File.ReadAllText(AppContext.BaseDirectory + "ueconfig.json"));
    /// <summary>
    /// 
    /// </summary>
    public static IDictionary<String, Object?>? Items
    {
        get
        {
            var list = _virtualFileProvider?.GetDirectoryContents("/").ToList();
            foreach(var item in list!)
            {
                XTrace.WriteLine($"读取目录：{item.Name}:{item.PhysicalPath}");
            }

            var directoryContents = _virtualFileProvider?.GetFileInfo($"/ueconfig.json");
            XTrace.WriteLine($"读取ueconfig.json：{Encoding.UTF8.GetString(directoryContents?.CreateReadStream().GetAllBytes()!)}");
            return JsonHelper.DecodeJson(Encoding.UTF8.GetString(directoryContents?.CreateReadStream().GetAllBytes()!));
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="key"></param>
    /// <returns></returns>
    public static T GetValue<T>(String key) => Items![key]!.AsTo<T>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static String[] GetStringList(String key) =>  []/*Items![key].Select(x => x.Value<String>()).ToArray()*/;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static String GetString(String key) => GetValue<String>(key);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static Int32 GetInt(String key) => GetValue<Int32>(key);
}