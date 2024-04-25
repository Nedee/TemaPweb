namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for another entity to store files and an example for a One-To-Many relation.
/// </summary>
public class Bids : BaseEntity
{
    public Guid UserId { get; set; } = default!;
    public Guid ItemId { get; set; } = default!;
    public int BidValue { get; set; } = default!;
    public DateTime Timestamp { get; set; } = default!;

    public User User { get; set; } = default!;
    public Items Item { get; set; } = default!;
}
