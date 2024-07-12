// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json.Linq;

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
        Console.WriteLine(args.Length);
        if (args.Length == 0)
        {
            Console.WriteLine("Provide a playlist ID.");
            return;
        }
        string result;
        if (args.Length == 1) 
            result = await this.client.GetPlaylist(args[0]);
        else 
            throw new ArgumentException("invalid input");
        outputter.OutputData(result);
    }
}
