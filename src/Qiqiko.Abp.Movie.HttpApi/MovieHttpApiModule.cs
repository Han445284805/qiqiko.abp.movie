using Localization.Resources.AbpUi;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Qiqiko.Abp.Movie.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace Qiqiko.Abp.Movie;

[DependsOn(
    typeof(MovieApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class MovieHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(MovieHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<MovieResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });

        // 如果在 Program.cs 中配置
        context.Services.Configure<FormOptions>(options =>
        {
            // 设置最大表单数据长度，例如 500MB (单位：字节)
            options.MultipartBodyLengthLimit = 5242880000;
            // 可选：设置单个表单项值的长度限制
            options.ValueLengthLimit = int.MaxValue;
        });

    }
}
