using DH.Entity;

using NewLife;
using NewLife.Collections;

namespace Pek.DsMallUI.Common;

/// <summary>
/// 分页帮助类
/// </summary>
public class PageHelper
{
    //private static String CreteUrl(String PageContent, IDictionary<String, String> dic, String PageName, IUrlHelper urlHelper, String action, String controller, String area, Int32 page, Boolean IsHome = false, String routeName = "", Boolean UseUrlSuffix = true)
    //{
    //    String? url;
    //    if (!IsHome)
    //    {
    //        dic[PageName] = page.ToString();
    //    }
    //    if (DHSetting.Current.IsAllowUrlSuffix && UseUrlSuffix)
    //    {
    //        if (!area.IsNullOrWhiteSpace())
    //        {
    //            dic["area"] = area;
    //        }
    //        if (routeName.IsNullOrWhiteSpace())
    //        {
    //            url = urlHelper.Action(action, controller, dic);
    //        }
    //        else
    //        {
    //            url = urlHelper.RouteUrl(routeName, dic);
    //        }
    //        if (IsHome)
    //        {
    //            PageContent += $"/p{page}";
    //        }
    //        if (url?.Contains('?') == false)
    //        {
    //            url = DHUrl.Combine(url.Replace(Setting.Current.UrlSuffix, ""), PageContent.TrimStart('/')) + Setting.Current.UrlSuffix;
    //        }
    //        else
    //        {
    //            url = DHUrl.Combine(url.Replace(Setting.Current.UrlSuffix, ""), PageContent.TrimStart('/')).Replace("?", $"{Setting.Current.UrlSuffix}?");
    //        }
    //    }
    //    else
    //    {
    //        if (!area.IsNullOrWhiteSpace())
    //        {
    //            dic["area"] = area;
    //        }
    //        if (routeName.IsNullOrWhiteSpace())
    //        {
    //            url = urlHelper.Action(action, controller, dic);
    //        }
    //        else
    //        {
    //            url = urlHelper.RouteUrl(routeName, dic);
    //        }
    //    }

    //    var lang = Language.FindByDefault();
    //    var _workContext = EngineContext.Current.Resolve<IWorkContext>();
    //    if (lang.UniqueSeoCode != _workContext.WorkingLanguage.UniqueSeoCode)
    //    {
    //        url = $"/{_workContext.WorkingLanguage.UniqueSeoCode}{url}";
    //    }

    //    return url;
    //}

    ///// <summary>
    ///// 分页
    ///// </summary>
    ///// <param name="page">当前页码</param>
    ///// <param name="total">总分页数量</param>
    ///// <param name="action">Action名称</param>
    ///// <param name="controller">Controller名称</param>
    ///// <param name="area">Area名称</param>
    ///// <param name="dic">参数</param>
    ///// <param name="PageName">地址栏分页名称</param>
    ///// <param name="urlHelper">Url生成器</param>
    ///// <param name="IsHome">是否首页</param>
    ///// <param name="Type">分页输出类型</param>
    ///// <param name="routeName">自定义路由名称</param>
    ///// <param name="UseUrlSuffix">是否使用Url如.html后缀</param>
    ///// <returns></returns>
    //public static String CreatePage(IUrlHelper urlHelper, Int32 page, Double total, String action, String controller, String area, IDictionary<String, String> dic, String PageName = "page", Boolean UseUrlSuffix = true, String routeName = "", Int32 Type = 1, Boolean IsHome = false)
    //{
    //    if (total <= 1)
    //    {
    //        return String.Empty;
    //    }

    //    var PageContent = String.Empty;
    //    if (IsHome && DHSetting.Current.IsAllowUrlSuffix)
    //    {
    //        var urls = Pool.StringBuilder.Get();
    //        foreach (var item in dic)
    //        {
    //            if (!item.Value.SafeString().IsNullOrWhiteSpace())
    //            {
    //                urls.Append($"/{item.Value}");
    //            }
    //        }
    //        PageContent = urls.Return(true);
    //    }

