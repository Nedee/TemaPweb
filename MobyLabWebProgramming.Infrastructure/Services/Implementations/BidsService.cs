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

public class BidsService : IBidsService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IFileRepository _fileRepository;

    public BidsService(IRepository<WebAppDatabaseContext> repository, IFileRepository fileRepository)
    {
        _repository = repository;
        _fileRepository = fileRepository;
    }

    public async Task<ServiceResponse> AddBid(BidsAddDTO a, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {

        var entity = await _repository.GetAsync(new ItemsSpec(a.Id_Item), cancellationToken);

        if (entity != null && a.bid_value <= entity.Current_Bid) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "You need to place a bid higher than the current one", ErrorCodes.WrongBidValue));
        }

        if (entity != null)
        {
            entity.Current_Bid = a.bid_value;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        await _repository.AddAsync(new Bids
        {
            UserId = requestingUser.Id,
            ItemId = a.Id_Item,
            BidValue = a.bid_value,

        }, cancellationToken); // A new entity is created and persisted in the database.

        return ServiceResponse.ForSuccess();
    }

    /// <summary>
    /// This static method creates the path for a user to where it has to store the files, each user should have an own folder.
    /// </summary>

}
