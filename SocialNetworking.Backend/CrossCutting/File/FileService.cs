﻿using Core.Domain;
using Domain.Entity;
using Domain.Enum;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CrossCutting.File
{
    public class FileService : IFileService
    {
        private string Folder => @"\Files\";

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IEntityRepository<FileReference> _repository;

        public FileService(IHostingEnvironment hostingEnvironment, IEntityRepository<FileReference> repository)
        {
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
        }

        public async Task<FileReference> SaveFileAsync(IFormFile file, User user)
        {
            var fileType = IdentifyFileType(file);

            var directory = string.Concat(_hostingEnvironment.WebRootPath, Folder);

            var extension = Path.GetExtension(file.FileName);

            var fileName = string.Concat(Guid.NewGuid().ToString(), extension);

            var filepath = Path.Combine(directory, fileName);

            await using var fileStream = new FileStream(filepath, FileMode.Create);

            var fileReference = new FileReference
            {
                User = user,
                Name = fileName,
                Size = file.Length,
                Sended = DateTime.Now,
                Path = Path.Combine(Folder, fileName),
                FileType = fileType
            };

            var resultado = _repository.CriarEntidadeAsync(fileReference);

            await file.CopyToAsync(fileStream);

            return await Task.FromResult(await resultado);
        }


        public async Task<IEnumerable<FileReference>> SaveFilesAsync(IEnumerable<IFormFile> files, User user)
        {
            var fileReferences = new List<FileReference>();

            if (files is not null)
            {
                foreach (var file in files)
                    fileReferences.Add(await SaveFileAsync(file, user));
            }


            return await Task.FromResult(fileReferences);
        }

        public FileType IdentifyFileType(IFormFile file)
        {
            var fileType = file switch
            {
                var f when ".gif".Equals(Path.GetExtension(f.FileName), StringComparison.InvariantCultureIgnoreCase) => FileType.GIF,
                var f when f.ContentType.Contains("image", StringComparison.InvariantCultureIgnoreCase) => FileType.IMAGE,
                var f when f.ContentType.Contains("video", StringComparison.InvariantCultureIgnoreCase) => FileType.VIDEO,
                var f when f.ContentType.Contains("text", StringComparison.InvariantCultureIgnoreCase) => FileType.TEXT,
                var f when f.ContentType.Contains("audio", StringComparison.InvariantCultureIgnoreCase) => FileType.AUDIO,
                _ => FileType.UNKNOWN
            };

            return fileType;
        }
    }
}
