using FluentValidation;
using Net.Examles.Examples.CQRS.Interfaces;



namespace Net.Examles.Examples.CQRS;


public record CreateMeetingCommand(string Name, DateTime HappensAt) : ICommand<Guid>
{

    public class Validator : AbstractValidator<CreateMeetingCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.HappensAt).NotNull();
        }
    }


    public record Handler(List<Meeting> meetings) : ICommandHandler<CreateMeetingCommand, Guid>
    {
        public async Task<Guid> Handle(CreateMeetingCommand command, CancellationToken cancellation)
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