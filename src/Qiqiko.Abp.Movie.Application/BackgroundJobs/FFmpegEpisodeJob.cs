using FFMpegCore;
using FFMpegCore.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Qiqiko.Abp.Movie.BackgroundJobs
{
    public class FFmpegEpisodeJob : AsyncBackgroundJob<FFmpegEpisodeArgs>, ITransientDependency
    {
        private readonly IRepository<Episode, Guid> repository;
        private readonly IOptions<MovieOptions> options;

        public FFmpegEpisodeJob(IRepository<Episode, Guid> repository, IOptions<MovieOptions> options)
        {
            this.repository = repository;
            this.options = options;
        }

        [UnitOfWork]
        public override async Task ExecuteAsync(FFmpegEpisodeArgs args)
        {
            var episode = await repository.FirstOrDefaultAsync(r => r.Id == args.EpisodeId);
            if (episode == null) return;
            if (episode.TempPath.IsNullOrWhiteSpace()) return;
            if (!episode.M3u8Path.IsNullOrWhiteSpace()) return;
            if (episode.FFmpegStatus == FFmpegStatus.Finished) return;

            //准备文件路径
            var tempBasePath = options.Value.TempBasePath ?? Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "temp-movie");
            var filePath = Path.Combine(tempBasePath, episode.TempPath);
  
            //检查是否有相同hash且已经转换成功的
            var goodEpisode = await repository.FirstOrDefaultAsync(r=>r.Id != episode.Id 
                                && r.OriginalHash == episode.OriginalHash
                                && r.FFmpegStatus== FFmpegStatus.Finished 
                                && r.M3u8Path != null && r.M3u8Path !=string.Empty);
            //有相同hash且已经转换成功的，直接复用
            if (goodEpisode != null)
            {
                episode.CompletFFmpeg(goodEpisode.M3u8Path);
                await repository.UpdateAsync(episode,true);
                File.Delete(filePath);
                return;
            }
            //文件不存在，标记为未上传
            if (!File.Exists(filePath))
            {
                episode.ChangeUnLoad();
                await repository.UpdateAsync(episode, true);
                File.Delete(filePath);
                return;
            }
            //调用ffmpeg进行m3u8转换
            await RunFFmpegAsync(filePath, episode.Id.ToString("N"));
            episode.CompletFFmpeg($"{Path.Combine( episode.Id.ToString("N"), "index.m3u8")}");
            await repository.UpdateAsync(episode, true);
        }

        private async Task RunFFmpegAsync(string inputFilePath,string episodeId)
        {
            var outputDirectory = options.Value.M3u8PBasePath ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "movie");
            outputDirectory = Path.Combine(outputDirectory, episodeId);
            Directory.Delete(outputDirectory,true);
            Directory.CreateDirectory(outputDirectory);

            var outputM3u8File = Path.Combine(outputDirectory, "index.m3u8");

            try
            {
                // 使用FFMpegCore执行转换
                await FFMpegArguments
                    .FromFileInput(inputFilePath)
                    .OutputToFile(outputM3u8File, overwrite: true, options => options
                        .WithVideoCodec(VideoCodec.LibX264)
                        .WithAudioCodec(AudioCodec.Aac)
                        .WithConstantRateFactor(21)
                        .WithVideoFilters(filterOptions => filterOptions.Scale(VideoSize.Hd))
                        .WithCustomArgument("-hls_time 3 -hls_list_size 0 -f hls"))
                    .ProcessAsynchronously();

                // 验证生成的文件
                if (File.Exists(outputM3u8File))
                {
                    // 删除临时文件
                    File.Delete(inputFilePath);
                }
                else
                {
                    throw new FileNotFoundException("M3U8文件生成失败");
                }
            }
            catch (Exception ex)
            {
                // 记录日志
                Logger?.LogError(ex, "视频切片处理失败 EpisodeId: {EpisodeId}", episodeId);
                throw new FileNotFoundException("M3U8文件生成失败");
            }
        }
    }
}
