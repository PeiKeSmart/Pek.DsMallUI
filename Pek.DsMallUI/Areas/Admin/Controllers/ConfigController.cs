using DH.Core.Domain.Localization;
using DH.Entity;

using Microsoft.AspNetCore.Mvc;

using NewLife;
using NewLife.Common;
using NewLife.Log;
using Pek;
using Pek.Configs;
using Pek.DsMallUI;
using Pek.Models;
using Pek.NCubeUI.Areas.Admin;
using Pek.NCubeUI.Common;

using System.ComponentModel;

using XCode.Membership;

namespace HlktechPower.Areas.Admin.Controllers;

/// <summary>站点设置</summary>
[DisplayName("站点设置")]
[Description("用于站点的站点设置、防灌水设置等基础设置")]
[AdminArea]
[DHMenu(100, ParentMenuName = "Settings", ParentMenuDisplayName = "设置", ParentMenuUrl = "~/{area}/Config", ParentMenuOrder = 95, ParentVisible = true, CurrentMenuUrl = "~/{area}/Config", CurrentMenuName = "ConfigSetting", CurrentIcon = "&#xe6e0;", LastUpdate = "20250527")]
public class ConfigController : PekCubeAdminControllerX
{

    /// <summary>
    /// 站点设置
    /// </summary>
    /// <returns></returns>
    [EntityAuthorize(PermissionFlags.Detail)]
    [DisplayName("站点设置")]
    public IActionResult Index()
    {
        ViewBag.LanguageList = Language.FindByStatus().OrderBy(e => e.DisplayOrder);
        ViewBag.Model = SiteInfo.FindDefault();
        return View();
    }

