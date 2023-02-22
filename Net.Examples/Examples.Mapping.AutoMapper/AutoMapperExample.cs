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


    public void Simple()
    {
        logger.Info($"Simple");

        var mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<User, UserDto>();
        })
        .CreateMapper()
        ;
        var user = new User(1, "User 1", "test@example.com", true, DateTime.Now);
        var userDto = mapper.Map<UserDto>(user);

        logger.Info($"user: {user}");
        logger.Info($"userDto: {userDto}");



    }





    public class TestProfile : Profile
    {
        public TestProfile()
        {
            CreateMap<User, UserDto>();
        }
    }


    public void DiTest()
    {
        logger.Info($"DiTest");

        IServiceCollection services = new ServiceCollection();
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<TestProfile>();
        });

        var provider = services.BuildServiceProvider();
        var mapper = provider.GetRequiredService<IMapper>();


        var user = new User(1, "User 1", "test@example.com", true, DateTime.Now);
        var userDto = mapper.Map<UserDto>(user);

        logger.Info($"user: {user}");
        logger.Info($"userDto: {userDto}");


    }



}



