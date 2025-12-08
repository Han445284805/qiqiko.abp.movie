using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Qiqiko.Abp.Movie.BackgroundJobs;
using Qiqiko.Abp.Movie.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Domain.Repositories;

namespace Qiqiko.Abp.Movie.Episodes
{
    public class EpisodeAppService : CrudAppService<Episode‌, Episode‌Dto, Guid, Episode‌PageRequestDto, CreateUpdateEpisode‌Dto>, IEpisode‌AppService
    {
        private readonly IRepository<Movie, Guid> _movieRepository;
        private readonly IOptions<MovieOptions> options;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public EpisodeAppService(IRepository<Episode‌, Guid> repository, IRepository<Movie, Guid> movieRepository, IOptions<MovieOptions> options, IBackgroundJobManager backgroundJobManager) : base(repository)
        {
            LocalizationResource = typeof(MovieResource);
            ObjectMapperContext = typeof(MovieApplicationModule);
            _movieRepository = movieRepository;
            this.options = options;
            _backgroundJobManager = backgroundJobManager;
        }

        public override async Task<EpisodeDto> CreateAsync(CreateUpdateEpisodeDto input)
        {
            var movie = await _movieRepository.GetAsync(input.MovieId);
            movie.EpisodeCount += 1;
            await _movieRepository.UpdateAsync(movie);
            return await base.CreateAsync(input);
        }

        public async override Task DeleteAsync(Guid id)
        {
            var episode = await Repository.GetAsync(id);
            var movie = await _movieRepository.GetAsync(episode.MovieId);
            movie.EpisodeCount -= 1;
            await _movieRepository.UpdateAsync(movie);
            await base.DeleteAsync(id);
        }

        protected override async Task<IQueryable<Episode>> CreateFilteredQueryAsync(EpisodePageRequestDto input)
        {
            var queryable = await base.CreateFilteredQueryAsync(input);
            return queryable.WhereIf(input.MovieId != Guid.Empty, e => e.MovieId == input.MovieId);
        }

        public async Task<EpisodeDto> UploadEpisodeAsync(Guid id, UploadEpisodeDto input)
        {
            // 确保临时目录存在
            var tempBasePath = options.Value.TempBasePath ?? Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "temp-movie");

            if (!Directory.Exists(tempBasePath))
                Directory.CreateDirectory(tempBasePath);

            // 生成文件名（包含原始扩展名）
            var fileName = $"{id}{Path.GetExtension(input.File.FileName)}";
            var filePath = Path.Combine(tempBasePath, fileName);

            // 先计算文件哈希，再保存文件
            var fileHash = await ComputeFileHashAsync(input.File);

            // 保存文件到临时路径
            await SaveFileToTempPathAsync(input.File, filePath);

            // 获取实体
            var episode = await Repository.GetAsync(id);
            // 更新实体信息
            episode.SetPath(fileHash, fileName, input.ConcurrencyStamp);
            await Repository.UpdateAsync(episode, true);
            await _backgroundJobManager.EnqueueAsync(new FFmpegEpisodeArgs
            {
                EpisodeId = episode.Id
            });
            return ObjectMapper.Map<Episode, EpisodeDto>(episode);
        }

        /// <summary>
        /// 计算文件的哈希值
        /// </summary>
        private static async Task<string> ComputeFileHashAsync(IFormFile file)
        {
            using var algorithm = SHA256.Create(); // 可根据需要改为MD5.Create()
                                                   // 创建内存流来读取文件内容
            using var memoryStream = new MemoryStream();
            // 将IFormFile复制到内存流
            file.CopyTo(memoryStream);
            memoryStream.Position = 0; // 重置流位置

            // 计算哈希值
            byte[] hashBytes = algorithm.ComputeHash(memoryStream);
            return Convert.ToHexStringLower(hashBytes);
        }

        /// <summary>
        /// 将文件保存到临时路径
        /// </summary>
        private static async Task SaveFileToTempPathAsync(IFormFile file, string filePath)
        {
            // 如果目标文件已存在，先删除
            if (File.Exists(filePath))
                File.Delete(filePath);

            using var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write,
                                              FileShare.None, 4096, FileOptions.Asynchronous);
            await file.CopyToAsync(stream);
        }
    }
}
