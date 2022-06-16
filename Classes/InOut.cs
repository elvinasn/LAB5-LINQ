using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;

namespace labar5.Classes
{
    /// <summary>
    /// class to store in out utils
    /// </summary>
    public static class InOut
    {
        /// <summary>
        /// reads items from given directory txt files
        /// </summary>
        /// <param name="dir">directory to search for txt files</param>
        /// <param name="IDs">Ids of warehouses</param>
        /// <param name="error">error for exception text</param>
        /// <returns></returns>
        public static List<List<Item>> ReadItems(string dir, List<string> IDs, Label error)
        {

            List<List<Item>> itemsList = new List<List<Item>>();
            foreach (string txtName in Directory.GetFiles(dir, "*.txt"))
            {
                try
                {
                    List<Item> items = new List<Item>();
                    List<string> lines = File.ReadAllLines(txtName, Encoding.UTF8).ToList();
                    IDs.Add(lines[0]);
                    for (int i = 1; i < lines.Count(); i++)
                    {
                        string[] values = lines[i].Split(';');
                        Item item = new Item(values[0], Convert.ToInt32(values[1]), Convert.ToDecimal(values[2]));
                        items.Add(item);
                    }
                    itemsList.Add(items);
                }
                catch
                {
                    List<string> values = txtName.Split('\\').ToList();
                    error.Text = $"Neteisingi duomenys faile {values.Last()}";

                }

            }
            return itemsList;
        }

        /// <summary>
        /// Reads orders from directory single txt file
        /// </summary>
        /// <param name="dir">directory to read from</param>
        /// <param name="error">error for exception text</param>
        /// <returns></returns>
        public static List<Item> ReadOrders(string dir, Label error)
        {
            List<Item> orders = new List<Item>();
            var files = Directory.GetFiles(dir, "*.txt");

            try
            {
                List<string> lines = File.ReadAllLines(files[0], Encoding.UTF8).ToList();
                foreach (string line in lines)
                {
                    string[] values = line.Split(';');
                    Item item = new Item(values[0], Convert.ToInt32(values[1]));
                    orders.Add(item);
                }
            }
            catch
            {
                List<string> values = files[0].Split('\\').ToList();
                error.Text = $"Neteisingi duomenys faile {values.Last()}";
            }

            return orders;
        }

        /// <summary>
        /// prints given list of items to txt file
        /// </summary>
        /// <param name="fileName">txt file to print in</param>
        /// <param name="items">items data to print</param>
        /// <param name="ID">ID of warehouse</param>
        public static void PrintItemsTXT(string fileName, List<Item> items, string ID)
        {
            List<string> lines = new List<string>();
            string dashes = new string('-', new Item("", 0).ToString().Length);
            lines.Add(ID);
            lines.Add(dashes);
            lines.Add($"| {"Vertybė",-15} | {"Kiekis",7} | {"Kaina",7} |");
            lines.Add(dashes);
            items.ForEach((item) => lines.Add(item.ToString()));
            lines.Add(dashes);
            lines.Add("");
            File.AppendAllLines(fileName, lines, Encoding.UTF8);
        }


        /// <summary>
        /// prints list of warehouses to print to txt files
        /// </summary>
        /// <param name="fileName">txt file</param>
        /// <param name="items">List of warehouses</param>
        /// <param name="ID">List of warehouses' ids</param>
        public static void PrintMultipleItemsTXT(string fileName, List<List<Item>> items, List<string> ID)
        {
            int i = 0;
            items.ForEach(list => { PrintItemsTXT(fileName, list, ID[i]); i++; });
        }

        /// <summary>
        /// prints results to txt file
        /// </summary>
        /// <param name="path">path to print in</param>
        /// <param name="items">items to print</param>
        /// <param name="header">header of the table</param>
        public static void PrintResults(string path, List<Item> items, string header)
        {
            List<string> lines = new List<string>();
            string tableHeading = $"| {"Vertybė",-15} | {"Kiekis",7} |";
            string dashes = new string('-', tableHeading.Length);

            lines.Add(header);
            lines.Add(dashes);
            lines.Add(tableHeading);
            lines.Add(dashes);
            lines.AddRange(Tasks.MakeLinesWithItemsCount(items));
            lines.Add(dashes);
            lines.Add($"Suma, kurią reikia sumokėti: {Tasks.ItemsPricesSum(items)}");
            lines.Add("");
            File.WriteAllLines(path, lines, Encoding.UTF8);

        }

  
    }
}