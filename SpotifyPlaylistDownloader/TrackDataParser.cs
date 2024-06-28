// See https://aka.ms/new-console-template for more information
using SpotifyPlaylistDownloader;
using Newtonsoft.Json;

internal class TrackDataParser : IJsonParser<TrackData>
{
    IEnumerable<TrackData> IJsonParser<TrackData>.Parse(string data)
    { 
        List<TrackData> trackData = new();
        var playlist = JsonConvert.DeserializeObject<PlaylistQueryResult>(data);
        if (playlist == null)
            throw new Exception("couldnt parse json");
        foreach (var track in playlist.Tracks.Items.Select(i => i.Track)) {
            string artists = track.Artists.Select(a => a.Name).Aggregate((a, b) => a + " / " + b);
            trackData.Add(new TrackData(track.Name, track.Album.Name, artists));
        }
        return trackData;
    }
}

internal record TrackData(string Name, string Album, string Artists);

