using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using labar5;

namespace labar5.Classes
{
    /// <summary>
    /// class to store task utils
    /// </summary>
    public static class Tasks
    {
        /// <summary>
        /// saves files to given path from fileupload object
        /// </summary>
        /// <param name="FileUpload1">given fileupload</param>
        /// <param name="path">path to dir</param>
        public static void SaveFiles(FileUpload FileUpload1, string path)
        {
            foreach (HttpPostedFile postedFile in FileUpload1.PostedFiles)
            {
                string fileName = Path.GetFileName(postedFile.FileName);
                postedFile.SaveAs(path + fileName);
            }
        }

        /// <summary>
        /// adds prices to given orders based on warehouses data by cheapest item available
        /// </summary>
        /// <param name="Warehouses">warehouses' data</param>
        /// <param name="Orders">orders' data</param>
        /// <returns></returns>
        public static List<Item> AddPrices(List<List<Item>> Warehouses, List<Item> Orders)
        {
            List<Item> AddedPrices = new List<Item>();
            List<Item> AllItems = SpreadList(Warehouses);
            Orders.ForEach(Order =>
            {
                for (int i = 0; i < Order.Count; i++)
                {
                    Item item = new Item(Order.Name, 0);
                    List<Item> SameItems = 
                                AllItems.Where(current => current.Name == item.Name && current.Count > 0)
                                .OrderBy(current => current.Price)
                                .ToList();
                    if(SameItems.Count > 0)
                    {
                        Item lowestPrice = SameItems.First();
                        lowestPrice.Count--;
                        item.Count = 1;
                        item.Price = lowestPrice.Price;
                        AddedPrices.Add(item);
                    }
                  
                }
            });
            return Group(AddedPrices);
        }

        /// <summary>
        /// spreads given list of lists to single list
        /// </summary>
        /// <param name="Warehouses">list of lists</param>
        /// <returns>single list</returns>
        private static List<Item> SpreadList(List<List<Item>> Warehouses)
        {
            List<Item> spreaded = new List<Item>();
            Warehouses.ForEach(wh => spreaded = spreaded.Concat(wh).ToList());
            return spreaded;
        }

        /// <summary>
        /// groups items by same name and price
        /// </summary>
        /// <param name="items">given list of inems</param>
        /// <returns>returns grouped list by name and price</returns>
        private static List<Item> Group(List<Item> items)
        {
            return items.GroupBy(item => new { item.Name, item.Price })
                .Select(Group => new Item(Group.Key.Name, Group.Count(), Group.Key.Price)).ToList();
        }

        /// <summary>
        /// groups given items by name and makes list of strings based on their data
        /// </summary>
        /// <param name="items">list of items</param>
        /// <returns>list of string which contains formatted groupped items data</returns>
        public static List<string> MakeLinesWithItemsCount(List<Item> items)
        {
            List<string> lines = new List<string>();
            foreach (var line in items.GroupBy(item => item.Name)
                         .Select(group => new {
                             Name = group.Key,
                             Count = group.Aggregate(0, (sum, item) => sum += item.Count)
                         })
                         .OrderBy(x => x.Name).ThenBy(x => x.Count))
            {
                lines.Add($"| {line.Name,-15} | {line.Count,7} |");
            }
            return lines;
        }

        /// <summary>
        /// gets given items prices sum
        /// </summary>
        /// <param name="items">list of items</param>
        /// <returns>return the total price of list of items</returns>
        public static decimal ItemsPricesSum(List<Item> items)
        {
            return items.Aggregate(0m, (sum, item) => sum += item.Count * item.Price);
        }

        /// <summary>
        /// removes items from the list, until items' list price is less than given price, deletes the cheapest ones first
        /// </summary>
        /// <param name="items">list of items</param>
        /// <param name="price">maximum price</param>
        public static void RemoveByPrice(List<Item> items, decimal price)
        {
            while(ItemsPricesSum(items) > price && items.Count > 0)
            {
                Item needed = items.OrderBy(item => item.Price).First();
                needed.Count--;
                items.RemoveAll(item => item.Count < 1);
            }
        }
    }
}