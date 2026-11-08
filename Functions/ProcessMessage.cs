using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

public class ProcessMessage
{
    [Function("ProcessMessage")]
    public void Run(
        [ServiceBusTrigger("demo-queue", Connection = "ServiceBusConnection")] string message,
        FunctionContext context)
    {
        var logger = context.GetLogger<ProcessMessage>();
        logger.LogInformation("Received message: {Message}", message);
    }
}
