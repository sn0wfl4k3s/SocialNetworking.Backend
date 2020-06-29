using Domain.Entity;
using Domain.Enum;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossCutting.File
{
    public interface IFileService
    {
        Task<FileReference> SaveFileAsync(IFormFile file, User user);
        Task<IEnumerable<FileReference>> SaveFilesAsync(IEnumerable<IFormFile> files, User user);
        FileType IdentifyFileType(IFormFile file);
    }
}
