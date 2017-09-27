using FileApplication.ManagerFactory;

namespace FileApplication.Managers
{
    public class DiskManagerFactory : IManagerFactory
    {
        public IFileManager GetFileManager()
        {
            return new DiskFileManager();
        }

        public IFolderManager GetFolderManager()
        {
            return new DiskFolderManager();
        }
    }
}