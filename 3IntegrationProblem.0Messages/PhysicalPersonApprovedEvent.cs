using System.Text.Json.Serialization;

public class PhysicalPersonApprovedEvent
{
    public Guid Id { get; set; }

    [JsonConstructor]
    public PhysicalPersonApprovedEvent(Guid id)
    {
        Id = id;
    }
}
