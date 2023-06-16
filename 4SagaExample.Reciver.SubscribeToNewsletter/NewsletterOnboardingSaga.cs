using _4SagaExample._0Messages;
using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace _4SagaExample.Reciver.SubscribeT
{
    public class NewsletterOnboardingSaga :
        Saga<NewsletterOnboardingSagaData>,
        IAmInitiatedBy<SubscribeToNewsletter>,
        IHandleMessages<WelcomeEmailSent>,
        IHandleMessages<FollowUpEmailSent>
    {
        private readonly IBus _bus;

        public NewsletterOnboardingSaga(IBus bus)
        {
            _bus = bus;
        }

        protected override void CorrelateMessages(ICorrelationConfig<NewsletterOnboardingSagaData> config)
        {
            config.Correlate<SubscribeToNewsletter>(m => m.Id, d => d.Id);

            config.Correlate<WelcomeEmailSent>(m => m.Id, d => d.Id);

            config.Correlate<FollowUpEmailSent>(m => m.Id, d => d.Id);
        }

        public async Task Handle(SubscribeToNewsletter message)
        {
            if (!IsNew)
            {
                return;
            }

            await _bus.Send(new SendWelcomeEmail(message.Id,message.Email));
        }

        public async Task Handle(WelcomeEmailSent message)
        {
            Data.WelcomeEmailSent = true;

            await _bus.Defer(TimeSpan.FromSeconds(5), new SendFollowUpEmail(message.Id, message.Email));
        }

        public Task Handle(FollowUpEmailSent message)
        {
            Data.FollowUpEmailSent = true;

            MarkAsComplete();

            return Task.CompletedTask;
        }
    }

}
