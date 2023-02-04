using FluentValidation;
using Net.Examles.Examples.CQRS.Interfaces;

namespace Net.Examles.Examples.CQRS.Handlers;


public record GetMeetingQuery(Guid Guid)// : IQuery<Meeting>
{

    public class Validator : AbstractValidator<GetMeetingQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Guid).NotNull().NotEmpty();
        }
    }


    public record Handler(List<Meeting> meetings) : IQueryHandler<GetMeetingQuery, Meeting>
    {
        public async Task<Meeting> Handle(GetMeetingQuery query, CancellationToken token)
        {
            return meetings.First(x => x.Id == query.Guid);
        }
    }

}