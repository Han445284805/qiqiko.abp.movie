基于abp vnext 10.0.0-rc.2版本的视频切片模块 \r\n
没有使用abp vnext 10.0.0稳定版是因为我使用的是postgres数据库，abp用的postgres的10.0.0竟然引用了10.0.0-rc.1的包，导致如果用稳定版，迁移文件会失败
如果你没有使用postgres数据库，可以对当前代码变更版本包后使用
我也没有发布低级版本包。如果有使用低级版本包的需求，请同样直接使用源代码来降级

基础包：
Qiqiko.Abp.Movie.Application \r\n
Qiqiko.Abp.Movie.HttpApi（如果不需要包自带的路由api，可以不引用）\r\n
Qiqiko.Abp.Movie.EntityFrameworkCore \r\n
Qiqiko.Abp.Movie.Application.Contracts（可选） \r\n
Qiqiko.Abp.Movie.Domain（可选） \r\n
Qiqiko.Abp.Movie.Domain.Shared（可选） \r\n
Qiqiko.Abp.Movie.HttpApi.Client（可选） \r\n

配置：
1. 设置上传包大小（可选配置）
 // 设置上传包大小，已在Qiqiko.Abp.Movie.Application中配置，默认可以不设置，如果需要修改， 可在 Program.cs 中配置  
 context.Services.Configure<FormOptions>(options =>
 {
     // 设置最大表单数据长度，例如 500MB (单位：字节)
     options.MultipartBodyLengthLimit = 5242880000;
     // 可选：设置单个表单项值的长度限制
     options.ValueLengthLimit = int.MaxValue;
 });

2.指定文件存储地址：必选
//指定临时文件和切片后文件地址
  Configure<MovieOptions>(builder =>
 {
     builder.TempBasePath = "C:\\Movie\\TempBase";
     builder.M3u8PBasePath = "C:\\Movie\\M3u8Base";
 });

3.对应包中引入模块：
typeof(MovieApplicationModule)，typeof(MovieEntityFrameworkCoreModule)，typeof(MovieHttpApiModule)

4.efcore中继承迁移配置
[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ReplaceDbContext(typeof(IMovieDbContext))]
[ConnectionStringName("Default")]
public class MediaDbContext :
    AbpDbContext<MediaDbContext>,
    ITenantManagementDbContext,
    IIdentityDbContext, IMovieDbContext
{
   public DbSet<Movie> Movies { get; set; }
   public DbSet<Episode‌> MovieEpisodes { get; set; }
   public DbSet<MovieType> MovieTypes { get; set; }

     protected override void OnModelCreating(ModelBuilder builder)
  {
      base.OnModelCreating(builder);
      /* Include modules to your migration db context */
      ...
      builder.ConfigureMovie();
      ...
}
}

5. 进行数据迁移


6.docker部署需要在dockerfile中加入ffmepg的相关配置（后续会有配置文档）

后续还有图片和声音，开发中...

答疑qq：445284805
 
