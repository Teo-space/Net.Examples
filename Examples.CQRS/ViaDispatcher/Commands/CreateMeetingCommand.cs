using Examples.CQRS.ViaDispatcher.Interfaces;
using FluentValidation;


namespace Examples.CQRS.ViaDispatcher.Commands;


/// <summary>
/// Комманда для создания новой встречи
/// естественно валидатор и хэндлер могут быть вынесены в отдельный файл
/// </summary>
/// <param name="Name"></param>
/// <param name="HappensAt"></param>
public record CreateMeetingCommand(string Name, DateTime HappensAt)
{

    public class Validator : AbstractValidator<CreateMeetingCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Name).NotNull().MinimumLength(4).MaximumLength(50);
            RuleFor(x => x.HappensAt).NotNull();
        }
    }


    public class Handler(List<Meeting> meetings) : ICommandHandler<CreateMeetingCommand, Guid>
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