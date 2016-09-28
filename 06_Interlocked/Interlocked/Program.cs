using System;
using System.Threading;

// Interlocked - Предоставляет атомарные операции для переменных, общедоступных нескольким потокам. 

namespace InterLocked
{
    class Program
    {
        // Счетчик запущеных потоков.
        private static long counter;
        private static readonly Random random = new Random();

        private static void Function()
        {
           
            // Поток увеличивает счетчик.
          // Interlocked.Increment(ref counter);
     
            counter++;
            try
            {
                // Поток ожидает произвольный период времени от 1 до 12 секунд.
                int time = random.Next(1, 12);
                Thread.Sleep(time);
            }
            finally
            {
                // Поток уменьшает счетчик. 
               // Interlocked.Decrement(ref counter);
                counter--;
            }
        }

        // Проверка количества запущеных потоков. 
        private static void Report()
        {
            while (true)
            {
                long number = Interlocked.Read(ref counter);

                Console.WriteLine("{0} поток(ов) активно.", number);
                Thread.Sleep(100);
            }
        }

        static void Main()
        {
            var reporter = new Thread(Report) {IsBackground = true};
            reporter.Start();

            var threads = new Thread[150];

            for (uint i = 0; i < 150; ++i)
            {
                threads[i] = new Thread(Function);
                threads[i].Start();
            }

            Thread.Sleep(15000);
        }
    }
}
