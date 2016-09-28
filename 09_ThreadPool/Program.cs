using System;
using System.Threading;

// Пул потоков.

namespace ThreadPool
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Начало работы программы");
            ShowThreadInfo();
            Console.WriteLine("Запускаем Task1 в потоке из пула потоков");
            System.Threading.ThreadPool.QueueUserWorkItem(Task1);/////////////////////
            ShowThreadInfo();

            Console.WriteLine("Запускаем Task2 в потоке из пула потоков");
            Thread.Sleep(1000);
            System.Threading.ThreadPool.QueueUserWorkItem(Task2);///////////////////////
            ShowThreadInfo();

            Console.WriteLine("Главный поток.");
            Thread.Sleep(1000);

            Console.WriteLine("Главный поток завершен.\n");

            
            Console.WriteLine("Task1 и Task2 закончили работу");
            ShowThreadInfo();
            Console.ReadKey();
        }

        static void Task1(object state)
        {
            Thread.CurrentThread.Name = "1";
            Console.WriteLine("Поток {0}:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine();
            Thread.Sleep(200);
        }

        static void Task2(object state)
        {
            Thread.CurrentThread.Name = "2";
            Console.WriteLine("Поток {0}:{1}", Thread.CurrentThread.Name, Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(200);
        }

        //Когда метод GetAvailableThreads возвращает значение, переменная, указанная параметром workerThreads, 
        //содержит число дополнительных рабочих потоков, которые могут быть запущены, а переменная, указанная параметром 
        //completionPortThreads, содержит число дополнительных потоков асинхронного ввода/вывода, которые могут быть запущены.

        //Если доступные потоки отсутствуют, дополнительные запросы к пулу потоков будут оставаться в очереди до тех пор, 
        //пока в пуле потоков не появятся доступные потоки.
        static void ShowThreadInfo()
        {
            int availableWorkThreads, availableIOThreads, maxWorkThreads, maxIOThreads;
            System.Threading.ThreadPool.GetAvailableThreads(out availableWorkThreads, out availableIOThreads);
            System.Threading.ThreadPool.GetMaxThreads(out maxWorkThreads, out maxIOThreads);
            Console.WriteLine("-------------Доступно рабочих потоков в пуле:{0} из {1}", availableWorkThreads, maxWorkThreads);
            Console.WriteLine("-------------Доступно потоков ввода-вывода в пуле:{0} из {1}\n", availableIOThreads, maxIOThreads);
        }
    }
}
