 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 using System.Threading;
 using System.Threading.Tasks;

namespace Threads
{
    class Program
    {
        static void Main(string[] args)
        {
            Main0();// изменить цифру в конце для запуска небходимого примера
        }

        static void Main0()
        {
            ThreadStart writeSecondDelegate = new ThreadStart(WriteSecond0);
            Thread thread = new Thread(writeSecondDelegate);
            thread.Start();

            while (true)
            {
                Console.WriteLine("Primary");
            }
        }
        // method that we are going to execute on another thread / (simple)
        private static void WriteSecond0()
        {
            while (true)
            {
                Console.WriteLine(new string(' ', 10) + "Secondary");
            }
        }


        // Статический метод, который планируется выполнять одновременно в главном (первичном) и во вторичном потоках.
        static void WriteSecond1()
        {
            // CLR назначает каждому потоку свой стек и методы имеют свои собственные локальные переменные.
            // Отдельный экземпляр переменной counter создается в стеке каждого потока, 
            // поэтому для каждого потока выводятся, свои значения counter - 0,1,2.
            int counter = 0;

            while (counter < 10)
            {
                Console.WriteLine("Thread Id {0}, counter = {1}", Thread.CurrentThread.GetHashCode(), counter);
                counter++;
            }
        }
        static void Main1()
        {
            // Работа вторичного потока.
            Thread thread = new Thread(WriteSecond1);
            thread.Start();

            // Работа первичного потока.
            WriteSecond1();


            // Delay.
            Console.ReadKey();
        }

        // Thread.CurrentThread, Thread.Sleep
        static void WriteSecond2()
        {
            // Thread.CurrentThread - возвращает ссылку на экземпляр текущего потока.
            Thread thread = Thread.CurrentThread;

            // Присваиваем потоку имя.
            thread.Name = "Secondary";

            // Выводим на экран информацию о текущем потоке.
            Console.WriteLine("ID потока {0}: {1}", thread.Name, thread.GetHashCode());

            for (int counter = 0; counter < 10; counter++)
            {
                Console.WriteLine(new string(' ', 15) + thread.Name + " " + counter);
                // Приостанавливаем выполнение текущего потока.
                Thread.Sleep(1000);
            }
        }
        static void Main2()
        {
            // Получаем ссылку на экземпляр текущего потока.
            Thread primaryThread = Thread.CurrentThread;

            // Присваиваем потоку имя.
            primaryThread.Name = "Primary";

            // Выводим на экран информацию о текущем потоке.
            Console.WriteLine("ID потока {0}: {1}", primaryThread.Name, primaryThread.GetHashCode());


            // Работа вторичного потока.
            Thread secondaryThread = new Thread(WriteSecond2);
            secondaryThread.Start();

            // Работа первичного потока.
            for (int counter = 0; counter < 10; counter++)
            {
                Console.WriteLine(primaryThread.Name + " " + counter);
                // Приостанавливаем выполнение текущего потока.
                Thread.Sleep(1500);
            }

            // Delay.
            Console.ReadKey();
        }

        //ParametrizedThreadStart
        static void WriteSecond3(object argument)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(argument);
                Thread.Sleep(1000);
            }
        }
        static void Main3()
        {
            ParameterizedThreadStart writeSecond = new ParameterizedThreadStart(WriteSecond3);
            Thread thread = new Thread(writeSecond);
            thread.Start("Hello");

            Thread.Sleep(500);

            // Delay.
            Console.ReadKey();
        }

        // Потоки. Анонимные методы.
        static void Main4()
        {
            int counter = 0;

            // ThreadStart
            Thread thread = new Thread(delegate () { Console.WriteLine("1. counter = {0}", ++counter); });
            thread.Start();

            Thread.Sleep(100);
            Console.WriteLine("2. counter = {0}", counter);

            // ParameterizedThreadStart
            thread = new Thread((object argument) => { Console.WriteLine("3. counter = {0}", (int)argument); });
            thread.Start(counter);

            // Delay.
            Console.ReadKey();
        }

        static void WriteSecond5()
        {
            while (true)
            {
                Console.WriteLine(new string(' ', 15) + "Secondary");
                Thread.Sleep(500);
            }
        }

        //thread.IsBackground если поставить тру -  2 поток остановится после прекращения работы главного, иначе будет постоянно работать.
        static void Main5()
        {
            // Работа вторичного потока.
            ThreadStart writeSecond = new ThreadStart(WriteSecond5);
            Thread thread = new Thread(writeSecond);
            thread.Start();

            // Работа первичного потока.
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("Primary");
                Thread.Sleep(500);
            }

            // Завершить работу вторичного потока
            thread.IsBackground = true;
        }






    }
}
