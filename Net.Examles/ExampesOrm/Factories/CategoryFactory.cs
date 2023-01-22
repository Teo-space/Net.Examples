using ExampesOrm.Models;

namespace Net.Examles.ExampesOrm.Factories;


internal class CategoryFactory
{
    const int Limit = 5000;

    static Random Random { get; } = new Random();



    public static List<Category> Categories()
    {
        List<Category> Categories  = new();

        for (int i = 0; i < Limit; i++)
        {
            Categories.Add(new Category()
            {
                Name = $"CategotyName_{i}"
            });
        }

        return Categories.ToArray().ToList();
    }


    public static List<Category> CategoriesWithIdentity()
    {
        List<Category> Categories = new();

        for (int i = 0; i < Limit; i++)
        {
            Categories.Add(new Category()
            {
                CategoryId = i,
                Name = $"CategotyName_{i}"
            });
        }

        return Categories.ToArray().ToList();
    }




}
