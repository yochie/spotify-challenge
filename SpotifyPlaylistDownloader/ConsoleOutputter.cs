// See https://aka.ms/new-console-template for more information
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

internal class ConsoleOutputter : IDataOutputter
{
    public ConsoleOutputter()
    {
    }

    public void OutputData(string data)
    {
        foreach (var item in data){
            Console.WriteLine(item);
        }
    }
}
