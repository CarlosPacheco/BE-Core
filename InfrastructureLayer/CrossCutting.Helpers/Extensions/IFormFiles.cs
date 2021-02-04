using Microsoft.AspNetCore.Http;
using System.IO;

namespace CrossCutting.Helpers.Extensions
{
    /// <summary>
    /// IFormFile type Extension methods class.
    /// </summary>
    public static class IFormFiles
    {
        public static MemoryStream ToMemoryStream(this IFormFile value)
        {
            MemoryStream memoryStream = new MemoryStream();
            value.CopyTo(memoryStream);
            return memoryStream; 
        }
    }
}
