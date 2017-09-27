using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileApplication.ManagerFactory;
using FileApplication.Models;

namespace FileApplication.Managers
{
    public class DiskFolderManager : IFolderManager
    {
        //public DiskFolderManager(string path)
        //{
        //    _path = path;
        //    _dirInfo = new DirectoryInfo(path);
        //}

        //private string _path;

        //private DirectoryInfo _dirInfo;

        public FolderModel GetInfo(string path)
        {
            var attr = File.GetAttributes(path);

            if (!attr.HasFlag(FileAttributes.Directory))
                return null;

            var dirInfo = new DirectoryInfo(path);
            long dirSize = 0;
            var result = new FolderModel
            {
                Folders = new List<FolderModel>(),
                Files = new List<FileModel>(),
                Name = dirInfo.Name,
                Path = path
            };

            foreach (var fileInfo in dirInfo.GetFiles())
            {
                var file = new FileModel { Name = fileInfo.Name, Size = fileInfo.Length, Path = fileInfo.FullName };
                result.Files.Add(file);
                dirSize += fileInfo.Length;
            }

            foreach (var subDirInfo in dirInfo.GetDirectories())
            {
                var subDirSize = subDirInfo
                    .EnumerateFiles("*.*", SearchOption.AllDirectories)
                    .Sum(file => file.Length);
                var folder = new FolderModel
                {
                    Name = subDirInfo.Name,
                    Size = subDirSize,
                    Path = subDirInfo.FullName
                };

                result.Folders.Add(folder);
                dirSize += subDirSize;
            }

            result.Size = dirSize;
            return result;
        }

        // deep copy to the same folder with the new name
        public void Copy(string path)
        {
            var destPath = path + " - Copy";
            
            DirectoryCopy(path, destPath);
        }

        public void Rename(string path, string name)
        {
            var dirInfo = new DirectoryInfo(path);
            var newPath = dirInfo.Parent.FullName + @"\" + name;
            Directory.Move(dirInfo.FullName, newPath);
        }

        // new subdurectory inside the existing one
        public void Create(string path, string name)
        {
            var dirInfo = new DirectoryInfo(path);
            dirInfo.CreateSubdirectory(name);
        }

        public void Delete(string path)
        {
            var di = new DirectoryInfo(path);

            foreach (var file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (var dir in di.GetDirectories())
            {
                dir.Delete(true);
            }

            Directory.Delete(path);
        }

        public void UploadTo(Stream stream, string filePath)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyTo(fileStream);
            }
        }

        #region Helpers
        private static void DirectoryCopy(string sourceDirName, string destDirName)
        {
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();
            
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }
            
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var tempPath = Path.Combine(destDirName, file.Name);
                file.CopyTo(tempPath, false);
            }
            
            foreach (var subdir in dirs)
            {
                var tempPath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, tempPath);
            }
        }

        #endregion
    }
}