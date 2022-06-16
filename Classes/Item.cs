using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace labar5.Classes
{
    /// <summary>
    /// class to store data about an item
    /// </summary>
    public class Item
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public decimal Price { get; set; }

        /// <summary>
        /// ctor with multiple parameters
        /// </summary>
        /// <param name="name">given name</param>
        /// <param name="count">given count</param>
        /// <param name="price">given price, default value is 0</param>
        public Item(string name, int count, decimal price = 0)
        {
            Name = name;
            Count = count;
            Price = price;
        }

        /// <summary>
        /// overriden tostring method to return formatted data
        /// </summary>
        /// <returns>formated string of object' data</returns>
        public override string ToString()
        {
            return $"| {Name, -15} | {Count, 7} | {Price, 7} |";
        }

        /// <summary>
        /// gets properties in list of strings
        /// </summary>
        /// <returns>list of properties in strings</returns>
        public virtual List<string> GetProperties()
        {
            return new List<string> { Name, Count.ToString(), Price.ToString() };
        }
    }
}