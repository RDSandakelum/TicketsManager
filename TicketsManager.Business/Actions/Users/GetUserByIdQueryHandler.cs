using AutoMapper;
using MediatR;
using TicketsManager.Business.Repository;
using TicketsManager.Common.Database;
using TicketsManager.Common.Dto;

namespace TicketsManager.Business.Actions.Users
{
    public class GetUserByIdQuery : IRequest<GetUserByIdQueryResponse>
    {
        public Guid UserId { get; set; }
    }
    public class GetUserByIdQueryResponse
    {
        public UserDto UserDto { get; set; }
    }
    public class GetUserByIdQueryHandler : RepositoryAccess, IRequestHandler<GetUserByIdQuery, GetUserByIdQueryResponse>
    {
        private readonly IMapper mapper;

        public GetUserByIdQueryHandler(ITicketsManagerDbContext ticketsManagerDbContext, IMapper mapper) : base(ticketsManagerDbContext)
        {
            this.mapper = mapper;
        }

        public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var userEntity = await userRepository.GetUserById(request.UserId);
            if (userEntity == null)
            {
                return null; 
            }

            return new GetUserByIdQueryResponse
            {
                UserDto = mapper.Map<UserDto>(userEntity)
            };
        }
    }
}
