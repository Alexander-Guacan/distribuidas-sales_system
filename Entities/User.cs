using System;
using System.Collections.Generic;

namespace Entities;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public DateTime? LockoutTime { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    public virtual ICollection<FailedLoginAttempt> FailedLoginAttemptsNavigation { get; set; } = new List<FailedLoginAttempt>();

    public virtual Role? Role { get; set; }
}
