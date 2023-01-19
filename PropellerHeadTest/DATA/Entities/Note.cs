using DATA.Entities.Base;
using System.Text.Json.Serialization;

namespace DATA.Entities
{
    public class Note : BaseEntity
    {
        public string Content { get; set; }
        public int CustomerId { get; set; }

        [JsonIgnore]
        public virtual Customer Customer { get; set; }
    }
}
