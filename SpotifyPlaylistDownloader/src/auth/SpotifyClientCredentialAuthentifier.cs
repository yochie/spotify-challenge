using System.Net;
using Newtonsoft.Json.Linq;
namespace SpotifyPlaylistDownloader;

public class SpotifyClientCredentialAuthentifier : IAuthenticationProvider
{
    // dont change state (base url, etc) for this singleton since that can have impacts on other clients
    // kind of dumb that there is no way to prevent access to state, but what are ya gonna do... singletons
    // potential solutin would be to wrap in proxy that provides only access to request method or use DI along with IHttpClientFactory
    private static readonly HttpClient authClient;

    private readonly Uri endPoint;
    private Dictionary<string, string> formData = new();
    private AuthenticationToken? token;

    //Creates a singleton HttpClient to be shared by all authentication clients
    //sets socket timeout to avoid DNS remapping issues if long running
    static SpotifyClientCredentialAuthentifier(){

        SocketsHttpHandler SocketsHandler = new()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        };
        authClient = new HttpClient(SocketsHandler);
    }

    public SpotifyClientCredentialAuthentifier(string endpoint, string clientID, string secret)
    {
        endPoint = new Uri(endpoint);
        formData["grant_type"] = "client_credentials";
        formData["client_id"] = clientID;
        formData["client_secret"] = secret;
        this.token = null;
    }


    //returns valid access token
    //will fetch new one if none gotten yet or expired
    public async Task<string> GetAccessToken(){

        if (token == null || token.Expired){
            token = await RequestNewAuthToken();
        }
        return token.AccessToken;
    }

    private async Task<AuthenticationToken> RequestNewAuthToken()
    {
        HttpResponseMessage response = await authClient.PostAsync(endPoint, new FormUrlEncodedContent(formData));
        if (response.StatusCode != HttpStatusCode.OK){
            throw new Exception($"Couldn't request authentication from API. Status : {response.StatusCode}");
        }
        var rawJsonResponse = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(rawJsonResponse);
        string? accessToken = (string?) json["access_token"];
        int? tokenSeconds = (int?) json["expires_in"];
        if (accessToken == null || tokenSeconds == null){
            throw new Exception("Couldn't parse authentication response");
        }
        return new AuthenticationToken(accessToken, (int)tokenSeconds);
    }
}