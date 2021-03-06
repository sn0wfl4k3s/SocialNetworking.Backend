﻿using Core.Domain;
using Domain.Enum;
using System;

namespace Domain.Entity
{
    public class FileReference : IEntity
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public DateTime Sended { get; set; }
        public FileType FileType { get; set; }
        public virtual User User { get; set; }
    }
}
