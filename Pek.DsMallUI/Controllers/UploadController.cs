﻿using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

using Microsoft.AspNetCore.Mvc;

using NewLife;

using Pek.DsMallUI.UEditor;
using Pek.Mime;
using Pek.NCube;

using XCode.Membership;

namespace Pek.DsMallUI.Controllers;

/// <summary>
/// 文件上传
/// </summary>
[ApiExplorerSettings(IgnoreApi = true)]
public class UploadController : PekBaseControllerX
{
    /// <summary>
    /// 
    /// </summary>
    public IWebHostEnvironment? HostEnvironment { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="data"></param>
    /// <param name="isTrue"></param>
    /// <param name="message"></param>
    /// <returns></returns>
    public IActionResult ResultData(Object data, Boolean isTrue = true, String message = "")
    {
        return Content(JsonSerializer.Serialize(new
        {
            Success = isTrue,
            Message = message,
            Data = data
        }, new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        }), "application/json", Encoding.UTF8);
    }

    #region Word上传转码

    ///// <summary>
    ///// 上传Word转码
    ///// </summary>
    ///// <returns></returns>
    //[HttpPost]
    //public async Task<ActionResult> UploadWord(CancellationToken cancellationToken)
    //{
    //    var form = await Request.ReadFormAsync(cancellationToken);
    //    var files = form.Files;
    //    if (files.Count <= 0)
    //    {
    //        return ResultData(null, false, "请先选择您需要上传的文件!");
    //    }

    //    var file = files[0];
    //    string fileName = file.FileName;
    //    if (!Regex.IsMatch(Path.GetExtension(fileName), "doc|docx"))
    //    {
    //        return ResultData(null, false, "文件格式不支持，只能上传doc或者docx的文档!");
    //    }

    //    var html = await SaveAsHtml(file);
    //    if (html.Length < 10)
    //    {
    //        return ResultData(null, false, "读取文件内容失败，请检查文件的完整性，建议另存后重新上传！");
    //    }

    //    if (html.Length > 1000000)
    //    {
    //        return ResultData(null, false, "文档内容超长，服务器拒绝接收，请优化文档内容后再尝试重新上传！");
    //    }

    //    return ResultData(new
    //    {
    //        Title = Path.GetFileNameWithoutExtension(fileName),
    //        Content = html
    //    });
    //}
    //private async Task<string> ConvertToHtml(IFormFile file)
    //{
    //    var docfile = Path.Combine(Environment.GetEnvironmentVariable("temp") ?? "upload", file.FileName);
    //    try
    //    {
    //        await using var ms = file.OpenReadStream();
    //        await using var fs = System.IO.File.Create(docfile, 1024, FileOptions.DeleteOnClose);
    //        await ms.CopyToAsync(fs);
    //        using var doc = WordprocessingDocument.Open(fs, true);
    //        var pageTitle = file.FileName;
    //        var part = doc.CoreFilePropertiesPart;
    //        if (part != null)
    //        {
    //            pageTitle ??= (string)part.GetXDocument().Descendants(DC.title).FirstOrDefault();
    //        }

    //        var settings = new HtmlConverterSettings()
    //        {
    //            PageTitle = pageTitle,
    //            FabricateCssClasses = false,
    //            RestrictToSupportedLanguages = false,
    //            RestrictToSupportedNumberingFormats = false,
    //            ImageHandler = imageInfo =>
    //            {
    //                var stream = new MemoryStream();
    //                imageInfo.Bitmap.Save(stream, imageInfo.Bitmap.RawFormat);
    //                var base64String = Convert.ToBase64String(stream.ToArray());
    //                return new XElement(Xhtml.img, new XAttribute(NoNamespace.src, $"data:{imageInfo.ContentType};base64," + base64String), imageInfo.ImgStyleAttribute, imageInfo.AltText != null ? new XAttribute(NoNamespace.alt, imageInfo.AltText) : null);
    //            }
    //        };
    //        var htmlElement = HtmlConverter.ConvertToHtml(doc, settings);
    //        var html = new XDocument(new XDocumentType("html", null, null, null), htmlElement);
    //        var htmlString = html.ToString(SaveOptions.DisableFormatting);
    //        return htmlString;
    //    }
    //    finally
    //    {
    //        if (System.IO.File.Exists(docfile))
    //        {
    //            System.IO.File.Delete(docfile);
    //        }
    //    }
    //}

