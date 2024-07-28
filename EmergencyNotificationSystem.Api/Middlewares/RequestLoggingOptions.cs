namespace EmergencyNotificationSystem.Api.Middlewares
{
    public class RequestLoggingOptions
    {
        public Func<HttpRequest, object> RequestProjection { get; set; } = req => new RequestInfo(req);
    }
}
