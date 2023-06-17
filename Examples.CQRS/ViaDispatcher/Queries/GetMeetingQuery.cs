using Examples.CQRS.ViaDispatcher;
using Examples.CQRS.ViaDispatcher.Interfaces;
using FluentValidation;

namespace Examples.CQRS.ViaDispatcher.Queries;


public record GetMeetingQuery(Guid Guid)// : IQuery<Meeting>
{

    public class Validator : AbstractValidator<GetMeetingQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Guid).NotNull().NotEmpty();
        }
    }


    public class Handler(List<Meeting> meetings) : IQueryHandler<GetMeetingQuery, Meeting>
    {
        public async Task<Meeting> Handle(GetMeetingQuery query, CancellationToken token)
        {
            return meetings.First(x => x.Id == query.Guid);
        }
    }

}

