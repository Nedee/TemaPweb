using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ItemsService : IItemsService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IFileRepository _fileRepository;

    public ItemsService(IRepository<WebAppDatabaseContext> repository, IFileRepository fileRepository)
    {
        _repository = repository;
        _fileRepository = fileRepository;
    }

    public async Task<ServiceResponse> AddItem(ItemsAddDTO a, string categ, string auc, UserDTO? requestingUser = null,  CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add items", ErrorCodes.CannotAdd));
        }

        var entity = await _repository.GetAsync(new AuctionsSpec(auc), cancellationToken);
        var entity2 = await _repository.GetAsync(new CategoriesSpec(categ), cancellationToken);

        await _repository.AddAsync(new Items
        {
            Name = a.name,
            Description = a.description,
            Estimated_Price = a.estimated_price,
            Starting_Bid = a.starting_bid,
            Current_Bid = a.current_bid,
            CategoryId = entity2.Id,
            AuctionId = entity.Id

        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    /// <summary>
    /// This static method creates the path for a user to where it has to store the files, each user should have an own folder.
    /// </summary>

    public async Task<ServiceResponse> DeleteItem(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the item!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Items>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }

}
