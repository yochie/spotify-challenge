// See https://aka.ms/new-console-template for more information
using System.Text.Json.Nodes;

internal class SpotifyClient : ISpotifyClient
{
    private IAuthenticationProvider authentifier;

    public SpotifyClient(IAuthenticationProvider authentifier)
    {
        this.authentifier = authentifier;
    }

    public JsonObject GetPlaylist(string id)
    {
        throw new NotImplementedException();
    }
}