    //private async Task<string> SaveAsHtml(IFormFile file)
    //{
    //    var html = await ConvertToHtml(file);
    //    var context = BrowsingContext.New(Configuration.Default);
    //    var doc = context.OpenAsync(req => req.Content(html)).Result;
    //    var body = doc.Body;
    //    var nodes = body.GetElementsByTagName("img");
    //    if (nodes != null)
    //    {
    //        foreach (var img in nodes)
    //        {
    //            var attr = img.Attributes["src"].Value;
    //            var strs = attr.Split(",");
    //            var base64 = strs[1];
    //            var bytes = Convert.FromBase64String(base64);
    //            var ext = strs[0].Split(";")[0].Split("/")[1];
    //            await using var image = new MemoryStream(bytes);
    //            var imgFile = $"{new Snowflake().NewId()}.{ext}";
    //            var path = Path.Combine(HostEnvironment.WebRootPath, CommonHelpers.SystemSettings.GetOrAdd("UploadPath", "upload").Trim('/', '\\'), "images", imgFile);
    //            var dir = Path.GetDirectoryName(path);
    //            Directory.CreateDirectory(dir);
    //            await using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    //            await image.CopyToAsync(fs);
    //            img.Attributes["src"].Value = path.Substring(HostEnvironment.WebRootPath.Length).Replace("\\", "/");
    //        }
    //    }

    //    return body.InnerHtml.HtmlSantinizerStandard().HtmlSantinizerCustom(attributes: new[] { "dir", "lang" });
    //}

    private static async Task SaveFile(IFormFile file, String path)
    {
        var dir = Path.GetDirectoryName(path);

        if (dir.IsNullOrWhiteSpace()) return;

        Directory.CreateDirectory(dir);
        using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        await file.CopyToAsync(fs).ConfigureAwait(false);
    }

    #endregion

    /// <summary>
    /// 文件下载
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    [Route("downloads")]
    [Route("downloads/{**path}")]
    public ActionResult Download(String path)
    {
        if (String.IsNullOrEmpty(path)) return Content("null");
        var file = Path.Combine(HostEnvironment?.WebRootPath!, CommonHelper.SystemSettings.GetOrAdd("UploadPath", "upload").Trim('/', '\\'), path.Trim('.', '/', '\\'));
        if (System.IO.File.Exists(file))
        {
            return this.ResumePhysicalFile(file, Path.GetFileName(file));
        }

        return Content("null");
    }

    /// <summary>
    /// UEditor文件上传处理
    /// </summary>
    /// <returns></returns>
    [Route("fileuploader")]
    public async Task<ActionResult> UeditorFileUploader()
    {
        var action = Request.Query["action"].ToString() switch //通用
        {
            "config" => (Pek.DsMallUI.UEditor.Handler)new ConfigHandler(HttpContext),
            "uploadimage" => new UploadHandler(HttpContext, new UploadConfig()
            {
                AllowExtensions = UeditorConfig.GetStringList("imageAllowFiles"),
                PathFormat = "/" + CommonHelper.SystemSettings.GetOrAdd("UploadPath", DHSetting.Current.UploadPath).Trim('/', '\\') + UeditorConfig.GetString("imagePathFormat"),
                SizeLimit = UeditorConfig.GetInt("imageMaxSize"),
                UploadFieldName = UeditorConfig.GetString("imageFieldName")
            }),
            "uploadscrawl" => new UploadHandler(HttpContext, new UploadConfig()
            {
                AllowExtensions =
                [
                    ".png"
                ],
                PathFormat = "/" + CommonHelper.SystemSettings.GetOrAdd("UploadPath", DHSetting.Current.UploadPath).Trim('/', '\\') + UeditorConfig.GetString("scrawlPathFormat"),
                SizeLimit = UeditorConfig.GetInt("scrawlMaxSize"),
                UploadFieldName = UeditorConfig.GetString("scrawlFieldName"),
                Base64 = true,
                Base64Filename = "scrawl.png"
            }),
            "catchimage" => new CrawlerHandler(HttpContext),
            _ => new NotSupportedHandler(HttpContext)
        };

        if (ManageProvider.User != null && ManageProvider.User.Enable && ManageProvider.User.Ex1 == 1 && !ManageProvider.User.Role.Permission.IsNullOrWhiteSpace())
        {
            switch (Request.Query["action"])//管理员用
            {
                //case "uploadvideo":
                //    action = new UploadHandler(context, new UploadConfig()
                //    {
                //        AllowExtensions = UeditorConfig.GetStringList("videoAllowFiles"),
                //        PathFormat =  "/" + CommonHelper.SystemSettings.GetOrAdd("UploadPath", Setting.Current.UploadPath) + UeditorConfig.GetString("videoPathFormat"),
                //        SizeLimit = UeditorConfig.GetInt("videoMaxSize"),
                //        UploadFieldName = UeditorConfig.GetString("videoFieldName")
                //    });
                //    break;
                case "uploadfile":
                    action = new UploadHandler(HttpContext, new UploadConfig()
                    {
                        AllowExtensions = UeditorConfig.GetStringList("fileAllowFiles"),
                        PathFormat = "/" + CommonHelper.SystemSettings.GetOrAdd("UploadPath", DHSetting.Current.UploadPath).Trim('/', '\\') + UeditorConfig.GetString("filePathFormat"),
                        SizeLimit = UeditorConfig.GetInt("fileMaxSize"),
                        UploadFieldName = UeditorConfig.GetString("fileFieldName")
                    });
                    break;
                    //case "listimage":
                    //    action = new ListFileManager(context, CommonHelper.SystemSettings.GetOrAdd("UploadPath", Setting.Current.UploadPath) + UeditorConfig.GetString("imageManagerListPath"), UeditorConfig.GetStringList("imageManagerAllowFiles"));
                    //    break;
                    //case "listfile":
                    //    action = new ListFileManager(context, CommonHelper.SystemSettings.GetOrAdd("UploadPath", Setting.Current.UploadPath) + UeditorConfig.GetString("fileManagerListPath"), UeditorConfig.GetStringList("fileManagerAllowFiles"));
                    //    break;
            }
        }

        String result = await action.Process().ConfigureAwait(false);
        return Content(result, MimeTypes.ApplicationJson);
    }

