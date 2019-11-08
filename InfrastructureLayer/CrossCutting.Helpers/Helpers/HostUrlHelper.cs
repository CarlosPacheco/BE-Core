using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrossCutting.Helpers.Helpers
{
    public static class HostUrlHelper
    {
        #if PRD
        public static readonly string StaticContentUrlHost = @"http://klmsa1164.infineon.com/";
#elif QA
        public static readonly string StaticContentUrlHost = @"http://klmsa1164.infineon.com/";
#else // DEBUG
        public static readonly string StaticContentUrlHost = @"http://klmsa1164.infineon.com/";
#endif

        /// <summary>
        /// Generates a full URL for a static content by prepending the globally defined
        /// static content server or CDN server host address
        /// </summary>
        /// <param name="filePath">The URL path part. <example>"images/thumbs/x"</example></param>
        /// <param name="fileRepositoryPath"></param>
        /// <param name="fileExtension"></param>
        /// <returns>Full URL to static content</returns>
        public static string GetHostUrl(string filePath, string fileExtension = null, string fileRepositoryPath = "upload")
        {
            return string.IsNullOrWhiteSpace(filePath) ? null : Path.Combine(StaticContentUrlHost, fileRepositoryPath, $"{filePath}{fileExtension}");
        }

        public static string GetHostUrl(string filePath, string fileExtension = null)
        {
            string fileRepositoryPath = "upload";
            return string.IsNullOrWhiteSpace(filePath) ? null : Path.Combine(StaticContentUrlHost, fileRepositoryPath, $"{filePath}{fileExtension}");
        }

        public static string GetFileName(string filePath, string fileExtension = null, string fileRepositoryPath = "upload")
        {
            return string.IsNullOrWhiteSpace(filePath) ? null : filePath.Replace(Path.Combine(StaticContentUrlHost, fileRepositoryPath), string.Empty);
        }

        public static string GetFileName(string filePath)
        {
            return string.IsNullOrWhiteSpace(filePath) ? null : Path.GetFileName(filePath);
        }
    }
}
