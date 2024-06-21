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

    public Task Handle(string[] args)
    {
        throw new NotImplementedException();
    }
}
