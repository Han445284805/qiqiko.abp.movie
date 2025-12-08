using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Qiqiko.Abp.Movie;

public class MovieDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly QiqikoMovieTestData _testData;
    private readonly IMovieRepository _qiqikoMovieRepository;
    private readonly IMovieTypeRepository _qiqikoMovieTypeRepository;
    private readonly IEpisodeRepository _qiqikoMovieEpisodeRepository;

    public MovieDataSeedContributor(QiqikoMovieTestData testData, IMovieRepository qiqikoMovieRepository, IMovieTypeRepository qiqikoMovieTypeRepository, IEpisodeRepository qiqikoMovieEpisodeRepository)
    {
        _testData = testData;
        _qiqikoMovieRepository = qiqikoMovieRepository;
        _qiqikoMovieTypeRepository = qiqikoMovieTypeRepository;
        _qiqikoMovieEpisodeRepository = qiqikoMovieEpisodeRepository;
    }

    public async Task SeedAsync(DataSeedContext context)
    {
        await _qiqikoMovieTypeRepository.InsertAsync(new MovieType(_testData.MovieTypeId, 0, "测试影片分类"));
        await _qiqikoMovieRepository.InsertAsync(new Movie(_testData.MovieId, 0, "测试影片", "多啦A萌,赤夜萌香", "孙悟空", _testData.MovieTypeId, 51, "动漫", Language.zh_Hans.ToString(), "这是一个种子影片"));
        await _qiqikoMovieEpisodeRepository.InsertAsync(new Episode‌(_testData.MovieEpisodeId, _testData.MovieId, 0, "第1集", "这是第一集"));
        for (int i = 0; i < 50; i++)
        {
            await _qiqikoMovieRepository.InsertAsync(new Movie(Guid.NewGuid(), i + 1, $"测试影片{i}", "多啦A萌,赤夜萌香", null, _testData.MovieTypeId, 1, "动漫", Language.zh_Hans.ToString(), "这是一个种子影片"));
        }
        for (int i = 0; i < 50; i++)
        {
            await _qiqikoMovieTypeRepository.InsertAsync(new MovieType(Guid.NewGuid(), i + 1, $"测试影片分类{i}"));
        }
        for (int i = 0; i < 50; i++)
        {
            await _qiqikoMovieEpisodeRepository.InsertAsync(new Episode‌(Guid.NewGuid(), _testData.MovieId, i + 1, $"第{i + 2}集", $"这是第{i + 2}集"));
        }
    }
}
