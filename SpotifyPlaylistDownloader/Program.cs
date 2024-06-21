// See https://aka.ms/new-console-template for more information
using SpotifyPlaylistDownloader;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Nodes;

internal class Program
{

    private static async Task Main(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json")
            .Build();
        Settings settings = config.GetRequiredSection("Settings").Get<Settings>() ?? throw new Exception("Bad config");
        IAuthenticationProvider authentifier = new SpotifyClientCredentialAuthentifier(settings.AuthAPIAddress,
                                                                                       settings.ClientID,
                                                                                       settings.Secret);
        ISpotifyClient client = new SpotifyClient(authentifier);
        IDataOutputter outputter = new StringDataOutputter();
        await new CommandHandler(client, outputter).Handle(args);
    }
}

internal interface ICommandHandler
{
    public Task Handle(string[] args);
}

internal interface IDataOutputter
{
    public void OutputData(JsonObject data);
}

internal interface ISpotifyClient
{
    public JsonObject GetPlaylist(string id);
}

internal sealed class Settings {
    public required string AuthAPIAddress { get; set; }
    public required string ClientID { get; set;}
    public required string Secret { get; set;}
    public required string DataAPIAddress { get; set;}
}

internal interface IAuthenticationProvider
{
    public Task<string> Authenticate();
}