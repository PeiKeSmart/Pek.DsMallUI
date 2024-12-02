using System.ComponentModel;

using NewLife.Cube.Jobs;
using NewLife.Log;

using Pek.NCubeUI.Events;

namespace Pek.DsMallUI.Jobs;

/// <summary>
/// 生成sitemap作业参数
/// </summary>
public class SitemapArgument
{

}

/// <summary>生成sitemap服务</summary>
[DisplayName("生成sitemap")]
[Description("定时生成网站Sitemap")]
[CronJob("Sitemap", "20 1 * * * ? *", Enable = true)]
public class SitemapService : CubeJobBase<SitemapArgument>
{
    private readonly ITracer _tracer;
    private readonly IEventPublisher _eventPublisher;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>实例化生成sitemap服务</summary>
    /// <param name="tracer"></param>
    /// <param name="eventPublisher"></param>
    public SitemapService(ITracer tracer, IEventPublisher eventPublisher, IHttpContextAccessor httpContextAccessor)
    {
        _tracer = tracer;
        _eventPublisher = eventPublisher;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>执行作业</summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    protected override Task<String> OnExecute(SitemapArgument argument)
    {
        using var span = _tracer?.NewSpan("Sitemap", argument);

        XTrace.WriteLine($"是否为空：{_httpContextAccessor.HttpContext == null}");

        _eventPublisher.Publish(new SiteMapEvent(httpContext: _httpContextAccessor.HttpContext));

        //var Token = DHSetting.Current.ServerToken;  // 获取与服务器端协定的密钥
        //var TimeStamp = UnixTime.ToTimestamp();
        //var Nonce = Pek.Helpers.Randoms.RandomString(6);
        //var Sign = CheckSignature.Create(TimeStamp, Nonce, Token);

        //await $"{Setting.Current.CurDomainUrl.Trim('/')}/Site/SiteMap"
        //    .WithHeader("Signature", Sign)
        //    .WithHeader("Nonce", Nonce)
        //    .WithHeader("TimeStamp", TimeStamp)
        //    .GetAsync();

        return Task.FromResult("OK");
    }
}