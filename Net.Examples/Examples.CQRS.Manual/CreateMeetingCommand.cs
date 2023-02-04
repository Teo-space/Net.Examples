using FluentValidation;
using MediatR;

namespace Net.Examles.Examples.CQRS.Manual;


public record CreateMeetingCommand(string Name, DateTime HappensAt) : IRequest<Guid>
{

    public class Validator : AbstractValidator<CreateMeetingCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.HappensAt).NotNull();
        }
    }


    public record Handler(List<Meeting> meetings) : IRequestHandler<CreateMeetingCommand, Guid>
    {
        public async Task<Guid> Handle(CreateMeetingCommand command, CancellationToken token)
        {
            Guid guid = Guid.NewGuid();

            meetings.Add(new Meeting(
                Id: guid,
                Name: command.Name,
                HappensAt: command.HappensAt
                ));
            return guid;
        }
    }


}





