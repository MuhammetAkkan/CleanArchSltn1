using Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class UserRole : IAuditEntity
{
    public int Id { get; set; }

    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
    public virtual User User { get; set; }

    public DateTime Created { get; set; }
    public DateTime? Updated { get; set; }
}