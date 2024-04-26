using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service is a simple service to demonstrate how to work with files.
/// </summary>
public interface IAuctionsService
{
    public const string AuctionsDirectory = "Auctions";

    public Task<ServiceResponse> AddAuction(AuctionsAddDTO a, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    //public Task<ServiceResponse<PagedResponse<AuctionsDTO>>> GetAuctions(CancellationToken cancellationToken = default);
}
