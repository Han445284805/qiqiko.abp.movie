using Qiqiko.Abp.Movie.Movies;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Modularity;
using Volo.Abp.Uow;
using Xunit;

namespace Qiqiko.Abp.Movie.MovieTypes;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IIdentityUserAppService here).
 * Only test your own application services.
 */
public abstract class MovieTypesAppServiceTests<TStartupModule> : MovieApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private IMovieTypeAppService _movieTypeAppService;
    private readonly IMovieTypeRepository _movieTypeRepository;
    private QiqikoMovieTestData _qiqikoMovieTest;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public MovieTypesAppServiceTests()
    {
        _movieTypeAppService = GetRequiredService<IMovieTypeAppService>();
        _qiqikoMovieTest = GetRequiredService<QiqikoMovieTestData>();
        _movieTypeRepository = GetRequiredService<IMovieTypeRepository>();
        _unitOfWorkManager = GetRequiredService<IUnitOfWorkManager>();
    }

    [Fact]
    public async Task 创建影片分类时名称重复应抛出异常()
    {
        // Arrange - 准备一个与现有影片分类同名的DTO
        var createMovieTypeDto = new CreateUpdateMovieTypeDto
        {
            Name = "测试影片分类", // 使用一个可能重复的名称
            Index = 1,
        };

        // Act & Assert - 验证创建重复名称的影片分类时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieTypeAppService.CreateAsync(createMovieTypeDto);
        }, "创建同名影片分类时应抛出异常");
    }
    [Fact]
    public async Task 修改影片分类时名称重复应抛出异常()
    {

        //设置路由参数
        SetRoute("PUT", $"/api/movie-type/{_qiqikoMovieTest.MovieTypeId}", _qiqikoMovieTest.MovieTypeId.ToString());

        var entity = await _movieTypeAppService.GetAsync(_qiqikoMovieTest.MovieTypeId);
        var modifyMovieTypeDto = new CreateUpdateMovieTypeDto
        {
            Name = "测试影片分类1",
            Index = 2,
        };
        // Act & Assert - 验证修改时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieTypeAppService.UpdateAsync(_qiqikoMovieTest.MovieTypeId, modifyMovieTypeDto);
        }, "修改为重复名称时应抛出异常");
    }
    [Fact]
    public async Task 修改影片分类时名称不重复应成功()
    {

        //设置路由参数
        SetRoute("PUT", $"/api/movie-type/{_qiqikoMovieTest.MovieTypeId}", _qiqikoMovieTest.MovieTypeId.ToString());

        // Arrange
        var entity = await _movieTypeAppService.GetAsync(_qiqikoMovieTest.MovieTypeId);
        var modifyMovieTypeDto = new CreateUpdateMovieTypeDto
        {
            Name = "唯一的新名称_" + Guid.NewGuid(), // 确保名称唯一
            Index = 2,
        };

        // Act
        var result = await _movieTypeAppService.UpdateAsync(_qiqikoMovieTest.MovieTypeId, modifyMovieTypeDto);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(modifyMovieTypeDto.Name);
    }
    [Fact]
    public async Task 创建影片分类时名称唯一应成功()
    {
        // Arrange - 使用GUID确保名称唯一
        var uniqueName = "唯一影片分类名称_" + Guid.NewGuid();
        var createMovieTypeDto = new CreateUpdateMovieTypeDto
        {
            Name = uniqueName,
            Index = 1,
        };

        // Act
        var result = await _movieTypeAppService.CreateAsync(createMovieTypeDto);

        // Assert
        result.ShouldNotBeNull();
        result.Name.ShouldBe(uniqueName);
    }
    [Fact]
    public async Task 删除影片分类对象()
    {
        var requestDto = new PagedAndSortedResultRequestDto()
        {
            MaxResultCount = 1,
            SkipCount = 1
        };
        var result = await _movieTypeAppService.GetListAsync(requestDto);

        //Act
        await _movieTypeAppService.DeleteAsync(result.Items[0].Id);

        // Act & Assert - 验证修改时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieTypeAppService.GetAsync(_qiqikoMovieTest.MovieId);
        }, "删除影片分类对象再查询应该异常");
    }
    [Fact]
    public async Task 删除错误影片对象应抛出异常()
    {
        // Act & Assert - 验证修改时会抛出异常[2,4](@ref)
        await Should.ThrowAsync<Exception>(async () =>
        {
            await _movieTypeAppService.DeleteAsync(_qiqikoMovieTest.MovieTypeId);
        }, "删除影片对象再查询应该异常");
    }
    [Fact]
    public async Task 查询影片分类对象()
    {
        //Act
        var result = await _movieTypeAppService.GetAsync(_qiqikoMovieTest.MovieTypeId);
        //Assert
        result.ShouldNotBeNull();
    }
    [Fact]
    public async Task 查询影片分类分页对象()
    {
        //Act
        var requestDto = new MoviePageRequestDto
        {
            SkipCount = 5,
            MaxResultCount = 10,
            Sorting = " index "
        };
        var result = await _movieTypeAppService.GetListAsync(requestDto);
        //Assert
        result.Items.ShouldNotBeNull();
        result.Items.Count.ShouldBe(10);
    }
}
