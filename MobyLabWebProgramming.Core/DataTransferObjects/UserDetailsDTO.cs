namespace MobyLabWebProgramming.Core.DataTransferObjects;

/// <summary>
/// This DTO is used to transfer information about a user file within the application and to client application.
/// </summary>
public class UserDetailsDTO
{
    public Guid Id { get; set; }
    public string Adress { get; set; } = default!;
    public DateTime Date_of_Birth { get; set; }
}
