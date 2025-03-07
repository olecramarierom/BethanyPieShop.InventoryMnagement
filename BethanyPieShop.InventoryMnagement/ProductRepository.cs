﻿using BethanyPieShop.InventoryManagement.Domain.Contracts;
using BethanyPieShop.InventoryManagement.Domain.ProductManagement;
using BethanyPieShop.InventoryMnagement.Domain.General;
using BethanyPieShop.InventoryMnagement.Domain.ProductManagement;
using System.Text;

namespace BethanyPieShop.InventoryMnagement
{
    internal class ProductRepository
    {
        private string directory = @"C:\iecourses\";
        private string productFileName = "products.txt";

        private void CheckForExistingProductFile()
        {
            string path = $"{directory}{productFileName}";

            bool existingFileFound = File.Exists(path);

            if (!existingFileFound)
            {
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(directory);

                using FileStream fs = File.Create(path);
            }
        }

        public List<Product> LoadProductFromFile()
        {

            List<Product> products = new List<Product>();

            string path = Path.Combine(directory, productFileName);

            try
            {
                CheckForExistingProductFile();

                string[] producAsString = File.ReadAllLines(path);

                for (int i = 0; i < producAsString.Length; i++)
                {
                    string[] productSplits = producAsString[i].Split(";");

                    bool success = int.TryParse(productSplits[0], out int productId);
                    if (!success)
                    {
                        productId = 0;
                    }

                    string name = productSplits[1];
                    string descreption = productSplits[2];

                    success = int.TryParse(productSplits[3], out int maxItemInStock);

                    if (!success)
                    {
                        maxItemInStock = 100;
                    }

                    success = int.TryParse(productSplits[4], out int itemPrice);

                    if (!success)
                    {
                        itemPrice = 0;
                    }

                    success = Enum.TryParse(productSplits[5], out Currency currency);

                    if (!success)
                    {
                        currency = Currency.Dollar;
                    }

                    success = Enum.TryParse(productSplits[6], out UnitType unitType);

                    if (!success)
                    {
                        unitType = UnitType.PerItem;
                    }

                    string productType = productSplits[7];

                    Product? product = null;

                    switch (productType)
                    {
                        case "1":
                            success = int.TryParse(productSplits[8], out int amountPerBox);
                            if (!success)
                            {
                                amountPerBox = 1;
                            }

                            product = new BoxedProduct(productId, name, descreption, new Price() { ItemPrice = itemPrice, Currency = currency}, maxItemInStock, amountPerBox);
                            break;

                        case "2":
                            product = new FreshProduct(productId, name, descreption, new Price() { ItemPrice = itemPrice, Currency = currency}, unitType, maxItemInStock);
                            break;

                        case "3":
                            product = new BulkProduct(productId, name, descreption, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemInStock);
                            break;
                        
                        case "4":
                            product = new RegularProduct(productId, name, descreption, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemInStock);
                            break;

                    }

                    //Product product = new Product(productId, name, descreption, new Price() { ItemPrice = itemPrice, Currency = currency }, unitType, maxItemInStock);

                    products.Add(product);
                }
            }
            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong parsinfg the file, please check the data!");
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The file couldn't be found!");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return products;
        }

        public void SaveToFile(List<ISavable> savables)
        {
            StringBuilder sb = new StringBuilder();
            string path = $"{directory}{productFileName}";

            foreach (var item in savables) { 
                sb.Append(item.ConvertToStringForSaving());
                sb.Append(Environment.NewLine);
            }

            File.WriteAllText(path, sb.ToString());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Save items successfully");
            Console.ResetColor();
        }
    }
}
