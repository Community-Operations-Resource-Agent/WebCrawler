using Microsoft.Azure.ServiceBus;

namespace CrawlerFunctions.Providers
{
    /// <summary>
    /// Interface to send messages on service bus queue
    /// </summary>
    public interface IServiceBusProvider
    {
        void SendMessageAsync(Message message);
    }
}
