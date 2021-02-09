using System;
using System.Threading;
using CoreLibrary;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Creating Instance of Publisher
                var arbitraryStock = new Publisher("RY", 14.30);

                //Create Instances of Subscribers
                var sub1 = new Subscriber("User1", Subscriber.TriggerType.Any); //should return 10 items
                var sub2 = new Subscriber("User2", Subscriber.TriggerType.Sell); //should return 5 items (sell)
                var sub3 = new Subscriber("User3", Subscriber.TriggerType.Buy); //should return 5 items (buy)
                var sub4 = new Subscriber("User4", Subscriber.TriggerType.Sell, 0.3); //should return 2 items (sell)

                //Pass the publisher obj to their Subscribe function
                sub1.Subscribe(arbitraryStock);
                sub2.Subscribe(arbitraryStock);
                sub3.Subscribe(arbitraryStock);
                sub4.Subscribe(arbitraryStock);

                //let's change some values
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.27); //0.21
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.26); //0.07
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.25); //0.07
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.26); //0.07
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.25); //0.07
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.26); //0.07
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.24); //0.14
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.25); //0.07
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.35); //0.7
                Thread.Sleep(200);
                arbitraryStock.UpdateValue(14.40); //0.35
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Exiting....");
            }

            Console.ReadKey();
        }
    }
}
