using System.Collections.Generic;
using Newtonsoft.Json;

namespace PizzaTest
{
    /// <summary>
    /// Lets create a list of pizzas from our json input.
    /// </summary>
    public static class MakePizzaList
    {
        /// <summary>
        /// Deserialize the string to a list C# wrapper classes.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static List<Pizza> FromJson(string json)
        {
            return JsonConvert.DeserializeObject<List<Pizza>>(json);
        }
    }

    public class Pizza
    {
        [JsonIgnore]
        private List<string> _toppings;

        [JsonIgnore]
        private int _hashCode;

        /// <summary>
        /// The unique identifier for this set of toppings regardless of order.
        /// </summary>
        [JsonIgnore]
        public int HashCode
        {
            get => _hashCode;
            private set => _hashCode = value;
        }

        /// <summary>
        /// What's on this pizza - list of toppings
        /// </summary>
        [JsonProperty("toppings")]
        private List<string> Toppings
        {
            get => _toppings;
            set
            {
                _toppings = value;
                HashCode = GetToppingsHash();
            }
        }

        /// <summary>
        /// Convert our topping list into a comma delimited string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            _toppings.Sort();
            return string.Join(", ", _toppings);
        }

        /// <summary>
        /// Calculate the unique identifier - math is faster than sorting.
        /// </summary>
        /// <returns></returns>
        private int GetToppingsHash()
        {
            int hash = 0;
            for(int i = 0; i < _toppings.Count; i++) 
            {
                hash = (hash ^ EqualityComparer<string>.Default.GetHashCode(_toppings[i]) + _toppings.Count);
            }
            return hash;
        }
    }
}
