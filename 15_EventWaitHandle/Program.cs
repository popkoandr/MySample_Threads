using System;
using System.Threading;

namespace _1_EventWaitHandle
{
    class Program
    {
        // AutoResetEvent - Уведомляет ожидающий поток о том, что произошло событие. 
        static readonly AutoResetEvent auto = new AutoResetEvent(false);

        static void Main()
        {
            Console.WriteLine("Нажмите на любую клавишу для перевода AutoResetEvent в сигнальное состояние.\n");
        
            var thread = new Thread(Function1);
            thread.Start();
            
            Console.ReadKey();
            auto.Set(); // Послать сигнал первому потоку

            Console.ReadKey();
            auto.Set(); // Послать сигнал второму потоку
 
            // Delay.
            Console.ReadKey();
        }

        static void Function1()
        {
            Console.WriteLine("Красный свет");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Console.WriteLine("Желтый");
            auto.WaitOne(); // после завершения WaitOne() AutoResetEvent автоматически переходит в несигнальное состояние.
            Console.WriteLine("Зеленый");
        }
    }
}
