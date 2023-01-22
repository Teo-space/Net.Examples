using ExampesOrm.Models;

namespace Net.Examles.ExampesOrm.Factories;


internal class ProductFactory
{
    const int Limit = 50000;

    static Random Random { get; } = new Random();

    public static List<Product> Products(List<Category> Categories)
    {
        List<Product> Products = new();

        for (int i = 0; i < Limit; i++)
        {
            var RandomCategory = Categories[Random.Next(0, Categories.Count - 1)];
            Products.Add(new Product()
            {
                ProductId = Guid.NewGuid(),
                Name = $"ProductName_{i}",
                Price = Random.Next(0, 10000),
                CategoryId = RandomCategory.CategoryId
            });
        }
        return Products.ToArray().ToList();
    }


}
