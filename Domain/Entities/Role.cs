using Domain.Common;

namespace Domain.Entities;

public class Role : IAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}