using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service is a simple service to demonstrate how to work with files.
/// </summary>
public interface IBidsService
{
    public const string BidsDirectory = "Bids";

    public Task<ServiceResponse> AddBid(BidsAddDTO a, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

}
