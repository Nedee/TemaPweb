using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;
using System.Xml.Linq;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class AuctionsSpec : BaseSpec<AuctionsSpec, Auctions>
{
    public AuctionsSpec(Guid id) : base(id)
    {
    }

    public AuctionsSpec(string name)
    {
        Query.Where(e => e.name == name);
    }

    public AuctionsSpec()
    {
        Query.Where(e => e.start_date < DateTime.Now && e.end_date > DateTime.Now);
    }

}