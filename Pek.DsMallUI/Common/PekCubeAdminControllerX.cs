using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using NewLife.Collections;
using NewLife.Web;
using Pek.NCube.BaseControllers;
using Pek.Webs;

namespace Pek.DsMallUI;

/// <summary>
/// 后台控制器基类
/// </summary>
public class PekCubeAdminControllerX : PekAdminControllerBaseX
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
        base.OnActionExecuting(context);

        // Ajax请求不需要设置ViewBag
        if (!Request.IsAjaxRequest())
        {
            // 默认加上分页给前台
            var ps = context.ActionArguments.ToNullable();  // ActionArguments取的是Action中定义过的参数，未定义的参数取不到
            var p = ps["p"] as Pager ?? new Pager();

            HttpContext.Items["PekPage"] = p;
        }
        else
        {

        }
    }
}