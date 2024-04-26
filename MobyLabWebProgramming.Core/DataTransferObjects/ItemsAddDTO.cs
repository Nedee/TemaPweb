using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to transfer information about a user within the application and to client application.
/// Note that it doesn't contain a password property and that is why you should use DTO rather than entities to use only the data that you need or protect sensible information.
/// </summary>
public class ItemsAddDTO
{
    public string name { get; set; } = default!;
    public string description { get; set; } = default!;
    public int estimated_price { get; set; } = default!;
    public int starting_bid { get; set;} = default!;
    public int current_bid { get; set; } = default!;
    public string auction_name { get; set; } = default!;
    public string category_name { get; set; } = default!;
}