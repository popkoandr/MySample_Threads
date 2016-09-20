using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CriticalSection
{
    // Критическая секция(critical section).
    // lock - блокирует блок кода так, что в кждый отдельный момент времени, этот блок кода
    // сможет использовать только один поток. Все остальные потоки ждут пока текущий поток, закончит работу.
    // Если забыть убрать Монитор Екзит, то остальные потоки попадают в петлю смерти, не пройти не выйти так сказать 
    // Monitor Enter записывает to object - null, Ехіт возвращает ссылку на объект.
      
        // относится к Sample_1
    class MyClass
    {
        public static object block = new object();// как эстафетная палочка на уроке физ-ры

        //using lock {} (Sample_1)
        public void MethodLock()
        {
            int threadHashCode = Thread.CurrentThread.GetHashCode();
            //если убрать блокировку ресурса - то потоки "по очереди" будут захватывать консоль( разделяемый ресурс) и будет хаос
            lock (block) // тут мы создаем эту самую дорожку по которой сможет бежать с палкой только один ребенок
            {
                for (int counter = 0; counter < 10; counter++)
                {
                    Console.WriteLine("Поток #{0}: шаг {1}", threadHashCode, counter);
                    Thread.Sleep(500);
                }
                Console.WriteLine(new string('-', 20));
            }
        }

        //Sample_1 = Sample_2
        //using Monitor.Enter(block) and Monitor.Exit(block) (Sample_2)
        public void MethodUsingMonitor()
        {
            int hash = Thread.CurrentThread.GetHashCode();

            Monitor.Enter(block); // Закомментировать.

            for (int counter = 0; counter < 10; counter++)
            {
                Console.WriteLine(")) Поток # {0}: шаг {1}", hash, counter);
                Thread.Sleep(100);
            }
            Console.WriteLine(new string('-', 20));

            Monitor.Exit(block);  
        }

        //Sample_3. Тут мы используем блокировку объекта и при вызове 
        public  void WriteSecond()
        {
            for (int i = 0; i < 20; i++)
            {
                lock (block)// если убрать блокировку тут или в другом месте, то в дургой функции, что тоже использует этот ресурс, происходит переизменение цвета.
                    // собственно из за этого цвета будут не соответствовать ожидаемому
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(new string(' ', 10) + "Secondary");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(100);
                }
            }
        }
    }


    class Program
    {
        // Main fuction
        static void Main(string[] args)
        {
            Sample_3();
        }
        

        //Sample_1 (using lock)
        static void Sample_1()
        {
            Console.SetWindowSize(80, 40);

            MyClass instance = new MyClass();
            // cоздаем в цикле 3 потока
            for (int i = 0; i < 3; i++)
            {
                new Thread(instance.MethodLock).Start();// даже если один после другого идут, закономерности нет в захвате ресурса
                new Thread(instance.MethodUsingMonitor).Start();// - - -
            }
            Thread.Sleep(500);
            Console.ReadKey();
        }

        //part of Sample_3
        public static void Sample_3()
        {
            MyClass myClass = new MyClass();
            ThreadStart writeSecond = new ThreadStart(myClass.WriteSecond);
            Thread thread = new Thread(writeSecond);
            thread.Start();

            for (int i = 0; i < 20; i++)
            {
                lock (MyClass.block)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Primary");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Thread.Sleep(100);
                }
            }
        }
    }
}
