
namespace CrossCutting.Web.Mime
{ 
    /// <summary>
    /// Specifies the media type information for an email message attachment.
    /// </summary>
    public static class MediaType
    {
        /// <summary>
        /// Specifies the kind of application data in an email message attachment.
        /// </summary>
        public static class Application
        {
            /// <summary>
            ///  Specifies that the System.Net.Mime.MediaTypeNames.Application data is in Portable
            ///  Document Format (CSV).
            /// </summary>
            public const string Csv = "application/csv";

            /// <summary>
            ///  Specifies that the System.Net.Mime.MediaTypeNames.Application data is in Portable
            ///  Document Format (CSV).
            /// </summary>
            public const string Excel = "application/vnd.ms-excel";
        }
    }
}
