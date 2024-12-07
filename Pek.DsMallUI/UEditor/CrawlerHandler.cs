namespace Pek.DsMallUI.UEditor;

/// <summary>
/// Crawler 的摘要说明
/// </summary>
public class CrawlerHandler(HttpContext context) : Handler(context)
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public override async Task<String> Process()
    {
        var form = await Request.ReadFormAsync();
        String[]? sources = form["source[]"];
        if (sources?.Length > 0 || sources?.Length <= 10)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            return WriteJson(new
            {
                state = "SUCCESS",
                list = (await sources.SelectAsync(s =>
                {
                    return new Crawler(s).Fetch(cts.Token).ContinueWith(t => new
                    {
                        state = t.Result.State,
                        source = t.Result.SourceUrl,
                        url = t.Result.ServerUrl
                    });
                }))
            });
        }

        return WriteJson(new
        {
            state = "参数错误：没有指定抓取源"
        });
    }
}

/// <summary>
/// 
/// </summary>
public class Crawler(String sourceUrl)
{
    /// <summary>
    /// 
    /// </summary>
    public String? SourceUrl { get; set; } = sourceUrl;

    /// <summary>
    /// 
    /// </summary>
    public String? ServerUrl { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public String? State { get; set; }

    private static readonly HttpClient HttpClient = new(new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });

    /// <summary>
    /// 
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<Crawler> Fetch(CancellationToken token)
    {
        //if (!SourceUrl.IsExternalAddress())
        //{
        //    State = "INVALID_URL";
        //    return this;
        //}
        //try
        //{
        //    using var response = await HttpClient.GetAsync(SourceUrl);
        //    if (response.StatusCode != HttpStatusCode.OK)
        //    {
        //        State = "远程地址返回了错误的状态吗：" + response.StatusCode;
        //        return this;
        //    }

        //    ServerUrl = PathFormatter.Format(Path.GetFileNameWithoutExtension(SourceUrl), CommonHelpers.SystemSettings.GetOrAdd("UploadPath", Setting.Current.UploadPath) + UeditorConfig.GetString("catcherPathFormat")) + MimeMapper.ExtTypes[response.Content.Headers.ContentType?.MediaType ?? "image/jpeg"];
        //    await using var stream = await response.Content.ReadAsStreamAsync();
        //    var savePath = AppContext.BaseDirectory + /*"wwwroot" + */ServerUrl;
        //    var (url, success) = await EngineContext.Current.Resolve<ImagebedClient>().UploadImage(stream, savePath, token);
        //    if (success)
        //    {
        //        ServerUrl = url;
        //    }
        //    else
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(savePath));
        //        await File.WriteAllBytesAsync(savePath, await stream.ToArrayAsync());
        //    }
        //    State = "SUCCESS";
        //}
        //catch (Exception e)
        //{
        //    State = "抓取错误：" + e.Message;
        //    XTrace.WriteException(e);
        //}

        return this;
    }
}
