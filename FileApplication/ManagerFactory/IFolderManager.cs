using System.IO;
using FileApplication.Models;

namespace FileApplication.ManagerFactory
{
    public interface IFolderManager
    {
        void Copy(string path);

        FolderModel GetInfo(string path);

        void Delete(string path);

        void Rename(string path, string name);

        void Create(string path, string name);

        void UploadTo(Stream stream, string filePath);
    }
}
