using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Entities.Multimedia
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
