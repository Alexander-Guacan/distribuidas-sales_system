namespace Entities;

public partial class FailedLoginAttempt
{
    public int AttemptId { get; set; }

    public int? UserId { get; set; }

    public DateTime AttemptTime { get; set; }

    public string? IpAddress { get; set; }

    public virtual User? User { get; set; }
}
