// See https://aka.ms/new-console-template for more information
using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Newtonsoft.Json.Linq;

internal class FileOutputter<RecordT> : IDataOutputter 
{
    private readonly string outputFilePath;
    private readonly IJsonParser<RecordT> parser;

    public FileOutputter(string outputFilePath, IJsonParser<RecordT> parser)
    {
        this.outputFilePath = outputFilePath;
        this.parser = parser;
    }

    public void OutputData(string data)
    {
        CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
        config.InjectionOptions = InjectionOptions.Strip;
        using (var writer = new StreamWriter(this.outputFilePath, append: false))
        using (var csv = new CsvWriter(writer, config))
        {
            csv.WriteHeader<RecordT>();
            csv.NextRecord();
            foreach (RecordT record in parser.Parse(data)){
                csv.WriteRecord(record);
                csv.NextRecord();
            }
        }
    }
}

internal interface IJsonParser<RecordT>
{
    internal IEnumerable<RecordT> Parse(string data);
}