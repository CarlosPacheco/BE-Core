using Business.Core;
using Business.Entities;
using CrossCutting.Configurations;
using CrossCutting.Security.Identity;
using Data.AccessObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.IO;

namespace Business.LogicObjects.MultimediaFiles
{
    public class MultimediaBlo : BaseBlo<IMultimediaDao>, IMultimediaBlo
    {
        public IConfiguration Configuration { get; private set; }

        private readonly IOptions<UploadedOptions> Config;

        /// <summary>
        /// Initializes a new instance of <see cref="MultimediaBlo"/> (Business Controller)
        /// </summary>
        /// <param name="authorization">Security information access object to be used by this instance</param>
        /// <param name="dataAccess">Application Request's data access object to be used by this instance</param>
        public MultimediaBlo(IMultimediaDao dataAccess, IAuthorization authorization, IConfiguration configuration, IOptions<UploadedOptions> config) : base(dataAccess, authorization)
        {
            Configuration = configuration;
            Config = config;
        }

        /// <summary>
        /// Uploaded the image
        /// </summary>
        /// <param name="file">file to be save</param>
        /// <returns>A a object <see cref="MultimediaContent"/></returns>
        public Multimedia UploadedFile(Multimedia file)
        {
            // TODO: Authorization ...
            if(file == null ||  file.MemoryStream == null) return null;

            string uniqueId = Guid.NewGuid().ToString("N");
            string extension = Path.GetExtension(file.Name);
            string newFilename = uniqueId + extension;
            //override the filename
            file.Name = newFilename;

            string root = Config.Value.Root;
            string imagesPath = Config.Value.Path;

            string fullPath = Path.Combine(root, imagesPath, newFilename);

            if (File.Exists(fullPath))
            {
                return null;
            }

            using (MemoryStream ms = file.MemoryStream)
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                ms.WriteTo(stream);
            }

            return null;
        }

        /// <summary>
        /// Remove the image
        /// </summary>
        /// <param name="file">file to be save</param>
        /// <returns>A a object <see cref="MultimediaContent"/></returns>
        public void RemoveFile(Multimedia file)
        {
            // TODO: Authorization ...

            string root = Config.Value.Root;
            string imagesPath = Config.Value.Path;

            string fileName = Path.Combine(root, imagesPath, file.Name);

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }
    }
}