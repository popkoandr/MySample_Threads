using System;
using System.Threading;

namespace SemaphoreSlimSample
{
    class Program
    {
        // SemaphoreSlim  - легковесный класс-семафор, который не использует объекты синхронизации ядра.
        // Логика как и с обычным 
        static readonly SemaphoreSlim slim = new SemaphoreSlim(1, 2);

        static void Main()
        {
            for (int i = 1; i <= 10; i++)
            {
                Thread thread = new Thread(Function) {Name = i.ToString()};
                thread.Start();
            }

            Thread.Sleep(1000);
            slim.Release(1);  
            
            Console.ReadKey();
        }

        static void Function()
        {
            slim.Wait();

            Console.WriteLine("Поток {0} начал работу.", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            Console.WriteLine("Поток {0} закончил работу.\n", Thread.CurrentThread.Name);

            slim.Release();
        }
    }
}
