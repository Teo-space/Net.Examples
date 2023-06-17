global using static GlobalPrint;
using Net.Examles.Examples.MemoryCaches;



var cashe = new MemoryCacheWithPolicy<string>();

var value = cashe.GetOrCreate("KEY", () => "asdadadsasdadsasdad");
print(value);










