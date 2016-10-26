using Repository.EF.Mapping.AssetsAgg;
using Repository.EF.Mapping.FileAgg;
using Repository.EF.Mapping.GalleryAgg;
using Repository.EF.Mapping.ProductAgg;
using Repository.EF.Mapping.SystemAgg;
using Repository.EF.Mapping.UserAgg;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF.Mapping
{
    internal class TypeConfiguration
    {
        public static void ModelCreating(DbModelBuilder modelBuilder)
        {
            SystemAgg(modelBuilder);
            UserAgg(modelBuilder);
            FileAgg(modelBuilder);
            GalleryAgg(modelBuilder);
            AssetsAgg(modelBuilder);
            ProductAgg(modelBuilder);
        }

        private static void SystemAgg(DbModelBuilder modelBuilder)
        {
               modelBuilder.Configurations.Add(new SystemParameterEntityTypeConfiguration());
        }
        private static void FileAgg(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FileInfoEntityTypeConfiguration());

        }
        private static void ProductAgg(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductItemEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ProductAttachmentEntityTypeConfiguration()); 


        }
       private static void AssetsAgg(DbModelBuilder modelBuilder)
        {
         
            modelBuilder.Configurations.Add(new AssetsItemEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PurchaseLineItemEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PurchaseOrderEntityTypeConfiguration()); 


        }
        private static void GalleryAgg(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlbumEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PhotoEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PhotoAttributeEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PhotoFingerprintEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PhotoSimilarEntityTypeConfiguration());


        }
        private static void UserAgg(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserProfileEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ContactProfileEntityTypeConfiguration());
         //   modelBuilder.Configurations.Add(new ContactRelationEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new FamilyRoleEntityTypeConfiguration());
           // modelBuilder.Configurations.Add(new ContactRelationRightEntityTypeConfiguration());

        }
    }
}
