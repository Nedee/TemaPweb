using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service is a simple service to demonstrate how to work with files.
/// </summary>
public interface IItemsService
{
    public const string ItemsDirectory = "Items";

    public Task<ServiceResponse> AddItem(ItemsAddDTO a, string categ, string auc, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteItem(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
