// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
namespace SpotifyPlaylistDownloader;

internal class TracksQueryResult
{
    [JsonProperty("items")]
    required internal List<PlaylistTrackTrackQueryResult> Items { get; set;}
}
