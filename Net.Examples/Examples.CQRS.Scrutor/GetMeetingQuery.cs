using FluentValidation;
using MediatR;


namespace Net.Examles.Examples.CQRS.Scrutor;



public record GetMeetingQuery(Guid Guid) : IRequest<Meeting>
{

    public class Validator : AbstractValidator<GetMeetingQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Guid).NotNull();
        }
    }


    public record Handler(List<Meeting> meetings) : IRequestHandler<GetMeetingQuery, Meeting>
    {
        public async Task<Meeting> Handle(GetMeetingQuery query, CancellationToken token)
        {
            return meetings.First(x => x.Id == query.Guid);
        }
    }

}