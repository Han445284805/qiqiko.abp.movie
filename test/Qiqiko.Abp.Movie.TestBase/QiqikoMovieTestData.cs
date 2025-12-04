using System;
using Volo.Abp.DependencyInjection;

namespace Qiqiko.Abp.Movie
{
    public class QiqikoMovieTestData : ISingletonDependency
    {
        public Guid MovieId { get; } = Guid.NewGuid();
        public Guid MovieEpisodeId { get; } = Guid.NewGuid();
        public Guid MovieTypeId { get; } = Guid.NewGuid();

    }
    public enum Language
    {
        en_US,
        zh_Hans
    }
}