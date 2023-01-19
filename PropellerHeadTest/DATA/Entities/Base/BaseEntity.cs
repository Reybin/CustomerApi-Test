using System.ComponentModel.DataAnnotations;


namespace DATA.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdateDate { get; set; }

        //optimistic locking aproach
        public byte[] Version { get; set; }
    }
}
