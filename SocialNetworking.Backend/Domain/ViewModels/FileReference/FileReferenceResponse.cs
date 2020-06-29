using Domain.Enum;
using System;

namespace Domain.ViewModels.FileReference
{
    public class FileReferenceResponse
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime Sended { get; set; }
        public FileType FileType { get; set; }
    }
}