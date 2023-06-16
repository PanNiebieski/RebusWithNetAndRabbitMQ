using System.Text.Json.Serialization;

namespace _4SagaExample._0Messages;

public class SendWelcomeEmail
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    [JsonConstructor]
    public SendWelcomeEmail(Guid id, string email)
    {
        Id = id;
        Email = email;
    }
}




