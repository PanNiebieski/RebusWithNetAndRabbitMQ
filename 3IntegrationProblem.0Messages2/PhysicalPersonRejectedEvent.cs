using System.Text.Json.Serialization;

public class PhysicalPersonRejectedEvent
{
    public Guid Id { get; set; }

    [JsonConstructor]
    public PhysicalPersonRejectedEvent(Guid id)
    {
        Id = id;
    }
}