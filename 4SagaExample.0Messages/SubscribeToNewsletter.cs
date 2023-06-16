using System.Text.Json.Serialization;

namespace _4SagaExample._0Messages;

//public record SubscribeToNewsletter(string Email);

//public record SendWelcomeEmail(string Email);

//public record SendFollowUpEmail(string Email);

//public record FollowUpEmailSent(string Email);

public class SubscribeToNewsletter
{
    public Guid Id { get; set; }

    public string Email { get; set; }

    [JsonConstructor]
    public SubscribeToNewsletter(Guid id, string email)
    {
        Id = id;
        Email = email;
    }
}




