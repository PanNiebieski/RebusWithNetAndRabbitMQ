using System.Text.Json.Serialization;

namespace _4SagaExample._0Messages;

public class FollowUpEmailSent
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    [JsonConstructor]
    public FollowUpEmailSent(Guid id, string email)
    {
        Id = id;
        Email = email;
    }
}




