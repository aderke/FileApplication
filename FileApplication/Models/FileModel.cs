namespace FileApplication.Models
{
    public class FileModel
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        public string Path { get; set; }

        public long Size { get; set; }

        public byte[] Content { get; set; }
    }
}