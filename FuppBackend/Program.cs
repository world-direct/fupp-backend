using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FuppBackend {
    using System.Threading;

    class Program {
        static void Main(string[] args) {
            for (;;) {
                Console.WriteLine("Hello World!");
                Thread.Sleep(1000);
            }
        }
    }
}
