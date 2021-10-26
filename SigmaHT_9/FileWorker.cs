using System;
using System.IO;

namespace SigmaHT_9
{
    class FileWorker
    {
        public delegate void PrintIncorrectDataInFile(string fileName, string wrongLine, string message);
        public delegate void ModifyInput(Storage storage, string wrongLine, string productClass);

        public event PrintIncorrectDataInFile Notify;
        public event ModifyInput CorrectData;
        public string Text { get; private set; }

        public FileWorker(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                Text = reader.ReadToEnd();
            }
        }
        public Storage GetProducts()
        {
            string[] products = Text.Replace("\r\n", "\n").Split('\n');

            Storage storage = new Storage();

            Product product;

            for (int i = 0; i < products.Length; i++)
            {
                string[] values = products[i].Split(' ');

                string name = values[1];

                if (!double.TryParse(values[2], out double price))
                {
                    Notify?.Invoke("LogStorage.txt", products[i], "Wrong price");
                    CorrectData?.Invoke(storage, products[i], values[0]);
                    continue;
                }

                if (!double.TryParse(values[3], out double weight))
                {
                    Notify?.Invoke("LogStorage.txt", products[i], "Wrong weight");
                    CorrectData?.Invoke(storage, products[i], values[0]);
                    continue;
                }

                if (!int.TryParse(values[4], out int expirationDate))
                {
                    Notify?.Invoke("LogStorage.txt", products[i], "Wrong expiration date");
                    CorrectData?.Invoke(storage, products[i], values[0]);
                    continue;
                }

                string[] dateTime = values[5].Split(':');

                if (!int.TryParse(dateTime[0], out int day))
                {
                    Notify?.Invoke("LogStorage.txt", products[i], "Wrong number of days");
                    CorrectData?.Invoke(storage, products[i], values[0]);
                    continue;
                }

                if (!int.TryParse(dateTime[1], out int month))
                {
                    Notify?.Invoke("LogStorage.txt", products[i], "Wrong number of months");
                    CorrectData?.Invoke(storage, products[i], values[0]);
                    continue;
                }

                if (!int.TryParse(dateTime[2], out int year))
                {
                    Notify?.Invoke("LogStorage.txt", products[i], "Wrong number of years");
                    CorrectData?.Invoke(storage, products[i], values[0]);
                    continue;
                }

                DateTime productionDate = new DateTime(year, month, day);

                MeatCategory meatCategory = MeatCategory.ExtraClass;
                KindOfMeat kindOfMeat = KindOfMeat.Chicken;

                if (products[i].Split(' ')[0] == "Meat:")
                {
                    switch (values[6])
                    {
                        case "ExtraClass": meatCategory = MeatCategory.ExtraClass; break;
                        case "FisrtCalss": meatCategory = MeatCategory.FisrtCalss; break;
                        case "SecondClass": meatCategory = MeatCategory.SecondClass; break;
                        default:
                            Notify?.Invoke("LogStorage.txt", products[i], "Wrong meat category");
                            CorrectData?.Invoke(storage, products[i], values[0]);
                            continue;
                    }

                    switch (values[7])
                    {
                        case "Mutton": kindOfMeat = KindOfMeat.Mutton; break;
                        case "Veal": kindOfMeat = KindOfMeat.Veal; break;
                        case "Pork": kindOfMeat = KindOfMeat.Pork; break;
                        case "Chicken": kindOfMeat = KindOfMeat.Chicken; break;
                        default:
                            Notify?.Invoke("LogStorage.txt", products[i], "Wrong king of meat");
                            CorrectData?.Invoke(storage, products[i], values[0]);
                            continue;
                    }
                }

                switch (products[i].Split(' ')[0])
                {
                    case "Meat:": product = new Meat(name,price,weight,expirationDate,
                        productionDate, meatCategory,kindOfMeat); 
                        break;
                    case "Dairy:": product = new Dairy_products(name, price, weight, expirationDate, 
                        productionDate); 
                        break;
                    default:
                        product = new Product(name, price, weight, expirationDate, productionDate); 
                        break;
                }

                storage[i] = product;
            }

            return storage;
        }
    }
}
