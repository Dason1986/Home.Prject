using DomainModel;
using DomainModel.ContactAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.UserAgg
{
    internal class FamilyRoleEntityTypeConfiguration : EntityTypeConfiguration<FamilyRole>
    {
        public FamilyRoleEntityTypeConfiguration()
        {

            ToTable("FamilyRole");
        }
    }
}