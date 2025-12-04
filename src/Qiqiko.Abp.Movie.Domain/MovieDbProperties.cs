namespace Qiqiko.Abp.Movie;

public static class MovieDbProperties
{
    public static string DbTablePrefix { get; set; } = "Qiqiko";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "QiqikoMovie";
}