    ///// <summary>
    ///// 上传文件
    ///// </summary>
    ///// <param name="imagebedClient"></param>
    ///// <param name="file"></param>
    ///// <param name="cancellationToken"></param>
    ///// <returns></returns>
    //[HttpPost("upload")/*, ApiExplorerSettings(IgnoreApi = false)*/]
    //public async Task<ActionResult> UploadFile([FromServices] ImagebedClient imagebedClient, IFormFile file, CancellationToken cancellationToken)
    //{
    //    string path;
    //    string filename = SnowFlake.GetInstance().GetUniqueId() + Path.GetExtension(file.FileName);
    //    var pathBase = CommonHelpers.SystemSettings.GetOrAdd("UploadPath", "upload").Trim('/', '\\');
    //    switch (file.ContentType)
    //    {
    //        case var _ when file.ContentType.StartsWith("image"):
    //            {
    //                await using var stream = file.OpenReadStream();
    //                var (url, success) = await imagebedClient.UploadImage(stream, file.FileName, cancellationToken);
    //                if (success)
    //                {
    //                    return ResultData(url);
    //                }

    //                path = Path.Combine(HostEnvironment.WebRootPath, pathBase, "images", filename);
    //                var dir = Path.GetDirectoryName(path);
    //                Directory.CreateDirectory(dir);
    //                await using var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
    //                await file.CopyToAsync(fs);
    //                break;
    //            }
    //        case var _ when file.ContentType.StartsWith("audio") || file.ContentType.StartsWith("video"):
    //            path = Path.Combine(HostEnvironment.WebRootPath, pathBase, "media", filename);
    //            break;
    //        case var _ when file.ContentType.StartsWith("text") || (ContentType.Doc + "," + ContentType.Xls + "," + ContentType.Ppt + "," + ContentType.Pdf).Contains(file.ContentType):
    //            path = Path.Combine(HostEnvironment.WebRootPath, pathBase, "docs", filename);
    //            break;
    //        default:
    //            path = Path.Combine(HostEnvironment.WebRootPath, pathBase, "files", filename);
    //            break;
    //    }
    //    try
    //    {
    //        await SaveFile(file, path);
    //        return ResultData(path.Substring(HostEnvironment.WebRootPath.Length).Replace("\\", "/"));
    //    }
    //    catch (Exception e)
    //    {
    //        XTrace.WriteException(e);
    //        return ResultData(null, false, "文件上传失败！");
    //    }
    //}
}