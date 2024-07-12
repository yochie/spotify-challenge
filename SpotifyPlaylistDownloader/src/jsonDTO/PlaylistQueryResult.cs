// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
namespace SpotifyPlaylistDownloader;

internal class PlaylistQueryResult
{
    [JsonProperty("tracks")]
    required internal TracksQueryResult Tracks { get; set; }
}
