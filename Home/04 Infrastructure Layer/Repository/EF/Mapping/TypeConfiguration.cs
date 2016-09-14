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
            UserAgg(modelBuilder);
           
            SystemAgg(modelBuilder);
          
        }

        private static void SystemAgg(DbModelBuilder modelBuilder)
        {
           
        }

        private static void UserAgg(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserProfileEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ContactProfileEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ContactRelationEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ContactRoleEntityTypeConfiguration());
            modelBuilder.Configurations.Add(new ContactRelationRightEntityTypeConfiguration());

        }
    }
}
