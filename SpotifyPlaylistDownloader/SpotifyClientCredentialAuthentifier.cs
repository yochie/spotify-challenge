using System.Net;
using Newtonsoft.Json.Linq;
namespace SpotifyPlaylistDownloader;

public class SpotifyClientCredentialAuthentifier : IAuthenticationProvider
{
    public static readonly HttpClient authClient;

    private Dictionary<string, string> formData = new();

    static SpotifyClientCredentialAuthentifier(){

        SocketsHttpHandler SocketsHandler = new()
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        };
        authClient = new HttpClient(SocketsHandler);
    }

    public SpotifyClientCredentialAuthentifier(string authAPIAddress, string clientID, string secret)
    {
        authClient.BaseAddress = new Uri(authAPIAddress);
        formData["grant_type"] = "client_credentials";
        formData["client_id"] = clientID;
        formData["client_secret"] = secret;
    }

    public async Task<string> Authenticate(){

        HttpResponseMessage response = await authClient.PostAsync("token", new FormUrlEncodedContent(formData));
        if (response.StatusCode != HttpStatusCode.OK){
            throw new Exception($"Couldn't request authentication from API. Status : {response.StatusCode}");
        }
        var rawJsonResponse = await response.Content.ReadAsStringAsync();
        JObject json = JObject.Parse(rawJsonResponse);
        string? token = (string?) json["access_token"];
        return token == null ? throw new Exception("no access token returned") : token;
    }
}