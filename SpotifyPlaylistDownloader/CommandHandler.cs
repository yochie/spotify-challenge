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
        if (args.Length == 0)
        {
            Console.WriteLine("Provide a playlist ID.");
            return;
        }   
        var result = await this.client.GetPlaylist(args[0]);
        Console.WriteLine(result);
    }
}
