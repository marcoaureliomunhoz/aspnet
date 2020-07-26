using System;

namespace BookShop.Infra.Messaging.Exceptions
{
    public class MessageConsumerException : Exception
    {
        public MessageConsumerException()
        {
        }

        public MessageConsumerException(string topicName)
            : base($"Error consuming topic {topicName}")
        {
        }

        public MessageConsumerException(string topicName, Exception inner) 
            : base($"Error consuming topic {topicName}", inner)
        {
        }
    }
}