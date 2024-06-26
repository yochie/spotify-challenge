// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Text.Json.Nodes;
using System.Web;
using Newtonsoft.Json.Linq;

internal class SpotifyClient : ISpotifyClient
{
    private readonly IAuthenticationProvider authentifier;
    private readonly Uri endpoint;
    private static readonly HttpClient httpClient;

    static SpotifyClient(){

        SocketsHttpHandler SocketsHandler = new()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        };
        httpClient = new HttpClient(SocketsHandler);
    }

    public SpotifyClient(IAuthenticationProvider authentifier, string endpointUri)
    {
        this.authentifier = authentifier;
        this.endpoint = new Uri(endpointUri);
    }

    public async Task<JObject> GetPlaylist(string id, string fieldQuery = "fields=tracks.items(track(name,artists(name),album(name)))")
    {
        string accessToken = await authentifier.GetAccessToken();
        var msg = new HttpRequestMessage();
        msg.Headers.Add("Authorization", "Bearer " + accessToken);
        msg.Method = HttpMethod.Get;
        UriBuilder uriBuilder = new(endpoint);
        uriBuilder.Path += $"playlists/{id}";
        uriBuilder.Query = fieldQuery;
        msg.RequestUri = uriBuilder.Uri;
        msg.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        var response = await httpClient.SendAsync(msg);

        if (response.StatusCode != HttpStatusCode.OK){
            throw new Exception($"Couldn't request playlist data. Q: {msg.RequestUri}\n status : {response.StatusCode}");
        }
        var rawJsonResponse = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(rawJsonResponse);
        return json;
    }
}
