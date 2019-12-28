namespace LoggingLib
{
    public class Context
    {
        public string UserId { get; set; }

        public string TraceId { get; set; }

        public Application Application { get; set; }
    }
}