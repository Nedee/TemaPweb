using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service is a simple service to demonstrate how to work with files.
/// </summary>
public interface ICategoriesService
{
    public const string CategoriesDirectory = "Categories";

    public Task<ServiceResponse> AddCategory(CategoryAddDTO a, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

}
