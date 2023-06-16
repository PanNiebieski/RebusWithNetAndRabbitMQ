using System.Text.Json.Serialization;

namespace _4SagaExample._0Messages;

public class SendFollowUpEmail
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    [JsonConstructor]
    public SendFollowUpEmail(Guid id, string email)
    {
        Id = id;
        Email = email;
    }
}




