using Qiqiko.Abp.Movie.Movies;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Volo.Abp.Validation;
using Xunit;

namespace Qiqiko.Abp.Movie.Episodes;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class EpisodeAppServiceTests<TStartupModule> : MovieApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private IEpisodeAppService _episodeAppService;
    private IMovieAppService _movieAppService;
    private QiqikoMovieTestData _qiqikoMovieTest;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public EpisodeAppServiceTests()
    {
        _episodeAppService = GetRequiredService<IEpisodeAppService>();
        _movieAppService = GetRequiredService<IMovieAppService>();
        _qiqikoMovieTest = GetRequiredService<QiqikoMovieTestData>();
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
    }

    [Fact]
    public async Task 创建影片分集时名称重复应抛出异常()
    {
        // Arrange - 准备一个与现有影片分集同名的DTO
        var createEpisodeDto = new CreateUpdateEpisodeDto
        {
            Name = "第1集", // 使用一个可能重复的名称
            Index = 1,
            MovieId = _qiqikoMovieTest.MovieId,
            Description = "测试描述"
        };

        // Assert
        var actualException = await Should.ThrowAsync<AbpValidationException>(async () =>
        {
            await _episodeAppService.CreateAsync(createEpisodeDto);
        });

        actualException.ValidationErrors.Count.ShouldBe(1);
        actualException.ValidationErrors[0].ErrorMessage.ShouldBe("分集第1集已存在。");
    }
    [Fact]
    public async Task 创建影片分集时名称唯一应成功()
    {
        // Arrange - 使用GUID确保名称唯一
        var uniqueName = "唯一影片分集名称_" + Guid.NewGuid();
        var createEpisodeDto = new CreateUpdateEpisodeDto
        {
            MovieId = _qiqikoMovieTest.MovieId,
            Name = uniqueName,
            Index = 1,
        };

        // Act
        var result = await _episodeAppService.CreateAsync(createEpisodeDto);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(uniqueName);
    }
    [Fact]
    public async Task 创建影片分集时影片分集数增加()
    {
        // Arrange - 使用GUID确保名称唯一
        var uniqueName = "唯一影片分集名称_" + Guid.NewGuid();
        var createEpisodeDto = new CreateUpdateEpisodeDto
        {
            MovieId = _qiqikoMovieTest.MovieId,
            Name = uniqueName,
            Index = 1,
        };

        // Act
        var result = await _episodeAppService.CreateAsync(createEpisodeDto);
        var movie = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);

        // Assert
        movie.EpisodeCount.ShouldBe(52);
    }
    [Fact]
    public async Task 创建影片分集错误的影片Id应抛出异常()
    {
        // Arrange - 准备一个与现有影片分集同名的DTO
        var createEpisodeDto = new CreateUpdateEpisodeDto
        {
            Name = "第111集", // 使用一个可能重复的名称
            Index = 1,
            MovieId = _qiqikoMovieTest.MovieTypeId,
            Description = "测试描述"
        };

        // Assert
        var actualException = await Should.ThrowAsync<AbpValidationException>(async () =>
        {
            await _episodeAppService.CreateAsync(createEpisodeDto);
        });

        actualException.ValidationErrors.Count.ShouldBe(1);
        actualException.ValidationErrors[0].ErrorMessage.ShouldBe("影片不存在或已被删除。");
    }

    [Fact]
    public async Task 修改影片分集时名称重复应抛出异常()
    {

        //设置路由参数
        SetRoute("PUT", $"/api/movie-episode/{_qiqikoMovieTest.MovieEpisodeId}", _qiqikoMovieTest.MovieEpisodeId.ToString());

        var entity = await _episodeAppService.GetAsync(_qiqikoMovieTest.MovieEpisodeId);
        var modifyEpisodeDto = new CreateUpdateEpisodeDto
        {
            Name = "第2集",
            Index = 2,
            MovieId = entity.MovieId,
            Description = entity.Description,
            ConcurrencyStamp = entity.ConcurrencyStamp
        };
        // Assert
        var actualException = await Should.ThrowAsync<AbpValidationException>(async () =>
        {
            await _episodeAppService.UpdateAsync(_qiqikoMovieTest.MovieEpisodeId, modifyEpisodeDto);
        });

        actualException.ValidationErrors.Count.ShouldBe(1);
        actualException.ValidationErrors[0].ErrorMessage.ShouldBe("分集第2集已存在。");
    }
    [Fact]
    public async Task 修改影片分集时名称不重复应成功()
    {
        //设置路由参数
        SetRoute("PUT", $"/api/movie-type/{_qiqikoMovieTest.MovieEpisodeId}", _qiqikoMovieTest.MovieEpisodeId.ToString());

        // Arrange
        var entity = await _episodeAppService.GetAsync(_qiqikoMovieTest.MovieEpisodeId);
        var modifyEpisodeDto = new CreateUpdateEpisodeDto
        {
            Name = "第78集",
            Index = 2,
            MovieId = entity.MovieId,
            Description = entity.Description,
            ConcurrencyStamp = entity.ConcurrencyStamp
        };

        // Act
        var result = await _episodeAppService.UpdateAsync(_qiqikoMovieTest.MovieEpisodeId, modifyEpisodeDto);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(modifyEpisodeDto.Name);
    }
    [Fact]
    public async Task 修改影片分集时错误路由应抛出异常()
    {

        //设置路由参数
        SetRoute("PUT", $"/api/movie-episode/sssss", "sssss");

        var entity = await _episodeAppService.GetAsync(_qiqikoMovieTest.MovieEpisodeId);
        var modifyEpisodeDto = new CreateUpdateEpisodeDto
        {
            Name = "第2222集",
            Index = 2,
            MovieId = entity.MovieId,
            Description = entity.Description,
            ConcurrencyStamp = entity.ConcurrencyStamp
        };
        // Assert
        var actualException = await Should.ThrowAsync<AbpValidationException>(async () =>
        {
            await _episodeAppService.UpdateAsync(_qiqikoMovieTest.MovieEpisodeId, modifyEpisodeDto);
        });

        actualException.ValidationErrors.Count.ShouldBe(1);
        actualException.ValidationErrors[0].ErrorMessage.ShouldBe("路由主键类型错误。");
    }
    [Fact]
    public async Task 修改影片分集分集不存在应抛出异常()
    {
        //设置路由参数
        SetRoute("PUT", $"/api/movie-episode/{Guid.NewGuid()}", Guid.NewGuid().ToString());

        var entity = await _episodeAppService.GetAsync(_qiqikoMovieTest.MovieEpisodeId);
        var modifyEpisodeDto = new CreateUpdateEpisodeDto
        {
            Name = "第2222集",
            Index = 2,
            MovieId = entity.MovieId,
            Description = entity.Description,
            ConcurrencyStamp = entity.ConcurrencyStamp
        };
        // Assert
        var actualException = await Should.ThrowAsync<AbpValidationException>(async () =>
        {
            await _episodeAppService.UpdateAsync(_qiqikoMovieTest.MovieEpisodeId, modifyEpisodeDto);
        });

        actualException.ValidationErrors.Count.ShouldBe(1);
        actualException.ValidationErrors[0].ErrorMessage.ShouldBe("分集不存在或已被删除。");
    }

    [Fact]
    public async Task 删除影片分集对象()
    {
        //Act
        await _episodeAppService.DeleteAsync(_qiqikoMovieTest.MovieEpisodeId);

        var actualException = await Should.ThrowAsync<EntityNotFoundException>(async () =>
        {
            await _episodeAppService.GetAsync(_qiqikoMovieTest.MovieEpisodeId);
        });

        // Assert
        actualException.Message.ShouldBe($"There is no such an entity. Entity type: Qiqiko.Abp.Movie.Episode, id: {_qiqikoMovieTest.MovieEpisodeId}");
    }
    [Fact]
    public async Task 删除影片分集对象应减少影片集数()
    {
        //Act
        await _episodeAppService.DeleteAsync(_qiqikoMovieTest.MovieEpisodeId);

        var movie = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);

        // Assert
        movie.EpisodeCount.ShouldBe(50);

    }

    [Fact]
    public async Task 查询影片分集对象()
    {
        //Act
        var result = await _episodeAppService.GetAsync(_qiqikoMovieTest.MovieEpisodeId);
        //Assert
        result.ShouldNotBeNull();
    }
    [Fact]
    public async Task 查询影片分集分页对象()
    {
        //Act
        var requestDto = new EpisodePageRequestDto
        {
            SkipCount = 5,
            MaxResultCount = 10,
            MovieId = _qiqikoMovieTest.MovieId,
            Sorting = " index "
        };
        var result = await _episodeAppService.GetListAsync(requestDto);
        //Assert
        result.Items.ShouldNotBeNull();
        result.Items.Count.ShouldBe(10);
    }
}