    //    var Html = Pool.StringBuilder.Get();
    //    switch (Type)
    //    {
    //        case 2:
    //            {
    //                if (page != 1)
    //                {
    //                    var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, 1, IsHome, routeName, UseUrlSuffix);
    //                    Html.Append("<li><a href=\"" + url + $"\">{LocaleStringResource.GetResource("首页")}</a></li> ");

    //                    var url1 = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, page - 1, IsHome, routeName, UseUrlSuffix);
    //                    Html.Append("<li><a href=\"" + url1 + $"\">{LocaleStringResource.GetResource("上一页")}</a></li> ");
    //                }
    //                else
    //                {
    //                    Html.Append($"<li class=\"disabled\"><span>{LocaleStringResource.GetResource("首页")}</span></li> ");
    //                    Html.Append($"<li class=\"disabled\"><span>{LocaleStringResource.GetResource("上一页")}</span></li> ");
    //                }

    //                if (page != total)
    //                {
    //                    var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, page + 1, IsHome, routeName, UseUrlSuffix);
    //                    Html.Append(" <li><a href=\"" + url + $"\">{LocaleStringResource.GetResource("下一页")}</a></li>");

    //                    url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, total.ToInt(), IsHome, routeName, UseUrlSuffix);
    //                    Html.Append(" <li><a href=\"" + url + $"\">{LocaleStringResource.GetResource("末页")}</a></li>");
    //                }
    //                else
    //                {
    //                    Html.Append($"<li class=\"disabled\"><span>{LocaleStringResource.GetResource("下一页")}</span></li> ");
    //                    Html.Append($"<li class=\"disabled\"><span>{LocaleStringResource.GetResource("末页")}</span></li> ");
    //                }
    //            }
    //            break;

    //        default:
    //        case 1:
    //            {
    //                if (page != 1)
    //                {
    //                    var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, page - 1, IsHome, routeName, UseUrlSuffix);

    //                    Html.Append("<li><a href=\"" + url + "\">&laquo;</a></li> ");
    //                }
    //                else
    //                {
    //                    Html.Append("<li class=\"disabled\"><span>&laquo;</span></li> ");
    //                }

    //                var maxi = total;
    //                if (maxi < 12)
    //                {
    //                    for (int i = 1; i < maxi + 1; i++)
    //                    {
    //                        if (i == page)
    //                        {
    //                            Html.Append("<li class=\"active\"><span>" + i + "</span></li>");
    //                        }
    //                        else
    //                        {
    //                            var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, i, IsHome, routeName, UseUrlSuffix);

    //                            Html.Append("<li><a href=\"" + url + "\">" + i + "</a></li>");
    //                        }
    //                    }
    //                }
    //                else
    //                {
    //                    if (page < 7)
    //                    {
    //                        for (var i = 1; i < 9; i++)
    //                        {
    //                            if (i == page)
    //                            {
    //                                Html.Append("<li class=\"active\"><span>" + i + "</span></li>");
    //                            }
    //                            else
    //                            {
    //                                var url1 = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, i, IsHome, routeName, UseUrlSuffix);

    //                                Html.Append("<li><a href=\"" + url1 + "\">" + i + "</a></li>");
    //                            }
    //                        }

    //                        Html.Append("<li class=\"disabled\"><span>...</span></li>");

    //                        var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, total.ToInt() - 1, IsHome, routeName, UseUrlSuffix);
    //                        Html.Append("<li><a href=\"" + url + "\">" + (total - 1) + "</a></li>");

    //                        url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, total.ToInt(), IsHome, routeName, UseUrlSuffix);

    //                        Html.Append("<li><a href=\"" + url + "\">" + total + "</a></li>");
    //                    }
    //                    else if (page > 6 && page <= total - 6)
    //                    {
    //                        var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, 1, IsHome, routeName, UseUrlSuffix);
    //                        Html.Append("<li><a href=\"" + url + "\">" + 1 + "</a></li>");

    //                        url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, 2, IsHome, routeName, UseUrlSuffix);
    //                        Html.Append("<li><a href=\"" + url + "\">" + 2 + "</a></li>");
    //                        Html.Append("<li class=\"disabled\"><span>...</span></li>");

