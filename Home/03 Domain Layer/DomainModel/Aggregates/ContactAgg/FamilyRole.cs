using System.ComponentModel.DataAnnotations;

namespace DomainModel.ContactAgg
{

    public class FamilyRole : Entity
    {
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Remark { get; set; }


        public int Level { get; set; }

        public Gender Six { get; set; }

    }
}