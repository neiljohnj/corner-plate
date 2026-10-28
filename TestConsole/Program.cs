using CornerPlate.Application.Menus.DTOs;
using CornerPlate.Application.Menus.Services;
using CornerPlate.Infrastructure.Persistence;
using CornerPlate.Infrastructure.Persistence.Repositories;

var options = DatabaseConfiguration.CreateOptions("Test");

// Read with a completely new DbContext
await using var db = new AppDbContext(options);

var menuCategoryRepository = new MenuCategoryRepository(db);

var mcService = new MenuCategoryService(
    menuCategoryRepository,
    db);

var mpService = new MenuProductService(
    new MenuProductRepository(db),
    menuCategoryRepository,
    db);

var categories = await mcService.GetAllWithProductsAsync();

// Creating a new bundle


// Create a new bundle product
//var newBundleProduct = await mpService.CreateAsync(new CreateMenuProductRequest(
//    categories.ElementAt(0).Id,
//    "Burger Combo Meal A",
//    null,
//    CornerPlate.Domain.Enums.ProductType.Bundle,
//    255.55m,
//    "VAT"
//    ));

//var fries = categories.ElementAt(0);
//var burger = categories.ElementAt(1);
//var burgers = burger.Products;


// Creating a new product

//var beverage = await mpService.CreateAsync(new CreateMenuProductRequest(
//        categories.ElementAt(2).Id,
//        "Nestea Medium",
//        null,
//        CornerPlate.Domain.Enums.ProductType.Single,
//        75.0m,
//        "VAT"
//    ));


//if (categories.Count == 0)
//{
//    Console.WriteLine("There are currently no categories saved on this database!");
//}
//else
//{
//    foreach (MenuCategoryWithProductsDTO mc in categories)
//    {
//        Console.WriteLine($"{mc.Name} - Active: {mc.IsActive}");

//        if (mc.Products.Count == 0)
//            Console.WriteLine("This category has no products yet!");
//        else
//        {
//            Console.WriteLine("This category's products are:");
//            int i = 1;

//            foreach (var item in mc.Products)
//            {
//                Console.WriteLine($"[{i++}] {item.Name} ({item.CurrentPrice})");
//            }
//        }
//    }
//}

//var productA = categories.ElementAt(0).Products.ElementAt(0);

//Console.WriteLine($"Changing price of {productA.Name} from" +
//    $"{productA.CurrentPrice} to {{{185.55m}}}");

//// Change one into different shit...
//if (await mpService.ChangePriceAsync(productA.Id, 185.55m))
//    Console.WriteLine("Price changed!");

//var productB = categories.ElementAt(0).Products.ElementAt(1);

//Console.WriteLine($"Making product {productB.Name} unavaialble...");

//if (await mpService.SetAvailabilityAsync(productB.Id, false))
//    Console.WriteLine("Product is now unavailable...");

//// Show changes
//var categoryA = categories.ElementAt(0);

//int x = 1;

//foreach (var item in categoryA.Products)
//{
//    string available = item.IsAvailable ? "Available" : "Not Available";
//    Console.WriteLine($"[{x++}] {item.Name} ({item.CurrentPrice}) - {available}");
//}