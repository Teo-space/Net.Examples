using StackExchange.Redis;


namespace Net.Examles.Examples.Db.Redis;


public record RedisExample(ILogger<RedisExample> logger) : Handler
{
    public async Task Handle(CancellationToken token)
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
        //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("server1:6379,server2:6379");

        IDatabase db = redis.GetDatabase();
        //IDatabase db = redis.GetDatabase(databaseNumber, asyncState);

        {
            string value1 = "abcdefg";
            db.StringSet("mykey", value1);

            string value2 = db.StringGet("mykey");
        }


        {
            ISubscriber sub = redis.GetSubscriber();
            sub.Subscribe("messages", (channel, message) =>
            {
                //print((string)message!);
            });


            // Synchronous handler
            sub.Subscribe("messages").OnMessage(channelMessage =>
            {
                //print((string)channelMessage.Message!);
            });

            // Asynchronous handler
            sub.Subscribe("messages").OnMessage(async channelMessage =>
            {
                await Task.Delay(1000);
                //print((string)channelMessage.Message!);
            });

            sub.Publish("messages", "hello");

        }
    }


}
