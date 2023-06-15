using System.Text.Json.Serialization;

namespace _1TooBig._0Messages;

public class DocumentSavedEvent
{
    private Guid id;
    private string filename;

    [JsonConstructor]
    public DocumentSavedEvent(Guid id, string filename)
    {
        this.Id = id;
        this.FileName = filename;
    }

    public Guid Id { get => id; set => id = value; }
    public string FileName { get => filename; set => filename = value; }
}




