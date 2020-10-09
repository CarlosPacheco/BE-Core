using System.IO;

namespace Business.Entities
{
    public class Multimedia
    {
        /// <summary>
        /// File Contents stream
        /// </summary>
        public MemoryStream MemoryStream { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// File Path 
        /// </summary>
        public string Path { get; set; }
    }
}
