using System;
using System.Threading;
//1
// Ииспользование Mutex для синхронизации доступа к защищенным ресурсам.

// Mutex - Примитив синхронизации, который также может использоваться в межпроцессной и междоменной синхронизации.
// MutEx - Mutual Exclusion (Взаимное Исключение).

namespace MutexSample
{
    class Program
    {
        // Mutex - Примитив синхронизации, который также может использоваться в межпроцессорной синхронизации.
        // функционирует аналогично AutoResetEvent но снабжен дополнительной логикой:
        // 1. Запоминает какой поток им владеет. ReleaseMutex не может вызвать поток, который не владеет мьютексом.
        // 2. Управляет рекурсивным счетчиком, указывающим, сколько раз поток-владелец уже владел объектом.
        private static readonly Mutex Mutex1 = new Mutex(false, "MutexSample:AAED7056-380D-412E-9608-763495211EA8");////////////
        

        static void Main()
        {
            var threads = new Thread[5];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(new ThreadStart(Function))
                    {
                        Name = i.ToString()
                    };
                threads[i].Start();
            }

            // Delay.
            Console.ReadKey();
        }

        static void Function()
        {
           bool myMutex = Mutex1.WaitOne();////////////

            Console.WriteLine("Поток {0} зашел в защищенную область.", Thread.CurrentThread.Name);
            Thread.Sleep(2000);
            Console.WriteLine("Поток {0}  покинул защищенную область.\n", Thread.CurrentThread.Name);
            Mutex1.ReleaseMutex();/////////////
        }
    }
}
