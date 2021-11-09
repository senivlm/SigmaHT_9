using System;

namespace SigmaHT_9
{
    class Program
    {
        static void Main(string[] args)
        {
            FileWorker fileWorker = new FileWorker(@"C:\Users\User\source\repos\SigmaHT_9\SigmaHT_9\input.txt");

            fileWorker.Notify += EventHandler.FileWorker_Notify;
            fileWorker.CorrectData += EventHandler.FileWorker_CorrectData;

            Storage storage = fileWorker.GetProducts();
// Чому тільки одна подія
            storage.SearchForExpiredProducts += EventHandler.Storage_SearchForExpiredProducts;

            Console.WriteLine(storage);
        }
    }
}
