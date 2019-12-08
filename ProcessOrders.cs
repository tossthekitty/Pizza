using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

//  Pizza Exercise
//
//  One of our restaurant clients wants to know which
//      pizza topping combinations are the most popular.
//
//  Using a language in our technical stack, write an app or script that will
//      download orders directly from http://files.olo.com/pizzas.json 
//      and output the top 20 most frequently ordered pizza topping combinations. 
//
//          List the toppings for each popular pizza topping combination
//          along with its rank 
//          and the number of times that combination has been ordered.
//
//  For best results,
//      focus on accuracy and brevity.
//      Our estimate for this exercise is 30 minutes.
//
//  Submit your source file as a secret GitHub gist (https://gist.github.com). 
//      Then, add a GitHub comment with your output.

namespace PizzaTest
{
    /// <summary>
    /// This is our final conclusion class after processing.
    /// Very basic indeed.
    /// </summary>
    public class Popular
    {
        public string Toppings { get; set; }
        public int OrderedTotal { get; set; }
    }

    public class ProcessOrders
    {
        private List<Pizza> _pizzas;
        private readonly Uri _uri;

        /// <summary>
        /// Let's setup the json location as we create our process engine.
        /// </summary>
        /// <param name="uriLocation"></param>
        public ProcessOrders(string uriLocation)
        {
            _uri = new Uri(uriLocation);
        }

        /// <summary>
        /// Retrieve the result of the top maxOf most frequently ordered pizza topping combinations.
        /// </summary>
        /// <param name="maxOf"></param>
        /// <returns></returns>
        internal List<Popular> GetTopOrders(int maxOf)
        {
            using (WebClient wc = new WebClient())
            {
                string json = string.Empty;
                json = wc.DownloadString(_uri);
#if DEBUG
                if (!File.Exists(Environment.CurrentDirectory + "\\Pizzas.json"))
                    File.WriteAllText(Environment.CurrentDirectory + "\\Pizzas.json", json);
#endif
                _pizzas = MakePizzaList.FromJson(json);
                return _pizzas
                    .GroupBy(item => item.HashCode)
                    .Select(g => new Popular { Toppings = g.FirstOrDefault().ToString(), OrderedTotal = g.Count() })
                    .OrderByDescending(s => s.OrderedTotal)
                    .Take(maxOf)
                    .ToList();
            }
        }
    }

}
