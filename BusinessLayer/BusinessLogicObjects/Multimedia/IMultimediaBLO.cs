using Business.Entities.Multimedia;

namespace Business.LogicObjects.MultimediaFiles
{
    /// <summary>
    /// Describes a Logic Controller for <see cref="UploadedFileVO"/> business entity
    /// </summary>
    public partial interface IMultimediaBLO
    {
        /// <summary>
        /// Uploaded the image
        /// </summary>
        /// <param name="file">file to be save</param>
        /// <returns>A a object <see cref="MultimediaContent"/></returns>
        Multimedia UploadedFile(Multimedia file);

        /// <summary>
        /// Remove the image
        /// </summary>
        /// <param name="file">file to be save</param>
        /// <returns>A a object <see cref="MultimediaContent"/></returns>
        void RemoveFile(Multimedia file);
    }
}