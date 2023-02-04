using System.Collections.Concurrent;

namespace Net.Examles.Examples.DependencyInjection;


public record NicePeopleGenericRepository(ConcurrentDictionary<Guid, NicePeople> peoples)
    : IGenericRepository<NicePeople, Guid>
{

    public NicePeople Get(Guid Id)
    {
        if (peoples.TryGetValue(Id, out var o))
        {
            return o;
        }
        return default!;
    }


    public Guid Add(NicePeople item)
    {
        Guid guid = Guid.NewGuid();
        while (peoples.TryGetValue(guid, out var o))
        {
            guid = Guid.NewGuid();
        }
        peoples.TryAdd(guid, item);
        return guid;
    }

    public void Update(Guid Id, NicePeople item) => peoples[Id] = item;

    public void Delete(Guid Id) => peoples.TryRemove(Id, out var o);


}