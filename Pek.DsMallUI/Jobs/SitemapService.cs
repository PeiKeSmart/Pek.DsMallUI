using System.ComponentModel;

using NewLife.Cube.Jobs;
using NewLife.Log;

using Pek.NCube.Filters;
using Pek.Timing;

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

    /// <summary>实例化生成sitemap服务</summary>
    /// <param name="tracer"></param>
    public SitemapService(ITracer tracer)
    {
        _tracer = tracer;
    }

    /// <summary>执行作业</summary>
    /// <param name="argument"></param>
    /// <returns></returns>
    protected override async Task<String> OnExecute(SitemapArgument argument)
    {
        using var span = _tracer?.NewSpan("Sitemap", argument);

        var Token = DHSetting.Current.ServerToken;  // 获取与服务器端协定的密钥
        var TimeStamp = UnixTime.ToTimestamp();
        var Nonce = Pek.Helpers.Randoms.RandomString(6);
        var Sign = CheckSignature.Create(TimeStamp, Nonce, Token);

        await Helpers.DHWeb.Client().Get($"{UrlHelper.Combine(DHSetting.Current.CurDomainUrl, "Common", "SitemapXml")}")
            .Header("Signature", Sign)
            .Header("Nonce", Nonce)
            .Header("TimeStamp", TimeStamp)
            .WhenCatch<Exception>(ex =>
            {
                return ex.Message;
            })
            .ResultStringAsync().ConfigureAwait(false);

        return "OK";
    }
}