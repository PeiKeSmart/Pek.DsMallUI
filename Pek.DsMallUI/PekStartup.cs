﻿using Pek.Infrastructure;
using Pek.VirtualFileSystem;

namespace Pek.DsMallUI;

/// <summary>
/// 表示应用程序启动时配置服务和中间件的对象
/// </summary>
public partial class PekStartup : IPekStartup
{
    /// <summary>
    /// 配置添加的中间件的使用
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    public void Configure(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// 添加并配置任何中间件
    /// </summary>
    /// <param name="services">服务描述符集合</param>
    /// <param name="configuration">应用程序的配置</param>
    /// <param name="webHostEnvironment"></param>
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
    {
    }

    /// <summary>
    /// 配置虚拟文件系统
    /// </summary>
    /// <param name="options">虚拟文件配置</param>
    public void ConfigureVirtualFileSystem(DHVirtualFileSystemOptions options)
    {
        //XTrace.WriteLine($"[PekStartup.ConfigureVirtualFileSystem]配置虚拟文件系统");
        options.FileSets.AddEmbedded<PekStartup>(typeof(PekStartup).Namespace);
        //XTrace.WriteLine($"[PekStartup.ConfigureVirtualFileSystem]配置虚拟文件系统：{options.FileSets.ToJson()}");
        // options.FileSets.Add(new EmbeddedFileSet(item.Assembly, item.Namespace));
    }

    /// <summary>
    /// 注册路由
    /// </summary>
    /// <param name="endpoints">路由生成器</param>
    public void UseDHEndpoints(IEndpointRouteBuilder endpoints)
    {
    }

    /// <summary>
    /// 将区域路由写入数据库
    /// </summary>
    public void ConfigureArea()
    {

    }

    /// <summary>
    /// 调整菜单
    /// </summary>
    public void ChangeMenu()
    {

    }

    /// <summary>
    /// 升级处理逻辑
    /// </summary>
    public void Update()
    {

    }

    /// <summary>
    /// 配置使用添加的中间件
    /// </summary>
    /// <param name="application">用于配置应用程序的请求管道的生成器</param>
    public void ConfigureMiddleware(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// UseRouting前执行的数据
    /// </summary>
    /// <param name="application"></param>
    public void BeforeRouting(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// UseAuthentication或者UseAuthorization后面 Endpoints前执行的数据
    /// </summary>
    /// <param name="application"></param>
    public void AfterAuth(IApplicationBuilder application)
    {
    }

    /// <summary>
    /// 处理数据
    /// </summary>
    public void ProcessData()
    {
    }

    /// <summary>
    /// 获取此启动配置实现的顺序
    /// </summary>
    public Int32 StartupOrder => 999; //常见服务应在错误处理程序之后加载

    /// <summary>
    /// 获取此启动配置实现的顺序。主要针对ConfigureMiddleware、UseRouting前执行的数据、UseAuthentication或者UseAuthorization后面 Endpoints前执行的数据
    /// </summary>
    public Int32 ConfigureOrder => 0;
}