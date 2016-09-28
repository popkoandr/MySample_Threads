using System;
using System.Threading;

namespace WaitHandle
{
    class Program
    {
        static readonly System.Threading.WaitHandle[] Events = { new AutoResetEvent(false), new AutoResetEvent(false) };

        static Random random = new Random();

        static void Main()
        {
            DateTime dateTime = DateTime.Now;
            Console.WriteLine("Главный поток ожидает завершения ОБЕИХ задач.\n");

            // Очередь для двух задач в двух разных потоках. 
            ThreadPool.QueueUserWorkItem(Task1, Events[0]);
            ThreadPool.QueueUserWorkItem(Task2, Events[1]);

            // Ожидание пока все задачи завершаться.
            System.Threading.WaitHandle.WaitAll(Events);

            // Время отображаемое ниже, должно совпадать с продолжительностью выполнения самой длинной задачи.
            Console.WriteLine("Обе задачи завершены (время ожидания = {0})", (DateTime.Now - dateTime).TotalMilliseconds);

            dateTime = DateTime.Now;

            Console.WriteLine("\nОжидание завершения одной из задач.");
            ThreadPool.QueueUserWorkItem(Task1, Events[0]);
            ThreadPool.QueueUserWorkItem(Task2, Events[1]);

            // Ожидание пока одна из задач не завершится.
            int index = System.Threading.WaitHandle.WaitAny(Events);

            // Время отображаемое ниже, должно совпадать с продолжительностью выполнения самой короткой задачи.
            Console.WriteLine("Задача {0} завершилась первой (время ожидания = {1}).", index + 1, (DateTime.Now - dateTime).TotalMilliseconds);

            // Задержка.
            Console.ReadKey();
        }

        static void Task1(Object state)
        {
            var auto = (AutoResetEvent)state;
            int time = 1000 * random.Next(2, 10);
            Thread.Sleep(time);
            Console.WriteLine("Задача 1 выполнена за {0} миллисенунд.", time);
            auto.Set();
        }

        static void Task2(Object state)
        {
            var auto = (AutoResetEvent)state;
            int time = 1000 * random.Next(2, 10);
            Thread.Sleep(time);
            Console.WriteLine("Задача 2 выполнена за {0} миллисенунд.", time);
            auto.Set();
        }
    }
}
