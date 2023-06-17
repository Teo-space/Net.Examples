namespace Examples.CQRS.MediatR.Entities;


public record Meeting(Guid Id, string Name, DateTime HappensAt);