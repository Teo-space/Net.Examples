using Mapster;



var user = new UserEntity(Guid.NewGuid(), "Userilla", "Userilla@example.com", true, DateTime.Now);
var userDto = user.Adapt<UserDto>();
print(user);
print(userDto);



var addr = new AddressValueObject("Street", "City", "State", "Country", "ZipCode", "someHiddenProperty");
var addrDto = addr.Adapt<AddressDto>();
print(addr);
print(addrDto);



var userAggregate = new UserAggregate(user, addr);
print(userAggregate);


//Mapster can't do this
var userViewModelDto = userAggregate.Adapt<UserViewModelDto>();
print(userViewModelDto);





public record UserEntity(Guid Id, string Name, string Email, bool IsActive, DateTime CreatedAt);
public record UserDto(string Name, bool IsActive, DateTime CreatedAt);


public record AddressValueObject(string AddressLine, string City, string State, string Country, string ZipCode, string someHiddenProperty);
public record AddressDto(string AddressLine, string City, string State, string Country);


public record UserAggregate(UserEntity User, AddressValueObject Address);

public record UserViewModelDto(string Name, bool IsActive, string Country, string City);
