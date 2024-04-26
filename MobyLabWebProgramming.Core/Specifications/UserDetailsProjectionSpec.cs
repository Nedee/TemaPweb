using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a specification to filter the user entities and map it to and UserDTO object via the constructors.
/// Note how the constructors call the base class's constructors. Also, this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class UserDetailsProjectionSpec : BaseSpec<UserDetailsProjectionSpec, UserDetails, UserDetailsDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<UserDetails, UserDetailsDTO>> Spec => e => new()
    {
        Id = e.Id,
        Adress = e.Adress,
        Date_of_Birth = e.Date_of_Birth 
    };

    public UserDetailsProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public UserDetailsProjectionSpec(Guid id) : base(id)
    {
    }

    public UserDetailsProjectionSpec(Guid search, int n)
    {
        Query.Where(e => e.UserId.Equals(search)); // This is an example on who database specific expressions can be used via C# expressions.                                                                 // Note that this will be translated to the database something like "where user.Name ilike '%str%'".
    }
}
