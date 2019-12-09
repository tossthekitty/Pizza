using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaTest
{
    class Program
    {
        // Location of the json file
        private const string Web  = "http://files.olo.com/pizzas.json";

        // for Speed purposes and analysis - Pizzas.json is included in this build and copied to the
        // Bin/Debug folder.  If you are in DEBUG note (see directives below), ProcessOrders should
        // use the copied json file.
        static readonly string FileName = Environment.CurrentDirectory + "\\Pizzas.json";
        private static readonly int MaximumResults = 20;

        static void Main(string[] args)
        {
            // Read from Web: "http://files.olo.com/pizzas.json"
            // Read from File: Environment.CurrentDirectory + "\\Pizzas.json"
#if DEBUG
            // SO MUCH FASTER for debugging cycles.   
            ProcessOrders pizzaOrders = new ProcessOrders( FileName );
#else
            ProcessOrders pizzaOrders = new ProcessOrders( Web );
#endif
            var top20 = pizzaOrders.GetTopOrders(MaximumResults);
            Console.WriteLine("Rank\t#Ordered\tToppings ------------ {0} Orders Max",MaximumResults);
            Console.WriteLine("---------------------------------------");
            string str = "{0}\t{1}\t\t{2}";
            for(int rank = 0 ; rank < top20.Count ; rank++)
            {
                Console.WriteLine(String.Format(str, rank + 1, top20[rank].OrderedTotal, top20[rank].Toppings));
            }
            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
