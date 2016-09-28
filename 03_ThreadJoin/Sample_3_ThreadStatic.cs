using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadJoin
{
    class Sample_3_ThreadStatic
    {
        [ThreadStatic]
        public static int Counter;

        // Рекурсивный запуск потоков.
        static void Method()
        {
            // в цикле сразу создастся один поток, потом он вызовет 3, 3-й вызовет 4, и тд. при том каждый будет ждать пока закончится вызваный им поток. 
            // то есть тут создастся 100 потоков, а потом в обратном порядке начнут закрываться.

            if (Counter < 100)
            {
                Counter++; // Увеличение счетчика вызваных методов.
                Console.WriteLine(Counter + " - СТАРТ --- " + Thread.CurrentThread.GetHashCode());
                var thread = new Thread(Method);
                thread.Start();
                thread.Join(); // Закомментировать.               
            }

            Console.WriteLine("Поток {0} завершился.", Thread.CurrentThread.GetHashCode());
        }

        public static void Main_Sample3()
        {
            // Запуск вторичного потока.
            // 
            var thread = new Thread(Method);
            thread.Start();
            thread.Join();

            Console.WriteLine("Основной поток завершил работу...");

            // Задержка.
            Console.ReadKey();
        }
    }
}
