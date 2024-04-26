using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Net.Mime;

namespace MobyLabWebProgramming.Backend.Controllers;

/// <summary>
/// This is a controller example to show who to work with files and form data.
/// </summary>
[ApiController] // This attribute specifies for the framework to add functionality to the controller such as binding multipart/form-data.
[Route("api/[controller]/[action]")] // The Route attribute prefixes the routes/url paths with template provides as a string, the keywords between [] are used to automatically take the controller and method name.
public class AuctionsController : AuthorizedController
{
    private const long MaxFileSize = 128 * 1024 * 1024; // Set the maximum size for file requests to 128MB.

    private readonly IAuctionsService AuctionsService;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public AuctionsController(IUserService userService, IAuctionsService auctionsService) : base(userService)
    {
        AuctionsService = auctionsService;
    }

    [Authorize]
    [HttpPost] // This attribute will make the controller respond to a HTTP POST request on the route /api/UserFile/Add.
    public async Task<ActionResult<RequestResponse>> AddAuction([FromForm] AuctionsAddDTO form) // The FromForm attribute will bind each field from the form request to the properties of the UserFileAddDTO parameter.
                                                                                         // For files the property should be IFormFile or IFormFileCollection.
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await AuctionsService.AddAuction(form, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

}