    //                        for (var i = page - 3; i < page; i++)
    //                        {
    //                            url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, i, IsHome, routeName, UseUrlSuffix);
    //                            Html.Append("<li><a href=\"" + url + "\">" + i + "</a></li>");
    //                        }
    //                        Html.Append("<li class=\"active\"><span>" + page + "</span></li>");
    //                        for (var i = page + 1; i < page + 4; i++)
    //                        {
    //                            url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, i, IsHome, routeName, UseUrlSuffix);
    //                            Html.Append("<li><a href=\"" + url + "\">" + i + "</a></li>");
    //                        }

    //                        Html.Append("<li class=\"disabled\"><span>...</span></li>");

    //                        url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, total.ToInt() - 1, IsHome, routeName, UseUrlSuffix);
    //                        Html.Append("<li><a href=\"" + url + "\">" + (total - 1) + "</a></li>");

    //                        url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, total.ToInt(), IsHome, routeName, UseUrlSuffix);
    //                        Html.Append("<li><a href=\"" + url + "\">" + total + "</a></li>");
    //                    }
    //                    else
    //                    {
    //                        var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, 1, IsHome, routeName, UseUrlSuffix);
    //                        Html.Append("<li><a href=\"" + url + "\">" + 1 + "</a></li>");

    //                        url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, 2, IsHome, routeName, UseUrlSuffix);
    //                        Html.Append("<li><a href=\"" + url + "\">" + 2 + "</a></li>");
    //                        Html.Append("<li class=\"disabled\"><span>...</span></li>");

    //                        for (var i = total - 8; i <= total; i++)
    //                        {
    //                            if (i == page)
    //                            {
    //                                Html.Append("<li class=\"active\"><span>" + i + "</span></li>");
    //                            }
    //                            else
    //                            {
    //                                url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, i.ToInt(), IsHome, routeName, UseUrlSuffix);
    //                                Html.Append("<li><a href=\"" + url + "\">" + i + "</a></li>");
    //                            }
    //                        }
    //                    }
    //                }

    //                if (page != total)
    //                {
    //                    var url = CreteUrl(PageContent, dic, PageName, urlHelper, action, controller, area, page + 1, IsHome, routeName, UseUrlSuffix);
    //                    Html.Append(" <li><a href=\"" + url + "\">&raquo;</a></li>");
    //                }
    //                else
    //                {
    //                    Html.Append(" <li class=\"disabled\"><span>&raquo;</span></li>");
    //                }
    //            }
    //            break;
    //    }

