using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace funclabs01
{
    public class QueueTriggerFunc
    {
        private readonly ILogger<QueueTriggerFunc> _logger;

        public QueueTriggerFunc(ILogger<QueueTriggerFunc> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QueueTriggerFunc))]
        public void Run([QueueTrigger("myqueue-items", Connection = "AzureWebJobsStorage")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
