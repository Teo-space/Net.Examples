using Examples.CQRS.MediatR.Entities;
using FluentValidation;
using MediatR;


namespace Examples.CQRS.MediatR.Query;


public record GetMeetingQuery(Guid Guid) : IRequest<Meeting>
{

    public class Validator : AbstractValidator<GetMeetingQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Guid).NotNull();
        }
    }


    public class Handler(List<Meeting> meetings) : IRequestHandler<GetMeetingQuery, Meeting>
    {
        public async Task<Meeting> Handle(GetMeetingQuery query, CancellationToken token)
        {
            return meetings.First(x => x.Id == query.Guid);
        }
    }

}