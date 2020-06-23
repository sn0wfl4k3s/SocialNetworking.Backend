﻿using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfraData.Mapping
{
    public class FileReferenceMap : IEntityTypeConfiguration<FileReference>
    {
        public void Configure(EntityTypeBuilder<FileReference> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name).IsRequired();

            builder.Property(e => e.Path).IsRequired();
        }
    }
}