using AppCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program();
        }

        Program()
        {
            ComponentsFactory.Init();
            string test = ComponentsFactory.Get<string>();
            Console.WriteLine(test);
            Console.ReadKey();
        }
    }
}
