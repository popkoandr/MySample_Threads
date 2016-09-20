using System;
using System.Threading;

// Есть два варианта работы потоков Foreground и Background
// Foreground - Будет работать после завершения работы первичного потока.
// Background - Завершает работу вместе с первичным потоком.

namespace ThreadBackground
{
    class Program
    {
        private static void Function()
        {
            for (int i = 0; i < 500; i++)
            {
                Thread.Sleep(10);
                Console.Write(".");
            }
            Console.WriteLine("\nЗавершение вторичного потока.");
        }

        static void Main_Sample1()
        {
            var thread = new Thread(Function);

            // IsBackground - устанавливает поток как фоновый. Не ждем завершения вторичного потока в данном случае.
            // По умолчанию - thread.IsBackground = false; 

            thread.IsBackground = true; // 
            thread.Start();

            Thread.Sleep(500);

            Console.WriteLine("\nЗавершение главного потока.");
        }
        static void Main()
        {
            Main_Sample1();
            Priority.Main_Sample2();
        }
    }
}
