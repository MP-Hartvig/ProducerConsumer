using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    internal class Program
    {
        public static bool terminator = false;

        public static Queue<bool> producedGoods = new Queue<bool>();

        public static int attempts = 0;

        public static void Produce()
        {
            Random rand = new Random();

            while (terminator == false)
            {
                if (producedGoods.Count < 3)
                {
                    producedGoods.Enqueue(true);
                    Console.WriteLine($"Item has been produced.");
                    Thread.Sleep(100 / 15);
                }
                else
                {
                    attempts++;
                    if (attempts > 10)
                    {
                        attempts = 0;
                        Console.WriteLine("Producer on break");
                        Thread.Sleep(3000);
                        continue;
                    }
                    Console.WriteLine("Producer wasn't allowed to produce.");
                    Thread.Sleep(100 / 15);
                }
            }
        }

        public static void Consume()
        {
            Random rand = new Random();

            while (terminator == false)
            {
                if (producedGoods.Count == 0)
                {
                    Console.WriteLine("No items to consume");
                }
                else
                {
                    if (producedGoods.Count >= 0)
                    {
                        while (producedGoods.Count > 0)
                        {
                            producedGoods.Dequeue();
                            Console.WriteLine($"Item has been consumed.");
                            Thread.Sleep(100 / 15);
                        }
                    }
                }
                Thread.Sleep(2000);
            }
        }

        static void Main(string[] args)
        {
            Thread producer = new Thread(Produce);
            producer.Name = "Producer";
            producer.Start();

            Thread consumer = new Thread(Consume);
            consumer.Name = "Consumer";
            consumer.Start();

            while (terminator == false)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                {
                    terminator = true;
                }
            }

            try
            {
                producer.Join();
                consumer.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }
}
