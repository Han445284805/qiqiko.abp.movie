using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Xunit;

namespace Qiqiko.Abp.Movie.Movies;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class MovieAppServiceTests<TStartupModule> : MovieApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private IMovieAppService _movieAppService;
    private readonly IMovieRepository _qiqikoMovieRepository;
    private QiqikoMovieTestData _qiqikoMovieTest;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public MovieAppServiceTests()
    {
        _movieAppService = GetRequiredService<IMovieAppService>();
        _qiqikoMovieTest = GetRequiredService<QiqikoMovieTestData>();
        _qiqikoMovieRepository = GetRequiredService<IMovieRepository>();
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
    }

    [Fact]
    public async Task 创建影片时名称重复应抛出异常()
    {
        // Arrange - 准备一个与现有影片同名的DTO
        var createMovieDto = new CreateUpdateMovieDto
        {
            MovieTypeId = _qiqikoMovieTest.MovieTypeId,
            Name = "测试影片", // 使用一个可能重复的名称
            Description = "这是一个测试影片",
            Index = 1,
            Language = "zh-Hans",
            Performers = "演员A,演员B",
            Star = "主演A",
            Tags = "标签1,标签2"
        };

        // Act & Assert - 验证创建重复名称的影片时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieAppService.CreateAsync(createMovieDto);
        }, "创建同名影片时应抛出异常");
    }

    [Fact]
    public async Task 修改影片时名称重复应抛出异常()
    {
        //设置路由参数
        SetRoute("PUT", $"/api/movie/{_qiqikoMovieTest.MovieId}", _qiqikoMovieTest.MovieId.ToString());

        var entity = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);
        var modifyMovieDto = new CreateUpdateMovieDto
        {
            ConcurrencyStamp = entity.ConcurrencyStamp,
            MovieTypeId = _qiqikoMovieTest.MovieTypeId,
            Name = "测试影片2",
            Description = "Description123",
            Index = 2,
            Language = "zh",
            Performers = "演员6",
            Star = "主演7",
            Tags = "标签8"
        };
        // Act & Assert - 验证修改时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieAppService.UpdateAsync(_qiqikoMovieTest.MovieId, modifyMovieDto);
        }, "修改为重复名称时应抛出异常");
    }

    [Fact]
    public async Task 修改影片时影片类型不存在应抛出异常()
    {
        //设置路由参数
        SetRoute("PUT", $"/api/movie/{_qiqikoMovieTest.MovieId}", _qiqikoMovieTest.MovieId.ToString());

        var entity = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);
        var modifyMovieDto = new CreateUpdateMovieDto
        {
            ConcurrencyStamp = entity.ConcurrencyStamp,
            MovieTypeId = Guid.NewGuid(),
            Name = "测试影片" + Guid.NewGuid().ToString("N"),
            Description = "Description123",
            Index = 2,
            Language = "zh",
            Performers = "演员6",
            Star = "主演7",
            Tags = "标签8"
        };
        // Act & Assert - 验证修改时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieAppService.UpdateAsync(_qiqikoMovieTest.MovieId, modifyMovieDto);
        }, "修改影片时影片类型不存在应抛出异常");
    }

    [Fact]
    public async Task 修改影片时名称不重复应成功()
    {
        //设置路由参数
        SetRoute("PUT", $"/api/movie/{_qiqikoMovieTest.MovieId}", _qiqikoMovieTest.MovieId.ToString());
        // Arrange
        var entity = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);
        var modifyMovieDto = new CreateUpdateMovieDto
        {
            ConcurrencyStamp = entity.ConcurrencyStamp,
            MovieTypeId = _qiqikoMovieTest.MovieTypeId,
            Name = "唯一的新名称_" + Guid.NewGuid(), // 确保名称唯一
            Description = "Description123",
            Index = 2,
            Language = "zh",
            Performers = "演员6",
            Star = "主演7",
            Tags = "标签8"
        };

        // Act
        var result = await _movieAppService.UpdateAsync(_qiqikoMovieTest.MovieId, modifyMovieDto);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(modifyMovieDto.Name);
    }

    [Fact]
    public async Task 创建影片时名称唯一应成功()
    {
        // Arrange - 使用GUID确保名称唯一
        var uniqueName = "唯一影片名称_" + Guid.NewGuid();
        var createMovieDto = new CreateUpdateMovieDto
        {
            MovieTypeId = _qiqikoMovieTest.MovieTypeId,
            Name = uniqueName,
            Description = "这是一个具有唯一名称的测试影片",
            Index = 1,
            Language = "zh-Hans",
            Performers = "演员A,演员B",
            Star = "主演A",
            Tags = "标签1,标签2"
        };

        // Act
        var result = await _movieAppService.CreateAsync(createMovieDto);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(uniqueName);
    }

    [Fact]
    public async Task 删除影片对象成功()
    {
        //Act
        await _movieAppService.DeleteAsync(_qiqikoMovieTest.MovieId);

        // Act & Assert - 验证修改时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);
        }, "删除影片对象再查询应该异常");
    }

    [Fact]
    public async Task 查询影片对象()
    {
        //Act
        var result = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);
        //Assert
        result.ShouldNotBeNull();
    }
    [Fact]
    public async Task 查询影片分页对象()
    {
        //Act
        var requestDto = new MoviePageRequestDto
        {
            SkipCount = 5,
            MaxResultCount = 10,
            Sorting = " index "
        };
        var result = await _movieAppService.GetListAsync(requestDto);
        //Assert
        result.Items.ShouldNotBeNull();
        result.Items.Count.ShouldBe(10);
    }
    [Fact]
    public async Task 按条件查询影片分页对象()
    {
        //Act
        var requestDto = new MoviePageRequestDto
        {
            SkipCount = 0,
            MaxResultCount = 10,
            Sorting = " index ",
            Name = "测试影片11",
            Performers = ["赤夜萌香"]
        };
        var result = await _movieAppService.GetListAsync(requestDto);
        //Assert
        result.Items.ShouldNotBeNull();
        result.Items.Count.ShouldBe(1);
    }
    [Fact]
    public async Task 按条件查询影片分页对象2()
    {
        //Act
        var requestDto = new MoviePageRequestDto
        {
            SkipCount = 0,
            MaxResultCount = 10,
            Sorting = " index ",
            Name = "测试影片11",
            Star = ["苍井空"]
        };
        var result = await _movieAppService.GetListAsync(requestDto);
        //Assert
        result.Items.Count.ShouldBe(0);
    }

    [Fact]
    public async Task 设置影片分类应成功()
    {
        //设置路由参数
        SetRoute("PUT", $"/api/movie/{_qiqikoMovieTest.MovieId}", _qiqikoMovieTest.MovieId.ToString());
        // Arrange
        var entity = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);
        var setMovieTypeDto = new SetMovieTypeDto
        {
            MovieTypeId = _qiqikoMovieTest.MovieTypeId
        };

        // Act
        var result = await _movieAppService.SetMovieTypeAsync(_qiqikoMovieTest.MovieId, setMovieTypeDto);

        // Assert
        result.ShouldNotBeNull();
        result.MovieTypeId.ShouldBe(setMovieTypeDto.MovieTypeId);
    }

    [Fact]
    public async Task 设置错误影片分类应抛出异常()
    {
        //设置路由参数
        SetRoute("PUT", $"/api/movie/{_qiqikoMovieTest.MovieId}", _qiqikoMovieTest.MovieId.ToString());
        // Arrange
        var entity = await _movieAppService.GetAsync(_qiqikoMovieTest.MovieId);
        var setMovieTypeDto = new SetMovieTypeDto
        {
            MovieTypeId = Guid.NewGuid()
        };

        // Act & Assert - 验证修改时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieAppService.SetMovieTypeAsync(_qiqikoMovieTest.MovieId, setMovieTypeDto);
        }, "设置错误影片分类应抛出异常");
    }
    [Fact]
    public async Task 查询全部标签()
    {
        //Act
        var result = await _movieAppService.GetAllTagsAsync();
        //Assert
        result.Count.ShouldBeGreaterThan(0);
    }
    [Fact]
    public async Task 查询全部演员()
    {
        //Act
        var result = await _movieAppService.GetAllPerformersAsync();
        //Assert
        result.Count.ShouldBeGreaterThan(0);
    }

}
