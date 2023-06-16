using System.Text.Json.Serialization;

namespace _4SagaExample._0Messages;

public class WelcomeEmailSent
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    [JsonConstructor]
    public WelcomeEmailSent(Guid id, string email)
    {
        Id = id;
        Email = email;
    }
}
