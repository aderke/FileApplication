using FileApplication.Models;

namespace FileApplication.ManagerFactory
{
    public interface IFileManager
    {
        void Copy(string path);

        FileModel GetInfo(string path);

        void Delete(string path);

        void Rename(string path, string name);

        FileModel Download(string path);
    }
}