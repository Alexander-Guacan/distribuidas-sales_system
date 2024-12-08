using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Entities;

[DataContract]
public partial class Product
{
    [DataMember]
    public int ProductId { get; set; }

    [DataMember]
    public string ProductName { get; set; } = null!;

    [DataMember]
    public int CategoryId { get; set; }

    [DataMember]
    public decimal UnitPrice { get; set; }

    [DataMember]
    public int UnitsInStock { get; set; }

    [IgnoreDataMember]
    [JsonIgnore]
    public virtual Category? Category { get; set; }
}