    /// <summary>
    /// 站点设置
    /// </summary>
    /// <returns></returns>
    [EntityAuthorize(PermissionFlags.Update)]
    [HttpPost]
    [DisplayName("站点设置")]
    public IActionResult UpdateBase(String curDomainUrl, Int32 isEnableLanguage, String Registration, Int32 isAllowSwagger, Int32 isAllowMarkupMin, Int32 refreshuserperiod, Int32 sessiontimeout, Int32 logoutAll, Int32 useSsoRole, String companyname, String Copyright, Int32 allowLogin, Int32 isAllowUrlSuffix, String SiteName, String SeoDescribe, String SeoKey, String SeoTitle, Int32 seoFriendlyUrlsForLanguagesEnabled, IFormFile site_logowx, IFormFile site_logo, IFormFile site_icon, String WwwRequirement, String SslEnabled, Int32 SiteIsClose, IFormFile memberlogo, IFormFile membersmalllogo, String UrlSuffix, String Statistical, String PageFoot, Int32 PageTitleSeoAdjustment, String CustomerTel, Int32 AutoRegister, Int32 isHtmlStaticDevelopmentMode, Int32 htmlStaticExpireMinutes, String ServerToken, Int32 AllowRequestParams, Int32 EnableOnlineStatistics, Int32 MaxOnlineCount, String BanAccessTime, String BanAccessIP, String AllowAccessIP, Int32 OnlineCountExpire, Int32 UpdateOnlineTimeSpan, Int32 OnlineUserExpire, String PaswordStrength, String StarWeb, String FooterCustomHtml, String HeaderCustomHtml, Int32 AllowMobileTemp)
    {
        var Model = SiteInfo.FindDefault();
        if (SiteName.IsNullOrWhiteSpace())
        {
            return Prompt(new PromptModel { Message = GetResource("网站名称不可为空") });
        }

        ConfigFileHelper.AddOrUpdateAppSetting("SwaggerOption:Enabled", isAllowSwagger == 1, "Settings/Swagger.json");

        Model.SeoTitle = SeoTitle.SafeString().Trim();
        Model.SeoKey = SeoKey.SafeString().Trim();
        Model.SeoDescribe = SeoDescribe.SafeString().Trim();
        Model.SiteName = SiteName;
        Model.Url = $"{DHSetting.Current.CurDomainUrl}/";
        Model.Registration = Registration.SafeString().Trim();
        Model.SiteCopyright = Copyright.SafeString().Trim();
        Model.HeaderCustomHtml = HeaderCustomHtml;
        Model.FooterCustomHtml = FooterCustomHtml;
        Model.SiteTel = CustomerTel;
        Model.Update();

        var languagelist = Language.FindByStatus().OrderBy(e => e.DisplayOrder); //获取全部有效语言

        var list = SiteInfoLan.FindAllBySId(SiteInfo.FindDefault().Id);  //获取到的当前权限的语言
        using (var tran1 = SiteInfoLan.Meta.CreateTrans())
        {
            foreach (var item in languagelist)
            {
                var ex = list.Find(x => x.LanguageId == item.Id);
                if (ex != null)
                {
                    ex.SiteName = (GetRequest($"[{item.Id}].SiteName")).SafeString().Trim();
                    ex.SeoDescribe = (GetRequest($"[{item.Id}].SeoDescribe")).SafeString().Trim();
                    ex.SeoKey = (GetRequest($"[{item.Id}].SeoKey")).SafeString().Trim();
                    ex.SeoTitle = (GetRequest($"[{item.Id}].SeoTitle")).SafeString().Trim();
                    ex.Registration = (GetRequest($"[{item.Id}].Registration")).SafeString().Trim();
                    ex.SiteCopyright = (GetRequest($"[{item.Id}].SiteCopyright")).SafeString().Trim();
                    ex.Update();
                }
                else
                {
                    ex = new SiteInfoLan();
                    ex.SiteName = (GetRequest($"[{item.Id}].SiteName")).SafeString().Trim();
                    ex.SeoDescribe = (GetRequest($"[{item.Id}].SeoDescribe")).SafeString().Trim();
                    ex.SeoKey = (GetRequest($"[{item.Id}].SeoKey")).SafeString().Trim();
                    ex.SeoTitle = (GetRequest($"[{item.Id}].SeoTitle")).SafeString().Trim();
                    ex.Registration = (GetRequest($"[{item.Id}].Registration")).SafeString().Trim();
                    ex.SiteCopyright = (GetRequest($"[{item.Id}].SiteCopyright")).SafeString().Trim();

                    //if (ex.SiteName.IsNullOrWhiteSpace() && ex.SeoDescribe.IsNullOrWhiteSpace() && ex.SeoKey.IsNullOrWhiteSpace() && ex.SeoTitle.IsNullOrWhiteSpace())
                    //{
                    //    continue;
                    //}

                    ex.LanguageId = item.Id;
                    ex.SiteInfoId = 1;
                    ex.Insert();
                }
            }
            tran1.Commit();
        }

        var set = DHSetting.Current;
        set.CurDomainUrl = curDomainUrl.TrimEnd('/');

        var DisplayName = GetRequest("DisplayName");
        var sys = SysConfig.Current;
        sys.DisplayName = DisplayName;
        sys.Company = companyname;
        sys.Save();

        set.StarWeb = StarWeb;
        set.IsAllowUrlSuffix = isAllowUrlSuffix == 1;
        set.UrlSuffix = UrlSuffix;
        set.Statistical = Statistical;
        set.SessionTimeout = sessiontimeout;
        set.RefreshUserPeriod = refreshuserperiod;
        set.AllowLogin = allowLogin == 1;
        set.AutoRegister = AutoRegister == 1;
        set.UseSsoRole = useSsoRole == 1;
        set.LogoutAll = logoutAll == 1;
        set.IsHtmlStaticDevelopmentMode = isHtmlStaticDevelopmentMode == 0;
        set.HtmlStaticExpireMinutes = htmlStaticExpireMinutes;
        set.PaswordStrength = PaswordStrength;
        set.PageTitleSeoAdjustment = PageTitleSeoAdjustment;
        set.AllowMobileTemp = AllowMobileTemp == 1;
        set.MaxOnlineCount = MaxOnlineCount;
        set.UpdateOnlineTimeSpan = UpdateOnlineTimeSpan;
        set.BanAccessTime = BanAccessTime;
        set.BanAccessIP = BanAccessIP;
        set.AllowAccessIP = AllowAccessIP;

        switch (SslEnabled)
        {
            default:
                set.SslEnabled = 0;
                set.AllSslEnabled = false;
                break;
            case "1":
                set.SslEnabled = 1;
                set.AllSslEnabled = false;
                break;
            case "2":
                set.SslEnabled = 2;
                set.AllSslEnabled = false;
                break;
            case "99":
                set.SslEnabled = 1;
                set.AllSslEnabled = true;
                break;
        }

        set.Save();

        PekSysSetting.Current.AllowRequestParams = AllowRequestParams == 1;
        PekSysSetting.Current.Save();

        LocalizationSettings.Current.IsEnable = isEnableLanguage == 1;
        LocalizationSettings.Current.SeoFriendlyUrlsForLanguagesEnabled = seoFriendlyUrlsForLanguagesEnabled == 1;
        LocalizationSettings.Current.Save();

        return Prompt(new PromptModel { Message = GetResource("保存成功"), IsOk = true, BackUrl = Url.Action("Index") });
    }
}

