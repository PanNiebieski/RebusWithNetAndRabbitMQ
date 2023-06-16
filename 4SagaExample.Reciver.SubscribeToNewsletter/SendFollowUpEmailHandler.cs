using _4SagaExample._0Messages;
using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Rebus.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4SagaExample.Reciver.SubscribeT
{
    public class SendFollowUpEmailHandler : IHandleMessages<SendFollowUpEmail>
    {
        private readonly IEmailService _emailService;
        private readonly IBus _bus;

        public SendFollowUpEmailHandler(IEmailService emailService, IBus bus)
        {
            _emailService = emailService;
            _bus = bus;
        }

        public async Task Handle(SendFollowUpEmail message)
        {
            await _emailService.SendFollowUpEmailAsync(message.Email);

            await _bus.Reply(new FollowUpEmailSent(message.Id,message.Email));
        }
    }

    public class SendWelcomeEmailHandler : IHandleMessages<SendWelcomeEmail>
    {
        private readonly IEmailService _emailService;
        private readonly IBus _bus;

        public SendWelcomeEmailHandler(IEmailService emailService, IBus bus)
        {
            _emailService = emailService;
            _bus = bus;
        }

        public async Task Handle(SendWelcomeEmail message)
        {
            await _emailService.SendWelcomeEmailAsync(message.Email);

            await _bus.Reply(new WelcomeEmailSent(message.Id, message.Email));
        }
    }

    internal sealed class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger;
        }

        public Task SendWelcomeEmailAsync(string email)
        {
            _logger.LogInformation("Sending welcome email to {Email}", email);

            return Task.CompletedTask;
        }

        public Task SendFollowUpEmailAsync(string email)
        {
            _logger.LogInformation("Sending follow-up email to {Email}", email);

            return Task.CompletedTask;
        }
    }

    public interface IEmailService
    {
        Task SendWelcomeEmailAsync(string email);

        Task SendFollowUpEmailAsync(string email);
    }
}
