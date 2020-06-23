using Domain.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrossCutting.FileTransference
{
    public class FileTranference
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private string Folder => "\\Files\\";

        public FileTranference(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public void SaveFile(IFormFile file)
        {
            var directory = string.Concat(_hostingEnvironment.WebRootPath, Folder);

            var filepath = Path.Combine(directory, file.FileName);

            using var fileStream = new FileStream(filepath, FileMode.Create);

            file.CopyTo(fileStream);

        }
        public void SaveFiles(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
                SaveFile(file);
        }

        public async Task SaveFileAsync(IFormFile file)
        {
            var directory = string.Concat(_hostingEnvironment.WebRootPath, Folder);

            var filepath = Path.Combine(directory, file.FileName);

            await using var fileStream = new FileStream(filepath, FileMode.Create);

            await file.CopyToAsync(fileStream);
        }

        public async Task SaveFilesAsync(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
                await SaveFileAsync(file);
        }
    }
}
