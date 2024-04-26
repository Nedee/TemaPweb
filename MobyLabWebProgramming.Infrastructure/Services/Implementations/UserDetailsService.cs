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

public class UserDetailsService : IUserDetailsService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IFileRepository _fileRepository;

    /// <summary>
    /// This static method creates the path for a user to where it has to store the files, each user should have an own folder.
    /// </summary>
    private static string GetFileDirectory(Guid userId) => Path.Join(userId.ToString(), IUserDetailsService.UserDetailsDirectory);

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public UserDetailsService(IRepository<WebAppDatabaseContext> repository, IFileRepository fileRepository)
    {
        _repository = repository;
        _fileRepository = fileRepository;
    }

    public async Task<ServiceResponse> AddUserDetails(UserDetailsAddDTO user, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {

        /*var result = await _repository.GetAsync(new UserSpec(user.Email), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The user already exists!", ErrorCodes.UserAlreadyExists));
        }*/

        await _repository.AddAsync(new UserDetails
        {
            Adress = user.Adress,
            Date_of_Birth = user.Date_of_Birth,
            UserId = requestingUser.Id
        }, cancellationToken); // A new entity is created and persisted in the database.

        //await _mailService.SendMail(user.Email, "Welcome!", MailTemplates.UserAddTemplate(user.Name), true, "My App", cancellationToken); // You can send a notification on the user email. Change the email if you want.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<PagedResponse<UserFileDTO>>> GetUserFiles(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new UserFileProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<UserFileDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> SaveFile(UserFileAddDTO file, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var fileName = _fileRepository.SaveFile(file.File, GetFileDirectory(requestingUser.Id)); // First save the file on the filesystem.

        if (fileName.Result == null) // If not successful respond with the error.
        {
            return fileName.ToResponse();
        }

        await _repository.AddAsync(new UserFile
        {
            Name = file.File.FileName,
            Description = file.Description,
            Path = fileName.Result,
            UserId = requestingUser.Id
        }, cancellationToken); // When the file is saved on the filesystem save the returned file path in the database to identify the file.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<FileDTO>> GetFileDownload(Guid id, CancellationToken cancellationToken = default) // If not successful respond with the error.
    {
        var userFile = await _repository.GetAsync<UserFile>(id, cancellationToken); // First get the file entity from the database to find the location on the filesystem.

        return userFile != null ?
            _fileRepository.GetFile(Path.Join(GetFileDirectory(userFile.UserId), userFile.Path), userFile.Name) :
            ServiceResponse<FileDTO>.FromError(new(HttpStatusCode.NotFound, "File entry not found!", ErrorCodes.EntityNotFound));
    }

    public async Task<ServiceResponse<UserDetailsDTO>> GetUserDetails(UserDTO? user, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new UserDetailsProjectionSpec(user.Id, 0), cancellationToken); // Get a user using a specification on the repository.

        return result != null ?
            ServiceResponse<UserDetailsDTO>.ForSuccess(result) :
            ServiceResponse<UserDetailsDTO>.FromError(CommonErrors.UserNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse> UpdateUserDetails(UserDTO? user, UserDetailsAddDTO input, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new UserDetailsSpec(user.Id, 0), cancellationToken);

        if (entity != null) 
        {
            Console.WriteLine("Merge");
            entity.Adress = input.Adress ?? entity.Adress;
            entity.Date_of_Birth = input.Date_of_Birth != null ? input.Date_of_Birth : entity.Date_of_Birth;

            await _repository.UpdateAsync(entity, cancellationToken); 
        }
        Console.WriteLine("Merge2");
        return ServiceResponse.ForSuccess();
    }
}
