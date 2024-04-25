namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for another entity to store files and an example for a One-To-Many relation.
/// </summary>
public class Auctions : BaseEntity
{
    public DateTime start_date { get; set; } = default!;
    public DateTime end_date { get; set; } = default!;
    /// <summary>
    /// This property is used as a foreign to the user table in the database and as a correlation key for the ORM.
    /// </summary>
    public ICollection<Items> Items { get; set; } = default!;
    public ICollection<Auctions_Of_Interest> AuctionsOfInterest { get; set; }
}
