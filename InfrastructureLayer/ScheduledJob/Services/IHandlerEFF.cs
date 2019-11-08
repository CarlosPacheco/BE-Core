namespace ScheduledJob.Services
{
    public interface IHandlerEFF
    {
        /// <summary>
        /// Read all files
        /// </summary>
        /// <returns></returns>
        void ReadFiles(Business.Entities.ScheduledJob.ScheduledJob scheduledJo);

        /// <summary>
        /// Gets an ScheduledJob by it's unique identifier
        /// </summary>
        /// <param name="id">The loh unique identifier</param>
        /// <returns>Case with the specified unique identifier</returns>
        Business.Entities.ScheduledJob.ScheduledJob GetById(int id);

        /// <summary>
        /// Check for old files to be deleted
        /// </summary>
        void DeleteFilesTracking(Business.Entities.ScheduledJob.ScheduledJob scheduledJo);
    }
}
