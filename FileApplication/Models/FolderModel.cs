using System.Collections.Generic;

namespace FileApplication.Models
{
    public class FolderModel
    {
        public List<FolderModel> Folders { get; set; }

        public List<FileModel> Files { get; set; }
        
        public string Name { get; set; }

        public string Path { get; set; }

        public long Size { get; set; }
    }
}