    //    return Html.Put(true);
    //}

    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="page">当前页码</param>
    /// <param name="total">总分页数量</param>
    /// <param name="totalCount">总记录数量</param>
    /// <param name="url">分页Url主路径</param>
    /// <param name="dic">参数</param>
    /// <param name="PageName">地址栏分页名称</param>
    /// <param name="IsShowJum">是否显示跳转栏</param>
    /// <param name="IsShowTotal">是否显示全部数量</param>
    /// <returns></returns>
    public static string CreatePage(Int32 page, Double totalCount, Double total, String? url, IDictionary<String, String> dic, Boolean IsShowTotal = true, Boolean IsShowJum = true, String PageName = "page")
    {
        if (total <= 1)
        {
            return String.Empty;
        }

        var Paeameter = Pool.StringBuilder.Get();
        foreach (var item in dic)
        {
            if (!item.Value.IsNullOrWhiteSpace())
            {
                Paeameter.Append($"&{item.Key}={item.Value}");
            }
        }

        var PageContent = Paeameter.Return(true);

        var Html = Pool.StringBuilder.Get();

        Html.Append($"<li class=\"disabled\"><span>{String.Format(LocaleStringResource.GetResource("共{0}条记录"), totalCount)}&nbsp&nbsp{String.Format(LocaleStringResource.GetResource("第{0}页/共{1}页"), page, total)}</span></li>");

        if (page != 1)
        {
            Html.Append($"<li><a href=\"{url}?{PageName}=1\">{LocaleStringResource.GetResource("首页")}</a></li>");
            Html.Append("<li><a href=\"" + url + $"?{PageName}=" + (page - 1) + PageContent + "\">&laquo;</a></li> ");
        }
        else
        {
            Html.Append($"<li class=\"disabled\"><span>{LocaleStringResource.GetResource("首页")}</span></li>");
            Html.Append("<li class=\"disabled\"><span>&laquo;</span></li> ");
        }

        var maxi = total;
        if (maxi < 12)
        {
            for (int i = 1; i < maxi + 1; i++)
            {
                if (i == page)
                {
                    Html.Append("<li class=\"active\"><span>" + i + "</span></li>");
                }
                else
                {
                    Html.Append("<li><a href=\"" + url + $"?{PageName}=" + i + PageContent + "\">" + i + "</a></li>");
                }
            }
        }
        else
        {
            if (page < 7)
            {
                for (var i = 1; i < 9; i++)
                {
                    if (i == page)
                    {
                        Html.Append("<li class=\"active\"><span>" + i + "</span></li>");
                    }
                    else
                    {
                        Html.Append("<li><a href=\"" + url + $"?{PageName}=" + i + PageContent + "\">" + i + "</a></li>");
                    }
                }

                Html.Append("<li class=\"disabled\"><span>...</span></li>");
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + (total - 1) + PageContent + "\">" + (total - 1) + "</a></li>");
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + total + PageContent + "\">" + total + "</a></li>");
            }
            else if (page > 6 && page <= total - 6)
            {
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + 1 + PageContent + "\">" + 1 + "</a></li>");
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + 2 + PageContent + "\">" + 2 + "</a></li>");
                Html.Append("<li class=\"disabled\"><span>...</span></li>");

                for (var i = page - 3; i < page; i++)
                {
                    Html.Append("<li><a href=\"" + url + $"?{PageName}=" + i + PageContent + "\">" + i + "</a></li>");
                }
                Html.Append("<li class=\"active\"><span>" + page + "</span></li>");
                for (var i = page + 1; i < page + 4; i++)
                {
                    Html.Append("<li><a href=\"" + url + $"?{PageName}=" + i + PageContent + "\">" + i + "</a></li>");
                }

                Html.Append("<li class=\"disabled\"><span>...</span></li>");
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + (total - 1) + PageContent + "\">" + (total - 1) + "</a></li>");
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + total + PageContent + "\">" + total + "</a></li>");
            }
            else
            {
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + 1 + PageContent + "\">" + 1 + "</a></li>");
                Html.Append("<li><a href=\"" + url + $"?{PageName}=" + 2 + PageContent + "\">" + 2 + "</a></li>");
                Html.Append("<li class=\"disabled\"><span>...</span></li>");

                for (var i = total - 8; i <= total; i++)
                {
                    if (i == page)
                    {
                        Html.Append("<li class=\"active\"><span>" + i + "</span></li>");
                    }
                    else
                    {
                        Html.Append("<li><a href=\"" + url + $"?{PageName}=" + i + PageContent + "\">" + i + "</a></li>");
                    }
                }
            }
        }

        if (page != total)
        {
            Html.Append(" <li><a href=\"" + url + $"?{PageName}=" + (page + 1) + PageContent + "\">&raquo;</a></li>");
            Html.Append($"<li><a href=\"{url}?{PageName}={total}\">尾页</a></li>");
        }
        else
        {
            Html.Append(" <li class=\"disabled\"><span>&raquo;</span></li>");
            Html.Append($"<li class=\"disabled\"><span>{LocaleStringResource.GetResource("尾页")}</span></li>");
        }

        if (IsShowJum)
        {
            Html.Append($"<li><form action='' method='get' style='display: inline-block'><a style='display:flex;margin-left:2px;padding:0'><span style='color: #777;'>{LocaleStringResource.GetResource("到")}</span> <input style='height:36px;padding:0;border:1px solid #ccc; display: inline-block; width: 40px; text-align: center; margin-right:2px;color: #777;' type='text' name='page' value='{page}' class='form-control'> <span style='color: #777;'>{LocaleStringResource.GetResource("页")}</span> <input type='submit' class='btn' style='height:38px;border-radius:0;' value='{LocaleStringResource.GetResource("确定")}'></a></form></li>");
        }

        return Html.Return(true);
    }
}