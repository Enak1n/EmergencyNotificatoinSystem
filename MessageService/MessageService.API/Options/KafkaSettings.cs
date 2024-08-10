namespace MessageService.API.Options
{
    public class KafkaSettings
    {
        public string Host { get; set; }
        public string SendTopic { get; set; }
        public string NotificationTopic { get; set; }
    }
}
