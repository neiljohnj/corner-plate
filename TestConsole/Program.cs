using CornerPlate.Domain.Entities;
using CornerPlate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var options = DatabaseConfiguration.CreateOptions("Test");

// Write with one DbContext
//await using (var db = new AppDbContext(options))
//{
//    var category = new MenuCategory("Burgers");

//    try
//    {
//        db.MenuCategories.Add(category);
//        await db.SaveChangesAsync();

//        Console.WriteLine("Category saved!");
//    }
//    catch (DbUpdateException ex)
//        when (ex.InnerException is PostgresException
//        {
//            SqlState: PostgresErrorCodes.UniqueViolation
//        } pgEx)
//    {
//        Console.WriteLine(
//            $"A unique constraint was violated: {pgEx.ConstraintName}");
//    }
//}

// Read with a completely new DbContext
await using (var db = new AppDbContext(options))
{
    var categories = await db.MenuCategories
        .AsNoTracking()
        .OrderBy(mc => mc.SortOrder)
        .ThenBy(mc => mc.Name)
        .Include(mc => mc.Products.OrderBy(p => p.Name))
        .ToListAsync();

    if (categories.Count == 0)
    {
        Console.WriteLine("There are currently no categories saved on this database!");
    }
    else
    {
        foreach (MenuCategory mc in categories)
        {
            Console.WriteLine($"Menu Category '{mc.Name}'");
            
            if (mc.Products.Count == 0)
            {
                Console.WriteLine("This category has no products yet!");
            }
            else
            {
                Console.WriteLine("Products under this category: ");

                int i = 1;

                foreach (MenuProduct menuProduct in mc.Products)
                {
                    string available = menuProduct.IsAvailable ? "Available" : "Not Available";

                    Console.WriteLine($"{i++}. {menuProduct.Name} - {menuProduct.CurrentPrice} ({available})");
                }
            }
        }
    }   
}