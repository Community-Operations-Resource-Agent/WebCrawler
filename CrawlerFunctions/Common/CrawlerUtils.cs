using System;
using System.Text;
using Microsoft.Azure.ServiceBus;

namespace CrawlerFunctions.Common
{
    public class CrawlerUtils
    {
        public static readonly string JsonMediaType = "application/json";

        public static Message CreateMessage(string messageBody, string label)
        {
            return new Message(Encoding.UTF8.GetBytes(messageBody))
            {
                Label = label,
                ContentType = JsonMediaType,
                CorrelationId = Guid.NewGuid().ToString()
            };
        }
    }
}
