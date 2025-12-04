using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace Qiqiko.Abp.Movie.EntityFrameworkCore;

public static class MovieDbContextModelCreatingExtensions
{
    public static void ConfigureMovie(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<MovieType>(b =>
        {
            b.ToTable(MovieDbProperties.DbTablePrefix + "MovieType", MovieDbProperties.DbSchema);
            b.Property(r=>r.Index).HasDefaultValue(0).HasMaxLength(2).HasComment("排序号");
            b.Property(r => r.Name).IsRequired().HasMaxLength(24).HasComment("分类名称");
            b.ConfigureByConvention();
            b.ApplyObjectExtensionMappings();
        });
        builder.Entity<Movie>(b =>
        {
            b.ToTable(MovieDbProperties.DbTablePrefix + "Movie", MovieDbProperties.DbSchema);
            b.Property(r => r.Index).HasDefaultValue(0).HasMaxLength(255).HasComment("排序号");
            b.Property(r => r.Name).IsRequired().HasMaxLength(255).HasComment("影片名称");
            b.Property(r => r.Performers).HasMaxLength(255).HasComment("演员");
            b.Property(r => r.Star).HasMaxLength(255).HasComment("主演");
            b.Property(r => r.MovieTypeId).HasComment("视频类型");
            b.Property(r => r.EpisodeCount).HasDefaultValue(0).HasMaxLength(3).HasComment("视频类型");
            b.Property(r => r.Tags).HasMaxLength(255).HasComment("标签");
            b.Property(r => r.Language).HasMaxLength(10).HasComment("语言");
            b.Property(r => r.Description).HasComment("描述");
            b.HasOne(r => r.MovieType).WithMany().HasForeignKey(r => r.MovieTypeId).IsRequired(false);
            b.ConfigureByConvention();
            b.ApplyObjectExtensionMappings();
        });
        builder.Entity<Episode‌>(b =>
        {
            b.ToTable(MovieDbProperties.DbTablePrefix + "MovieEpisode‌", MovieDbProperties.DbSchema);
            b.Property(r => r.MovieId).IsRequired().HasComment("影片Id");
            b.Property(r => r.Index).HasDefaultValue(0).HasMaxLength(3).HasComment("分集号");
            b.Property(r => r.Name).IsRequired().HasMaxLength(24).HasComment("分集名称");
            b.Property(r => r.TempPath).HasMaxLength(255).HasComment("临时文件路径");
            b.Property(r => r.OriginalHash).HasMaxLength(255).HasComment("原文件Hash");
            b.Property(r => r.M3u8Path).HasMaxLength(255).HasComment("m3u8路径");
            b.Property(r => r.FFmpegStatus).HasDefaultValue(FFmpegStatus.UnUpLoad).IsRequired().HasComment("切片状态");
            b.Property(r => r.Description).HasComment("描述");
            b.ConfigureByConvention();
            b.ApplyObjectExtensionMappings();
        });
        builder.TryConfigureObjectExtensions<MovieDbContext>();
    }
}
