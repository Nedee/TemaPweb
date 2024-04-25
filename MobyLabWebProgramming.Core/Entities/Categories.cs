namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for another entity to store files and an example for a One-To-Many relation.
/// </summary>
public class Categories : BaseEntity
{
    public string name { get; set; } = default!;
    public Items item { get; set; } = default!;
}
