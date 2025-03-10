﻿namespace Pek.DsMallUI.UEditor;

/// <summary>
/// UploadHandler 的摘要说明
/// </summary>
public class UploadHandler : Handler
{
    /// <summary>
    /// 
    /// </summary>
    public UploadConfig UploadConfig { get; }

    /// <summary>
    /// 
    /// </summary>
    public UploadResult Result { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="config"></param>
    public UploadHandler(HttpContext context, UploadConfig config) : base(context)
    {
        UploadConfig = config;
        Result = new UploadResult()
        {
            State = UploadState.Unknown
        };
    }

    public override async Task<String> Process()
    {
        //var form = await Request.ReadFormAsync();
        //var file = form.Files[UploadConfig.UploadFieldName];
        //var uploadFileName = file.FileName;

        //if (!CheckFileType(uploadFileName))
        //{
        //    Result.State = UploadState.TypeNotAllow;
        //    return WriteResult();
        //}
        //if (!CheckFileSize(file.Length))
        //{
        //    Result.State = UploadState.SizeLimitExceed;
        //    return WriteResult();
        //}

        //Result.OriginFileName = uploadFileName;
        //var savePath = PathFormatter.Format(uploadFileName, UploadConfig.PathFormat);
        //var localPath = AppContext.BaseDirectory + /*"wwwroot" + */savePath;
        //var cts = new CancellationTokenSource(30000);
        //var stream = file.OpenReadStream();
        //try
        //{
        //    stream = stream.AddWatermark();
        //    var (url, success) = await EngineContext.Current.Resolve<ImagebedClient>().UploadImage(stream, localPath, cts.Token);
        //    if (success)
        //    {
        //        Result.Url = url;
        //    }
        //    else
        //    {
        //        Directory.CreateDirectory(Path.GetDirectoryName(localPath));
        //        await File.WriteAllBytesAsync(localPath, await stream.ToArrayAsync());
        //        Result.Url = savePath;
        //    }
        //    Result.State = UploadState.Success;
        //}
        //catch (Exception e)
        //{
        //    Result.State = UploadState.FileAccessError;
        //    Result.ErrorMessage = e.Message;
        //    XTrace.WriteException(e);
        //}
        //finally
        //{
        //    cts.Dispose();
        //    stream.Close();
        //    await stream.DisposeAsync();
        //}

        return WriteResult();
    }

    private string WriteResult()
    {
        return WriteJson(new
        {
            state = GetStateMessage(Result.State),
            url = Result.Url,
            title = Result.OriginFileName,
            original = Result.OriginFileName,
            error = Result.ErrorMessage
        });
    }

    private string GetStateMessage(UploadState state)
    {
        switch (state)
        {
            case UploadState.Success:
                return "SUCCESS";
            case UploadState.FileAccessError:
                return "文件访问出错，请检查写入权限";
            case UploadState.SizeLimitExceed:
                return "文件大小超出服务器限制";
            case UploadState.TypeNotAllow:
                return "不允许的文件格式";
            case UploadState.NetworkError:
                return "网络错误";
        }
        return "未知错误";
    }

    private bool CheckFileType(string filename)
    {
        return UploadConfig.AllowExtensions.Any(x => x.Equals(Path.GetExtension(filename), StringComparison.CurrentCultureIgnoreCase));
    }

    private bool CheckFileSize(long size)
    {
        return size < UploadConfig.SizeLimit;
    }
}

public class UploadConfig
{
    /// <summary>
    /// 文件命名规则
    /// </summary>
    public string PathFormat { get; set; }

    /// <summary>
    /// 上传表单域名称
    /// </summary>
    public string UploadFieldName { get; set; }

    /// <summary>
    /// 上传大小限制
    /// </summary>
    public int SizeLimit { get; set; }

    /// <summary>
    /// 上传允许的文件格式
    /// </summary>
    public string[] AllowExtensions { get; set; }

    /// <summary>
    /// 文件是否以 Base64 的形式上传
    /// </summary>
    public bool Base64 { get; set; }

    /// <summary>
    /// Base64 字符串所表示的文件名
    /// </summary>
    public string Base64Filename { get; set; }
}

public class UploadResult
{
    public UploadState State { get; set; }
    public string Url { get; set; }
    public string OriginFileName { get; set; }

    public string ErrorMessage { get; set; }
}

public enum UploadState
{
    NetworkError = -4,
    FileAccessError = -3,
    TypeNotAllow = -2,
    SizeLimitExceed = -1,
    Success = 0,
    Unknown = 1
}