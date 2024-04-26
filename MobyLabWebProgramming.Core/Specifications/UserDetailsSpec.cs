using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a simple specification to filter the user entities from the database via the constructors.
/// Note that this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class UserDetailsSpec : BaseSpec<UserDetailsSpec, UserDetails>
{
    public UserDetailsSpec(Guid id) : base(id)
    {
    }

    public UserDetailsSpec(Guid id, int n)
    {
        Query.Where(e => e.UserId == id);
    }
}