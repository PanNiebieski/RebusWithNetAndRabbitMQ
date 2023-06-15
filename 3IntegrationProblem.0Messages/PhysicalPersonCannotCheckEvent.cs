using System.Text.Json.Serialization;

public class PhysicalPersonCannotCheckEvent
{
    public Guid Id { get; set; }

    [JsonConstructor]
    public PhysicalPersonCannotCheckEvent(Guid id)
    {
        Id = id;
    }
}