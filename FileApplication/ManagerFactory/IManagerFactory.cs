namespace FileApplication.ManagerFactory
{
    public interface IManagerFactory
    {
        IFileManager GetFileManager();

        IFolderManager GetFolderManager();
    }
}
