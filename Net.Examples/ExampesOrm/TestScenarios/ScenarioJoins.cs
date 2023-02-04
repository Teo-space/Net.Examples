using ExampesOrm.Models;
using Net.Examles.Tools.BenchMark;

namespace Net.Examles.ExampesOrm.TestScenarios;

record ScenarioJoins(Action<string> action)
{
    void print(string message) => action?.Invoke(message);

    public void RunAll(IQueryable<Product> Products, IQueryable<Category> Categories)
    {
        var Chain = new BenchChain();
        Chain.Add("CrossJoin", () => CrossJoin(Products.AsNoTracking(), Categories.AsNoTracking()));
        Chain.Add("InnerJoin", () => InnerJoin(Products.AsNoTracking(), Categories.AsNoTracking()));
        Chain.Add("InnerJoinFromFrom", () => InnerJoinFromFrom(Products.AsNoTracking(), Categories.AsNoTracking()));
        Chain.Add("LeftJoin", () => LeftJoin(Products.AsNoTracking(), Categories.AsNoTracking()));
        Chain.Add("LeftJoinFromFrom", () => LeftJoinFromFrom(Products.AsNoTracking(), Categories.AsNoTracking()));
        Chain.Add("GroupJoin", () => GroupJoin(Products.AsNoTracking(), Categories.AsNoTracking()));
        Chain.Run();

        print(Chain.ToString());
    }


    record ProductCategoryDto(Product Product, Category Category);

    public void CrossJoin(IQueryable<Product> Products, IQueryable<Category> Categories)
    {
        var result = (from product in Products
                      .Take(100)
                      from category in Categories
                      select new ProductCategoryDto(product, category))
                      .Take(50000)
                      .ToList();
    }

    public void InnerJoin(IQueryable<Product> Products, IQueryable<Category> Categories)
    {
        var result = (from product in Products
                      join category in Categories
                      on product.CategoryId equals category.CategoryId
                      select new ProductCategoryDto(product, category))
                      .ToList();
    }

    public void InnerJoinFromFrom(IQueryable<Product> Products, IQueryable<Category> Categories)
    {
        var result = (from product in Products
                      from category in Categories
                      where product.CategoryId == category.CategoryId
                      select new ProductCategoryDto(product, category))
                      .ToList();
    }

    public void LeftJoin(IQueryable<Product> Products, IQueryable<Category> Categories)
    {
        var result = (from product in Products
                      join category in Categories
                      on product.CategoryId equals category.CategoryId
                      into CategoryCatGroup
                      from category in CategoryCatGroup.DefaultIfEmpty()
                      select new ProductCategoryDto(product, category))
                      .ToList();
    }

    public void LeftJoinFromFrom(IQueryable<Product> Products, IQueryable<Category> Categories)
    {
        var result = (from product in Products
                      from category in Categories.DefaultIfEmpty()
                      where product.CategoryId == category.CategoryId
                      select new ProductCategoryDto(product, category))
                      .ToList();
    }

    public void GroupJoin(IQueryable<Product> Products, IQueryable<Category> Categories)
    {
        var result = (from product in Products
                      join category in Categories
                      on product.CategoryId equals category.CategoryId
                      into CategoryGroup
                      select new
                      {
                          product,
                          CategoryGroup
                      })
                      .ToList();
    }


}
