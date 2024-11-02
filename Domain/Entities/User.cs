using Domain.Common;

namespace Domain.Entities;

public class User : IAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }

    public string Token { get; set; } = null;
}