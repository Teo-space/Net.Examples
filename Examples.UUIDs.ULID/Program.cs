//Uids которые можно сортировать по дате создания
//соответственно при запись в бд будет последовательной и индексы будут работать лучше
//так же мы не теряем идемпотентноcть


var ulids = new List<Ulid>();

for (int x = 0; x < 6; x++)
{
	var u = Ulid.NewUlid();
	//Console.WriteLine(u);
	Console.WriteLine($"[{u.Time}]      {u}");

	ulids.Add(u);
	Task.Delay(555).Wait();
}

Console.WriteLine($"Reverse");
ulids.Reverse();

foreach (var u in ulids)
{
	Console.WriteLine($"[{u.Time}]      {u}");
}


Console.WriteLine($"OrderBy");
foreach (var u in ulids.OrderBy(x => x))
{
	Console.WriteLine($"[{u.Time}]      {u}");
}