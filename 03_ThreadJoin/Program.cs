using System;
using System.Threading;

namespace ThreadJoin
{
    class Program
    {
        // простой пример джоина
        private static void ThreadFunc()
        {
            Console.WriteLine("ID Вторичного потока: {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ForegroundColor = ConsoleColor.Yellow;

            for (int i = 0; i < 160; i++)
            {
                Thread.Sleep(20);
                Console.Write(".");
            }

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("Вторичный поток завершился.");
        }

        static void Main_Sample1()
        {
            Console.WriteLine("ID Первичного потока: {0} \n", Thread.CurrentThread.GetHashCode());
            Console.WriteLine("Запуск нового потока...");

            // Создание нового потока.
            var thread = new Thread(new ThreadStart(ThreadFunc));
            thread.Start();

            Console.WriteLine(Thread.CurrentThread.GetHashCode());

            // Ожидание первичным потоком, завершения работы вторичного потока.
            thread.Join(); // то есть этот поток в котором главная функция, будет ждать пока выполнится поток второй.

            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < 160; i++)
            {
                Thread.Sleep(20);
                Console.Write("-");
            }

            Console.ForegroundColor = ConsoleColor.Gray;

            Console.WriteLine("\nПервичный поток завершился.");

            // Задержка.
            Console.ReadKey();
        }
        static void Main()
        {
            //Main_Sample1();
            //Sample_2.Main_Sample2();
            Sample_3_ThreadStatic.Main_Sample3();
        
        }
    }
}
