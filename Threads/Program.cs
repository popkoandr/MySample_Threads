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
        // method that we are going to execute on another thread / (simple)
        private static void WriteSecond()
        {
            while (true)
            {
                Console.WriteLine(new string(' ', 10) + "Secondary");
            }
        }

        static void Main(string[] args)
        {
            ThreadStart writeSecondDelegate = new ThreadStart(WriteSecond);
            Thread thread = new Thread(writeSecondDelegate);
            thread.Start();

            while (true)
            {
                Console.WriteLine("Primary");
            }
        }
    }
}
