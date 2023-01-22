using ExampesOrm.Models;
using Net.Examles.ExampesOrm.Factories;
using Net.Examles.Tools.BenchMark;
using Net.OrmTests.Orms.EntityFrameworkCore.Contexts;


namespace Net.Examles.ExampesOrm.TestScenarios;


public class ScenarioEFCore : BackgroundService
{
    private readonly ILogger<ScenarioEFCore> logger;

    public ScenarioEFCore(ILogger<ScenarioEFCore> logger)
    {
        this.logger = logger;
    }


    void print(string message) => logger.Info(message);

    static Random Random { get; } = new Random();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var Categories = CategoryFactory.Categories();

        print("EFCore");

        using (var context = new EFCoreTestContext())
        {
            print("Context");

            if (context.Products.Count() == 0)
            {
                {
                    print($"Categories  {Categories.Count}");

                    context.BulkCopy(Categories);

                    print("Categories.BulkCopy");

                    var CategoriesAfterInsert = context.Categories.ToList();
                    var Products = ProductFactory.Products(CategoriesAfterInsert);


                    print($"ProductFactory.Products  {Products.Count}");

                    context.BulkCopy(Products);

                    print("Products.BulkCopy");
                }

                {
                    var CategoriesAfterInsert = context.Categories.ToList();
                    print("Inserting");

                    for (int i = 0; i < 500; i++)
                    {
                        var RandomCategory = CategoriesAfterInsert[Random.Next(0, Categories.Count - 1)];

                        var product = new Product()
                        {
                            Name = $"ProductsName_Inserting_{i}",
                            CategoryId = RandomCategory.CategoryId
                        };
                        context.Add(product); context.SaveChanges();//3400
                                                                    //context.Add(product); context.BulkSaveChanges();//3400
                                                                    //context.CopyTo(product);//1200
                                                                    //context.InsertTo(product);//1650
                    }

                    print("Inserting Done");
                }
            }


            new ScenarioJoins(print).
                RunAll(context.Products.AsNoTracking(), context.Categories.AsNoTracking());


            print(".Include");
            var result = context.Products.AsNoTracking()
                .Include(x => x.Category)
                .ToList();
            print($".Include Count  {result.Count}");

        }
    }



}

