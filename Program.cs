List<Product> products = new List<Product>()
{
  new Product()
  {
    Name = "Unicorn Wand",
    Price = 100.00M,
    Sold = false,
    ProductTypeId = 4,
    DateStocked = new DateTime(2022, 11, 10)
  },
  new Product()
  {
    Name = "Health Potion",
    Price = 11.00M,
    Sold = false,
    ProductTypeId = 2,
    DateStocked = new DateTime(2023, 1, 20)
  },
  new Product()
  {
    Name = "Pendulum of Time Travel",
    Price = 1.00M,
    Sold = false,
    ProductTypeId = 3,
    DateStocked = new DateTime(2023, 9, 27)
  },
  new Product()
  {
    Name = "Enchanted Helm",
    Price = 1.00M,
    Sold = false,
    ProductTypeId = 1,
    DateStocked = new DateTime(2021, 10, 30)
  },
  new Product()
  {
    Name = "Mana Potion",
    Price = 15.00M,
    Sold = true,
    ProductTypeId = 2,
    DateStocked = new DateTime(2023, 10, 11)
  }
};

List<ProductType> productTypes = new List<ProductType>()
{
  new ProductType()
  {
    Name = "apparel",
    Id = 1
  },
  new ProductType()
  {
    Name = "potions",
    Id = 2
  },
  new ProductType()
  {
    Name = "enchanted objects",
    Id = 3
  },
  new ProductType()
  {
    Name = "wands",
    Id = 4
  }
};

string greeting = "Welcome to Reductio & Absurdum Magical Supplies!";
Console.WriteLine(greeting);

