﻿using Home.DomainModel.Aggregates.GalleryAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.GalleryAgg
{
    internal class PhotoAttributeEntityTypeConfiguration : EntityTypeConfiguration<PhotoAttribute>
    {
        public PhotoAttributeEntityTypeConfiguration()
        {
            this.HasRequired(t => t.Owner)
.WithMany()
.HasForeignKey(t => t.OwnerID)
.WillCascadeOnDelete(false);
            ToTable("PhotoAttribute");
        }
    }
}