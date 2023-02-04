using ExampesOrm.Models;

namespace Net.Examles.ExampesOrm.Factories;


internal class CategoryFactory
{
    const int Limit = 5000;

    static Random Random { get; } = new Random();


    public static List<Category> Categories()
    {
        List<Category> Categories = new();

        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"Zero"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"One"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"Two"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"Three"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"Four"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"Five"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"Six"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"Seven"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"eight"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"nine"
        });
        Categories.Add(new Category()
        {
            CategoryId = Guid.NewGuid(),
            Name = $"ten"
        });

        return Categories.ToArray().ToList();
    }




}
