using Microsoft.AspNetCore.Mvc;

using NewLife.Collections;

using Pek.NCube;

namespace Pek.DsMallUI;

/// <summary>
/// 后台控制器基类
/// </summary>
public class DSMALLCubeAdminControllerX : PekAdminControllerBaseX
{
    /// <summary>
    /// 消息跳转
    /// </summary>
    /// <param name="Message"></param>
    /// <returns></returns>
    public IActionResult MessageTip(String Message)
    {
        var html = Pool.StringBuilder.Get();
        html.Append("<script>parent.layer.alert('" + Message + "',{yes:function(index, layero){parent.location.reload();},cancel:function(index, layero){parent.location.reload();}});</script>");
        return Content(html.Return(true), "text/html");
    }
}
