using System;

namespace BookShop.Infra.Messaging.Exceptions
{
    public class MessageProducerException : Exception
    {
        public MessageProducerException()
        {
        }

        public MessageProducerException(string topicName)
            : base($"Error producing to topic {topicName}")
        {
        }

        public MessageProducerException(string topicName, Exception inner) 
            : base($"Error producing to topic {topicName}", inner)
        {
        }
    }
}