using AutoMapper;


namespace Net.Examles.Examples.Mapping.AutoMapper;



public record AutoMapperExample(ILogger<AutoMapperExample> logger) : Handler
{
    public async Task Handle(CancellationToken token)
    {
        Simple();
        DiTest();
    }



    public record User(int Id, string Name, string Email, bool IsActive, DateTime CreatedAt);
    public record UserDto(string Name, bool IsActive, DateTime CreatedAt);
    //public record UserAddr(int Id, string Name, string Email, bool IsActive, DateTime CreatedAt, Address Address);
    //public record Address(string AddressLine, string City, string State, string Country, string ZipCode);
    //public record AddressDto(string AddressLine, string City, string State, string Country);



    public void Simple()
    {
        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
            //cfg.CreateMap<Address, AddressDto>();
            //cfg.CreateMap<UserAddr, UserDto>();
            //cfg.CreateMap<UserAddr, AddressDto>();
        })
        .CreateMapper()
        ;
        var user = new User(1, "User 1", "test@example.com", true, DateTime.Now);
        var userDto = mapper.Map<UserDto>(user);
        //print(userDto);

        //var addr = new Address("1", "2", "3", "4", "5");
        //var userAddr = new UserAddr(1, "User 1", "test@example.com", true, DateTime.Now, addr);
        //var addrDto = mapper.Map<AddressDto>(userAddr);
        //print(addrDto);

    }

    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<User, UserDto>();
            //CreateMap<Address, AddressDto>();
            //CreateMap<UserAddr, UserDto>();
            //CreateMap<UserAddr, AddressDto>();
        }
    }
    public void DiTest()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddAutoMapper(cfg =>
        {
            //cfg.CreateMap<>();
            //cfg.AddMaps
            cfg.AddProfile<TestProfile>();
        });

        var provider = services.BuildServiceProvider();
        var mapper = provider.GetRequiredService<IMapper>();


        var user = new User(1, "User 1", "test@example.com", true, DateTime.Now);
        var userDto = mapper.Map<UserDto>(user);
        //print(userDto);

        //var addr = new Address("1", "2", "3", "4", "5");
        //var userAddr = new UserAddr(1, "User 1", "test@example.com", true, DateTime.Now, addr);
        //var addrDto = mapper.Map<AddressDto>(userAddr);
        //print(addrDto);
    }


}



