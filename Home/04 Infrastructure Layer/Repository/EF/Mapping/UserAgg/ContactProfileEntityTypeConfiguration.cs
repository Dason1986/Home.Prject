using Home.DomainModel.Aggregates.ContactAgg;
using System.Data.Entity.ModelConfiguration;

namespace Repository.EF.Mapping.UserAgg
{
    internal class ContactProfileEntityTypeConfiguration : EntityTypeConfiguration<ContactProfile>
    {
        public ContactProfileEntityTypeConfiguration()
        {

            ToTable("ContactProfile");
        }
    }
}