string userChoice = null;
while (userChoice != "0")
{
  Console.WriteLine(@"Choose an option:
                      0. Exit
                      1. View all products
                      2. Add a product to the inventory
                      3. Delete a product from the inventory
                      4. Update a product's details
                      5. Look up product by type
                      6. View all available products");
  userChoice = Console.ReadLine();
  Console.Clear();
  switch (userChoice)
  {
    case "0":
      Console.WriteLine("Goodbye from R&A!");
      break;
    case "1":
      if (products.Count > 0)
      {
        ListAllProducts();
      }
      else
      {
        Console.WriteLine("No products to list");
      }
      break;
    case "2":
      AddProduct();
      break;
    case "3":
      DeleteProduct();
      break;
    case "4":
      UpdateProduct();
      break;
    case "5":
      SearchByProductType();
      break;
    case "6":
      ListAllAvailableProducts();
      break;
    default:
      Console.WriteLine(greeting);
      break;
  }
}

void ListAllProductTypes()
{
  Console.WriteLine("Product Types:");
  foreach (ProductType productType in productTypes)
  {
    Console.WriteLine($"{productTypes.IndexOf(productType) + 1}. {productType.Name}");
  }
}

void ListAllProducts()
{
  Console.WriteLine("Products:");
  foreach (Product product in products)
  {
    Console.WriteLine($"{products.IndexOf(product) + 1}. {ProductDetails(product)}");
  }
}

void ListAllAvailableProducts()
{
  List<Product> unsoldProducts = products.Where(p => !p.Sold).ToList();
  foreach (Product product in unsoldProducts)
  {
    Console.WriteLine($"{unsoldProducts.IndexOf(product) + 1}. {ProductDetails(product)}");
  }
}

void AddProduct()
{
  Console.WriteLine("Please enter the details of the product to be added");
  Console.WriteLine("What's the name of the product?");
  string nameOfProductToAdd = Console.ReadLine();
  Console.WriteLine("What's the price of the product?");
  decimal priceOfProductToAdd = decimal.Parse(Console.ReadLine());
  Console.WriteLine("What product Id # is the product?");
  ListAllProductTypes();
  int productTypeIdOfProductToAdd = int.Parse(Console.ReadLine());

  Product productToAdd = new Product();
  productToAdd.Name = nameOfProductToAdd;
  productToAdd.Price = priceOfProductToAdd;
  productToAdd.ProductTypeId = productTypeIdOfProductToAdd;
  productToAdd.Sold = false;

  products.Add(productToAdd);
  Console.WriteLine("You successfully added the product");
}

string ProductDetails(Product product)
{
  string currentProductType = productTypes
    .Where(productType => productType.Id == product.ProductTypeId)
    .Select(productType => productType.Name)
    .FirstOrDefault() ?? "";

  // string currentProductType = "";
  // foreach (ProductType productType in productTypes)
  // {
  //   if (product.ProductTypeId == productType.Id)
  //   {
  //     currentProductType = productType.Name;
  //   }
  // };
  string productString = $"{product.Name} ({currentProductType}) ${product.Price}, Days On Shelf: {product.DaysOnShelf}";
  return productString;
}

void DeleteProduct()
{
  Console.WriteLine("Type of number of the product would you like to delete:");
  ListAllProducts();
  int userInput = int.Parse(Console.ReadLine());
  Product productToDelete = products.FirstOrDefault((product) => userInput == products.IndexOf(product) + 1);
  if (productToDelete != null)
  {
    products.Remove(productToDelete);
  }
  // foreach (Product product in products)
  // {
  //   if (userInput == products.IndexOf(product) + 1)
  //   {
  //     products.Remove(product);
  //     return;
  //   }
  // }
}

void UpdateProduct()
{
  Console.WriteLine("Type the number of the product to be updated:");
  ListAllProducts();
  int userInput = int.Parse(Console.ReadLine());
  Product chosenProduct = null;
  int chosenProductIndex = 0;
  Product foundProduct = products.FirstOrDefault((product) => userInput == products.IndexOf(product) + 1);
  if (foundProduct != null)
  {
    chosenProduct = foundProduct;
    int foundProductIndex = products.IndexOf(foundProduct) + 1;
  }
  
  // foreach (Product product in products)
  // {
  //   if (userInput == products.IndexOf(product) + 1)
  //   {
  //     chosenProduct = product;
  //     chosenProductIndex = products.IndexOf(product);
  //   }
  // }
  Console.WriteLine("Which property of the product do you want to update?");
  Console.WriteLine($"1. {nameof(chosenProduct.Name)}");
  Console.WriteLine($"2. {nameof(chosenProduct.Price)}");
  Console.WriteLine($"3. {nameof(chosenProduct.Sold)}");
  Console.WriteLine($"4. {nameof(chosenProduct.ProductTypeId)}");
  int productPropertyInt = int.Parse(Console.ReadLine());
  switch (productPropertyInt)
  {
    case 1:
      Console.WriteLine("Type the updated Product Name:");
      string updatedName = Console.ReadLine();
      products[chosenProductIndex].Name = updatedName;
      break;
    case 2:
      Console.WriteLine("Type the updated Product Price:");
      decimal updatedPrice = decimal.Parse(Console.ReadLine());
      products[chosenProductIndex].Price = updatedPrice;
      break;
    case 3:
      Console.WriteLine("Has the Product been sold? Type 'yes' or 'no'");
      string updatedSold = Console.ReadLine();
      if (updatedSold.ToLower() == "yes")
      {
        products[chosenProductIndex].Sold = true;
      }
      else if (updatedSold.ToLower() == "no")
      {
        products[chosenProductIndex].Sold = false;
      }
      break;
    case 4:
      Console.WriteLine("Type the number of the Product Type you'd like to change your product to:");
      ListAllProductTypes();
      int updatedProductTypeId = int.Parse(Console.ReadLine());
      switch (updatedProductTypeId)
      {
        case 1:
          products[chosenProductIndex].ProductTypeId = updatedProductTypeId;
          break;
        case 2:
          products[chosenProductIndex].ProductTypeId = updatedProductTypeId;
          break;
        case 3:
          products[chosenProductIndex].ProductTypeId = updatedProductTypeId;
          break;
        case 4:
          products[chosenProductIndex].ProductTypeId = updatedProductTypeId;
          break;
        default:
          Console.WriteLine("Not a given option. Returning to main menu");
        return;
      }
      break;
    default:
      Console.WriteLine("Not a given option. Returning to main menu");
      return;
  }
  Console.WriteLine($"You've updated {ProductDetails(chosenProduct)}");
}

void SearchByProductType()
{
  Console.WriteLine("Type the number of the types of products to show:");
  ListAllProductTypes();
  int userInput = int.Parse(Console.ReadLine());
  List<Product> productsMatchingChosenType = products.Where(p => p.ProductTypeId == userInput).ToList();
  // List<Product> productsMatchingChosenType = new List<Product>();
  // foreach (Product product in products)
  // {
  //   if (product.ProductTypeId == userInput)
  //   {
  //     productsMatchingChosenType.Add(product);
  //   }
  // }
  Console.WriteLine(@$"Here's a list of product matching product type:");
  foreach (Product product in productsMatchingChosenType)
  {
    Console.WriteLine(@$"{ProductDetails(product)}");
  }
}