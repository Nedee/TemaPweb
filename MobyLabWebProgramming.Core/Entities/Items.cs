using Microsoft.VisualBasic;
using MobyLabWebProgramming.Core.Enums;
using System.Security.Cryptography;

namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for a user entity, it will be mapped to a single table and each property will have it's own column except for entity object references also known as navigation properties.
/// </summary>
public class Items : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Estimated_Price { get; set; } = default!;
    public int Starting_Bid { get; set; } = default!;

    public int Current_Bid { get; set; } = default!;

    /// <summary>
    /// References to other entities such as this are used to automatically fetch correlated data, this is called a navigation property.
    /// Collection such as this can be used for Many-To-One or Many-To-Many relations.
    /// Note that this field will be null if not explicitly requested via a Include query, also note that the property is used by the ORM, in the database this collection doesn't exist. 
    /// </summary>
    /// <summary>
    /// This property is used as a foreign to the user table in the database and as a correlation key for the ORM.
    /// </summary>
    public Guid AuctionId { get; set; }

    /// <summary>
    /// This is a navigation property for the ORM to correlate this entity with the entity that it references via the foreign key.
    /// </summary>
    public Auctions Auction { get; set; }
    public Categories Category { get; set; } = default!;
    public Guid CategoryId { get; set; }
    public ICollection<Bids> Bids { get; set; }

}
