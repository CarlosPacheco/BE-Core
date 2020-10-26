namespace CrossCutting.Configurations
{
    public class UploadedOptions
    {
        public const string key = "Uploaded";
        public string Root { get; set; }
        public string Path { get; set; }

        public string HostUrl { get; set; }
    }
}
