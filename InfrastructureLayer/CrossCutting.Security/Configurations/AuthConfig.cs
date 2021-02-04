namespace CrossCutting.Security.Configurations
{
    public class AuthConfig
    {
        public const string Position = "Auth";
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
