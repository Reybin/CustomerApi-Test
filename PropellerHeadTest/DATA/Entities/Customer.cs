using DATA.Entities.Base;
using DATA.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DATA.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public CustomerStatus Status { get; set; }

        [NotMapped]
        public virtual string StatusDescription { get { return this.Status.ToString(); } }       

        public virtual ICollection<Note> Notes { get; set; }
    }
}
