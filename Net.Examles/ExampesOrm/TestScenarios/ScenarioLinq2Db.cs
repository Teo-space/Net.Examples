using ExampesOrm.Models;
using Net.Examles.ExampesOrm.Factories;
using Net.Examles.Tools.BenchMark;
using Net.OrmTests.Orms.Linq2Db.Contexts;


namespace Net.Examles.ExampesOrm.TestScenarios;


public record ScenarioLinq2Db(
                                ILogger<ScenarioLinq2Db> logger, 
                                Linq2DBContext context) : Handler
{


    void print(string message) => logger.Info(message);


    static Random Random { get; } = new Random();


    public async Task Handle(CancellationToken token)
    {
        var Categories = CategoryFactory.Categories();

        print("Linq2DB");

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

                context.Products.BulkCopy(Products);
                //context.Products.CopyTo(Products.ToArray());

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
                        ProductId = Guid.NewGuid(),
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



        print("PriceStats");
        var PriceStats = context.Products
            .LoadWith(x => x.Category)
            .GroupBy(p => p.Category.Name)
            .Select(gr => new
            {
                CategoryName = gr.Key,
                count = gr.Count(),
                sum = gr.Sum(p => p.Price),
                avg = gr.Average(p => p.Price),
                min = gr.Min(p => p.Price),
                max = gr.Max(p => p.Price),
            })
            .ToList()
            ;
        print("PriceStats");

        foreach (var x in PriceStats.OrderBy(x => x.CategoryName))
        {
            print($"{x.CategoryName}  count: {x.count}  sum: {x.sum}  avg: {x.avg}  min: {x.min}  max: {x.max}");
        }


        context.Categories.Drop();
        context.Products.Drop();

    }






}

