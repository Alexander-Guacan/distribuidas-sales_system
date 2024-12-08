using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Entities;

[DataContract]
public partial class Category
{
    [DataMember]
    public int CategoryId { get; set; }

    [DataMember]
    public string CategoryName { get; set; } = null!;

    [DataMember]
    public string? Description { get; set; }

    [JsonIgnore]
    [IgnoreDataMember]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
