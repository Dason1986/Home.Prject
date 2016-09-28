using Repository.EF.Mapping.FileAgg;
using Repository.EF.Mapping.GalleryAgg;
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
          
        }

        private static void SystemAgg(DbModelBuilder modelBuilder)
        {
           
        }
        private static void FileAgg(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new FileInfoEntityTypeConfiguration());

        }

        private static void GalleryAgg(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AlbumEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PhotoEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new PhtotAttributeEntityTypeConfiguration());


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
