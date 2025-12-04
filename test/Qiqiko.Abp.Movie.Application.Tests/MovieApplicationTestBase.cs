using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie;

/* Inherit from this class for your application layer tests.
 * See SampleAppService_Tests for example.
 */
public abstract class MovieApplicationTestBase<TStartupModule> : MovieTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

    public void SetRoute(string path, string method, string id)
    {
        // 模拟HttpContext
        var httpContext = new DefaultHttpContext();
        httpContext.Request.Method = path;
        httpContext.Request.Path = method;

        // 最简单直接的方式：操作RouteValues字典
        // 确保在获取RouteData之前初始化RouteValues
        httpContext.Request.RouteValues = new RouteValueDictionary();
        httpContext.Request.RouteValues["id"] = id;

        // 获取并设置IHttpContextAccessor
        var httpContextAccessor = GetRequiredService<IHttpContextAccessor>();
        httpContextAccessor.HttpContext = httpContext;
    }
    protected override void AfterAddApplication(IServiceCollection services)
    {
        // 确保HTTP上下文访问器被注册
        services.AddHttpContextAccessor();
    }
}
