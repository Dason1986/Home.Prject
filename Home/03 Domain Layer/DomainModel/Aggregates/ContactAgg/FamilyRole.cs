using Library.ComponentModel.Model;
using Library.Domain;
using System.ComponentModel.DataAnnotations;

namespace Home.DomainModel.Aggregates.ContactAgg
{

    public class FamilyRole : CreateEntity
    {
        public FamilyRole()
        {

        }
        public FamilyRole(ICreatedInfo createinfo) : base(createinfo)
        {

        }
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Remark { get; set; }


        public int Level { get; set; }

        public Gender Six { get; set; }

    }
}