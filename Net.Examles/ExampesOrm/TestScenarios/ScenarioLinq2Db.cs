using ExampesOrm.Models;
using Net.Examles.ExampesOrm.Factories;
using Net.Examles.Tools.BenchMark;
using Net.OrmTests.Orms.Linq2Db.Contexts;


namespace Net.Examles.ExampesOrm.TestScenarios;



public class ScenarioLinq2Db : BackgroundService
{
    private readonly ILogger<ScenarioLinq2Db> logger;

    public ScenarioLinq2Db(ILogger<ScenarioLinq2Db> logger)
    {
        this.logger = logger;
    }

    void print(string message) => logger.Info(message);

    static Random Random { get; } = new Random();

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var Categories = CategoryFactory.Categories();

        print("Linq2DB");
        using (var context = new Linq2DBContext())//context.LastQuery
        {
            print($"Context");

            if (context.Products.Count() == 0)
            {
                {
                    print($"Categories  {Categories.Count}");

                    //context.Categories.BulkCopy(Categories);
                    context.Categories.CopyTo(Categories.ToArray());

                    print("Categories.BulkCopy");

                    var CategoriesAfterInsert = context.Categories.ToList();

                    var Products = ProductFactory.Products(CategoriesAfterInsert);

                    print($"ProductFactory.Products  {Products.Count}");

                    //context.Products.BulkCopy(Products);
                    context.Products.CopyTo(Products.ToArray());

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
                        context.Insert(product);//4800
                                                //context.Products.Insert();
                                                //context.Products.InsertOrUpdate
                                                //context.Products.CopyTo(product);
                    }

                    print("Inserting Done");
                }
            }

            new ScenarioJoins(print)
                .RunAll(context.Products.AsNoTracking(), context.Categories.AsNoTracking());

            print(".LoadWith");
            var result = context.Products
                .LoadWith(x => x.Category)
                .ToList();
            print($"Count  {result.Count}");


        }

    }






}









/*
		static void Linq2DB()
		{
			print("Linq2DB", ConsoleColor.Cyan);
			using (var context = new Linq2DBContext())//context.LastQuery
			{
				print("Linq2DB Context", ConsoleColor.DarkCyan);

				if (context.Products.Count() == 0)
				{
					Create<BulkCopyTestScenario>().Run(
						context.Products,
						(List<Product> products) => context.Products.BulkCopy(products),
						context.Categories,
						(List<Category> categories) => context.Categories.BulkCopy(categories)
						);


					var categories = context.Categories.ToList();
					InsertTestScenario.Run(categories, (product) => 
					{
						context.Insert(product);//4800
						//context.Products.Insert();
						//context.Products.InsertOrUpdate
						//context.Products.CopyTo(product);
					});
				}


				JoinsTestScenario.Run(context.Products, context.Categories);

				print(".LoadWith", ConsoleColor.Yellow);
				var result = context.Products
					.LoadWith(x => x.Category)
					.ToList();
				print("Done", ConsoleColor.Green);
				print($"Count  {result.Count}", ConsoleColor.Green);

				print("Linq2DB End");
			}
		}



class BulkCopyTestScenario : TestBase
{
    public void Run(
        IQueryable<Product> Products,
        Action<List<Product>> BulkCopyProductsAction,
        IQueryable<Category> Categories,
        Action<List<Category>> BulkCopyCategoriesAction)
    {
        print("BulkCopy Categories", ConsoleColor.Yellow);

        BulkCopyCategoriesAction(TestBase.Categories);

        print("BulkCopy Categories Done", ConsoleColor.Green);

        //print("preparation", ConsoleColor.Yellow);
        var CategoriesAfterInsert = Categories.ToList();
        List<Product> ProductsToBulkInsert = new List<Product>();
        for (int i = 0; i < CountProducts; i++)
        {
            var RandomCategory = CategoriesAfterInsert[Random.Next(0, CategoriesAfterInsert.Count - 1)];

            ProductsToBulkInsert.Add(new Product()
            {
                //ProductId = i,
                Name = $"ProductsName_{i}",
                CategoryId = RandomCategory.CategoryId
            });

        }
        //print("preparation done", ConsoleColor.Green);

        print("BulkCopy Products", ConsoleColor.Yellow);
        BulkCopyProductsAction(ProductsToBulkInsert);
        print("BulkCopy Products Done", ConsoleColor.Green);


        print($"context.Categories.Count: {Categories.Count()}", ConsoleColor.DarkGray);
        print($"context.Products.Count: {Products.Count()}", ConsoleColor.DarkGray);
    }
}
*/