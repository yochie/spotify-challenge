// See https://aka.ms/new-console-template for more information
internal interface IJsonParser<RecordT>
{
    internal IEnumerable<RecordT> Parse(string data);
}