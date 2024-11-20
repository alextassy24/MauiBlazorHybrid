namespace BlazorHybridBackend.Settings
{
    public class SmtpConfiguration
    {
        public string SmtpServer { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Username { get; set; } = string.Empty;
        public string AppPassword { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public string FromAddress { get; set; } = string.Empty;
        public string AppBaseUrl { get; set; }= string.Empty;
    }
}
