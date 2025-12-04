using Qiqiko.Abp.Movie.Episodes;
using Qiqiko.Abp.Movie.Movies;
using Qiqiko.Abp.Movie.MovieTypes;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace Qiqiko.Abp.Movie;

#pragma warning disable RMG012 // Source member was not found for target member
#pragma warning disable RMG020 // Source member was not found for target member
[Mapper]
public partial class MovieToMovieDtoMapper : MapperBase<Movie, MovieDto>
{
    public override partial MovieDto Map(Movie source);
    public override partial void Map(Movie source, MovieDto destination);
}

[Mapper]
public partial class CreateUpdateMovieDtoToMovieMapper : MapperBase<CreateUpdateMovieDto, Movie>
{
    public override partial Movie Map(CreateUpdateMovieDto source);
    public override partial void Map(CreateUpdateMovieDto source, Movie destination);
}

[Mapper]
public partial class MovieTypeMovieTypeDtoMapper : MapperBase<MovieType, MovieTypeDto>
{
    public override partial MovieTypeDto Map(MovieType source);
    public override partial void Map(MovieType source, MovieTypeDto destination);
}
[Mapper]
public partial class CreateUpdateMovieTypeDtoMovieTypeMapper : MapperBase<CreateUpdateMovieTypeDto, MovieType>
{
    public override partial MovieType Map(CreateUpdateMovieTypeDto source);
    public override partial void Map(CreateUpdateMovieTypeDto source, MovieType destination);
}


[Mapper]
public partial class EpisodeEpisodeDtoMapper : MapperBase<Episode, EpisodeDto>
{
    public override partial EpisodeDto Map(Episode source);
    public override partial void Map(Episode source, EpisodeDto destination);
}
[Mapper]
public partial class CreateUpdateEpisodeDtoEpisodeMapper : MapperBase<CreateUpdateEpisodeDto, Episode>
{
    public override partial Episode Map(CreateUpdateEpisodeDto source);
    public override partial void Map(CreateUpdateEpisodeDto source, Episode destination);
}

#pragma warning restore RMG020 // Source member was not found for target member
#pragma warning restore RMG012 // Source member was not found for target member