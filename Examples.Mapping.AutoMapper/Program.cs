using AutoMapper;

var mapper = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<UserEntity, UserDto>();
    cfg.CreateMap<AddressValueObject, AddressDto>();

    cfg.CreateMap<UserEntity, UserViewModelDto>();
    cfg.CreateMap<AddressValueObject, UserViewModelDto>();
    

    cfg.CreateMap<UserAggregate, UserViewModelDto>()
    .IncludeMembers(x => x.User)
    .IncludeMembers(x => x.Address)
    ;

})
.CreateMapper()
;


var user = new UserEntity(Guid.NewGuid(), "Userilla", "Userilla@example.com", true, DateTime.Now);
print(user);
var userDto = mapper.Map<UserDto>(user);
print(userDto);



var addr = new AddressValueObject("Street", "City", "State", "Country", "ZipCode", "someHiddenProperty");
print(addr);
var addrDto = mapper.Map<AddressDto>(addr);
print(addrDto);



var userAggregate = new UserAggregate(user, addr);
print(userAggregate);

//Incorrect Mapping
var userViewModelDto = mapper.Map<UserViewModelDto>(userAggregate);
print(userViewModelDto);
print(userViewModelDto.Name);
print(userViewModelDto.IsActive);
print(userViewModelDto.Country);
print(userViewModelDto.City);







public record UserEntity(Guid Id, string Name, string Email, bool IsActive, DateTime CreatedAt);
public record UserDto(string Name, bool IsActive, DateTime CreatedAt);


public record AddressValueObject(string AddressLine, string City, string State, string Country, string ZipCode, string someHiddenProperty);
public record AddressDto(string AddressLine, string City, string State, string Country);


public record UserAggregate(UserEntity User, AddressValueObject Address);


//public record UserViewModelDto(string Name, bool IsActive, string Country, string City);
public class UserViewModelDto
{
    public string Name;
    public bool IsActive;
    public string Country;
    public string City;
}