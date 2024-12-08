using System;
using System.Collections.Generic;

namespace Entities;

public partial class AuditLog
{
    public int LogId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    public DateTime ActionDate { get; set; }

    public string? IpAddress { get; set; }

    public string? UserAgent { get; set; }

    public virtual User? User { get; set; }
}
