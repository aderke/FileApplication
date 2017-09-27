using System;
using System.IO;
using FileApplication.ManagerFactory;
using FileApplication.Models;

namespace FileApplication.Managers
{
    public class DiskFileManager : IFileManager
    {
        //public DiskFileManager(string path)
        //{
        //    _path = path;
        //    _fileInfo = new FileInfo(path);
        //    _dirPath = Path.GetDirectoryName(path);
        //}

        //private string _path;

        //private string _dirPath;

        //private FileInfo _fileInfo;

        public FileModel GetInfo(string path)
        {
            var fileInfo = new FileInfo(path);
            return new FileModel
            {
                Name = fileInfo.Name,
                Size = fileInfo.Length
            };
        }

        // copy file to the same folder with a new name
        public void Copy(string path)
        {
            //var dirPath = Path.GetDirectoryName(path);
            var fileInfo = new FileInfo(path);
            var fileName = Path.GetFileNameWithoutExtension(path);
            var destPath = fileInfo.DirectoryName + @"\" + fileName + " - Copy" + fileInfo.Extension;

            File.Copy(path, destPath);
        }

        public void Rename(string path, string name)
        {
            var dirPath = Path.GetDirectoryName(path);
            var newPath = dirPath + @"\" + name;
            Directory.Move(path, newPath);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }

        public FileModel Download(string path)
        {
            var fileInfo = new FileInfo(path);
            return new FileModel { Content = File.ReadAllBytes(path), Name = fileInfo.Name };
        }
    }
}