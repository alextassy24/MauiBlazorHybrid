namespace BlazorHybridBackend.Utils
{
    public class TokenUtils
    {
        public string GenerateConfirmationToken()
        {
            var random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}
