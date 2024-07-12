// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
namespace SpotifyPlaylistDownloader;


internal class PlaylistTrackTrackQueryResult
{
    [JsonProperty("track")]
    required internal TrackQueryResult Track {get; set;}
}
