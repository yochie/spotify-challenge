// See https://aka.ms/new-console-template for more information
internal class CommandHandler : ICommandHandler
{
    private ISpotifyClient client;
    private IDataOutputter outputter;

    public CommandHandler(ISpotifyClient client, IDataOutputter outputter)
    {
        this.client = client;
        this.outputter = outputter;
    }

    public async Task Handle(string[] args)
    {
        await this.client.GetPlaylist("p");
        throw new NotImplementedException();
    }
}
