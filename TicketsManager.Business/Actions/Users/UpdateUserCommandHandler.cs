using AutoMapper;
using FluentValidation;
using MediatR;
using TicketsManager.Business.Repository;
using TicketsManager.Common.Database;
using TicketsManager.Common.Dto;

namespace TicketsManager.Business.Actions.Users;

public class UpdateUserCommand : IRequest<UpdateUserCommandResponse>
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
}

public class UpdateUserCommandResponse
{
    public UserDto User;
}
public class UpdateUserCommandHandler : RepositoryAccess, IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse>
{
    private readonly IMapper mapper;
    private readonly IValidator<UpdateUserCommand> commandValidator;

    public UpdateUserCommandHandler(ITicketsManagerDbContext ticketsManagerDbContext, IMapper mapper, IValidator<UpdateUserCommand> commandValidator) : base(ticketsManagerDbContext)
    {
        this.mapper = mapper;
        this.commandValidator = commandValidator;
    }

    public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var validationResult = commandValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        var user = await userRepository.GetUserById(request.UserId);
        if (user == null)
            return null;

        user = mapper.Map(request, user);

        user.NormalizedEmail = request.Email.ToLower();
        user.NormalizedUsername = request.Username.ToLower();

        await unitOfWork.SaveChangesAsync();

        return new UpdateUserCommandResponse
        {
            User = mapper.Map<UserDto>(user)
        };
    }
}
