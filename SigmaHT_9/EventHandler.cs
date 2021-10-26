using System;
using System.Collections.Generic;
using System.IO;

namespace SigmaHT_9
{
    class EventHandler
    {
        public static void Storage_SearchForExpiredProducts(List<Product> products, string fileName)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, true))
            {
                streamWriter.WriteLine("\nExpired products:\n");

                for (int i = 0; i < products.Count; i++)
                {
                    if (DateTime.Now > products[i].ProductionDate.AddDays(products[i].ExpirationDate))
                    {
                        products.Remove(products[i]);
                        i--;
                        streamWriter.WriteLine(products[i] + "\n");
                    }
                }
            }
            
        }

        public static void FileWorker_Notify(string fileName, string wrongLine, string message)
        {
            using (StreamWriter streamWriter = new StreamWriter(fileName, true))
            {
                string output = $"[{DateTime.Now}]\nWrong input in line:\n{wrongLine}\nLogMessage: {message}";

                streamWriter.WriteLine(output);
            }
        }

        public static void FileWorker_CorrectData(Storage storage, string wrongLine, string productClass)
        {
            Console.WriteLine($"Wrong input in line:\n{wrongLine}\n");

            string name;
            double price, weight;
            int expirationDate, day, month, year;
            DateTime productionDate;
            MeatCategory meatCategory = MeatCategory.ExtraClass;
            KindOfMeat kindOfMeat = KindOfMeat.Chicken;
            Product product;

            Console.WriteLine("Enter new name:");
            name = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("Enter new price:");
                if (double.TryParse(Console.ReadLine(), out price))
                    break;
            }
            while (true)
            {
                Console.WriteLine("Enter new weight:");
                if (double.TryParse(Console.ReadLine(), out weight))
                    break;
            }
            while (true)
            {
                Console.WriteLine("Enter new expiration date:");
                if (int.TryParse(Console.ReadLine(), out expirationDate))
                    break;
            }
            while (true)
            {
                Console.WriteLine("Enter new production date:");

                string[] dateTime = Console.ReadLine().Split(':');

                if (!int.TryParse(dateTime[0], out day))
                    continue;

                if (!int.TryParse(dateTime[1], out month))
                    continue;

                if (!int.TryParse(dateTime[2], out year))
                    continue;

                productionDate = new DateTime(year, month, day);
                break;
            }

            if (productClass == "Meat:")
            {
                while (true)
                {
                    Console.WriteLine("Enter new meat category:");
                    switch (Console.ReadLine())
                    {
                        case "ExtraClass": meatCategory = MeatCategory.ExtraClass; break;
                        case "FisrtCalss": meatCategory = MeatCategory.FisrtCalss; break;
                        case "SecondClass": meatCategory = MeatCategory.SecondClass; break;
                        default:
                            continue;
                    }
                    break;
                }

                while (true)
                {
                    Console.WriteLine("Enter new king of meat:");
                    switch (Console.ReadLine())
                    {
                        case "Mutton": kindOfMeat = KindOfMeat.Mutton; break;
                        case "Veal": kindOfMeat = KindOfMeat.Veal; break;
                        case "Pork": kindOfMeat = KindOfMeat.Pork; break;
                        case "Chicken": kindOfMeat = KindOfMeat.Chicken; break;
                        default:
                            continue;
                    }
                    break;
                }
            }

            switch (productClass)
            {
                case "Meat:":
                    product = new Meat(name, price, weight, expirationDate,
          productionDate, meatCategory, kindOfMeat);
                    break;
                case "Dairy:":
                    product = new Dairy_products(name, price, weight, expirationDate,
         productionDate);
                    break;
                default:
                    product = new Product(name, price, weight, expirationDate, productionDate);
                    break;
            }

            storage.Add(product);
        }
    }
}
