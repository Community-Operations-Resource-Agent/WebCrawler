using Microsoft.Azure.ServiceBus;

namespace CrawlerFunctions.Providers
{
    public class ServiceBusProvider : IServiceBusProvider
    {
        private readonly IQueueClient _queueClient;

        public ServiceBusProvider(string serviceBusConnectionString, string queueName)
        {
            this._queueClient = new QueueClient(serviceBusConnectionString, queueName, ReceiveMode.PeekLock,
                RetryPolicy.Default);
        }

        public void SendMessageAsync(Message message)
        {
            this._queueClient.SendAsync(message);
        }
    }
}
