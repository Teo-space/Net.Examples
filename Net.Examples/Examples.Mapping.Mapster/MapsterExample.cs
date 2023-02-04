using static Net.Examles.Examples.Mapping.AutoMapper.AutoMapperExample;
using System.Buffers;
using Mapster;

namespace Net.Examles.Examples.Mapping.Mapster;


public record MapsterExample(ILogger<MapsterExample> logger) : Handler
{
    public record User(Guid Id, string Name, string Email, bool IsActive, DateTime CreatedAt);
    public record UserDto(string Name, bool IsActive, DateTime CreatedAt);
    public record UserAddr(Guid Id, string Name, string Email, bool IsActive, DateTime CreatedAt, Address Address);
    public record Address(string AddressLine, string City, string State, string Country, string ZipCode);
    public record AddressDto(string AddressLine, string City, string State, string Country);



    public async Task Handle(CancellationToken token)
    {
        var user = new User(Guid.NewGuid(), "User 1", "test@example.com", true, DateTime.Now);
        var userDto = user.Adapt<UserDto>();
        //print(userDto);
        //info(userDto);

        //var addr = new Address("1", "2", "3", "4", "5");
        //var userAddr = new UserAddr(1, "User 1", "test@example.com", true, DateTime.Now, addr);
        //var addrDto = userAddr.Adapt<AddressDto>();
        //print(addrDto);
        //info(addrDto);


    }


}