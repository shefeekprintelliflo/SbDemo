using System.Net;
using System.Web;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

public class SendMessage
{
    [Function("SendMessage")]
    public async Task<MultiResponse> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req)
    {
        string text = HttpUtility.ParseQueryString(req.Url.Query)["text"] ?? "hello";

        var response = req.CreateResponse(HttpStatusCode.Accepted);
        await response.WriteStringAsync($"Queued: {text}");

        return new MultiResponse
        {
            QueueMessage = text,
            HttpResponse = response
        };
    }
}

// Return type that carries both the HTTP response and the queue message
public class MultiResponse
{
    [ServiceBusOutput("demo-queue", Connection = "ServiceBusConnection")]
    public string? QueueMessage { get; set; }

    public HttpResponseData HttpResponse { get; set; } = null!;
}
