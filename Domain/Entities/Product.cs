using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity<int>, IAuditEntity
{

    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [JsonIgnore]
    public Category Category { get; set; } = default!;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}
