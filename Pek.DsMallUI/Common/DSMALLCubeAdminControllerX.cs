using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NewLife.Collections;
using NewLife.Cube.Extensions;
using NewLife.Log;

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

    /// <summary>动作执行前</summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        XTrace.WriteLine($"DSMALLCubeAdminControllerX进来了么？");

        base.OnActionExecuting(context);

        XTrace.WriteLine($"DSMALLCubeAdminControllerX进来了么1？");

        // Ajax请求不需要设置ViewBag
        if (!Request.IsAjaxRequest())
        {
            var ps = context.ActionArguments.ToNullable();
            XTrace.WriteLine($"DSMALLCubeAdminControllerX进来了么2？{ps.Count}");
            foreach (var item in ps)
            {
                XTrace.WriteLine($"查看参数   {item.Key}:{item.Value}");
            }
        }
        else
        {

        }
    }
}