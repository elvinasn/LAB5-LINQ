using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using labar5.Classes;
using System.Web.UI;

namespace labar5
{
    /// <summary>
    /// partial class to extend main class
    /// </summary>
    public partial class lab5 : System.Web.UI.Page
    {
        /// <summary>
        /// makes asp.net table with given items data
        /// </summary>
        /// <param name="items">items to form the table of</param>
        /// <param name="header">header of the table</param>
        /// <returns></returns>
        public static Table MakeTable(List<Item> items, string header)
        {
            Table table = new Table();
            table.GridLines = GridLines.Both;
            TableHeaderRow row = new TableHeaderRow();
            TableCell cell = new TableCell();
            cell.ColumnSpan = 3;
            cell.Text = Bold(header);
            row.Cells.Add(cell);
            table.Rows.Add(row);
            List<string> headers = new List<string> { Bold("Vertybė"),
                Bold("Kiekis"), Bold("Kaina") };
            table.Rows.Add(CreateRow(headers));
            items.ForEach(item =>
           table.Rows.Add(CreateRow(item.GetProperties())));
            return table;
        }

        /// <summary>
        /// adds single table to given placeholder with given warehouse data
        /// </summary>
        /// <param name="placeHolder">placeholder to add into</param>
        /// <param name="items"> warehouse' data</param>
        /// <param name="ID">id of warehouse</param>
        /// <param name="divID">div id of placeholder</param>
        /// <param name="headerText">header of the placeholder</param>
        public static void AddMultipleTables(PlaceHolder placeHolder,List<List<Item>> warehouses, List<string> IDS, string divID,
            string headerText)
        {
            placeHolder.Controls.Clear();
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl header = new HtmlGenericControl("h2");
            header.InnerText = headerText;
            div.ID = divID;
            int i = 0;
            placeHolder.Controls.Add(header);
            warehouses.ForEach(list => {
                div.Controls.Add(MakeTable(list, IDS[i])); i++;
            });
            placeHolder.Controls.Add(div);
        }
        /// <summary>
        /// adds single table to given placeholder with given warehouse data
        /// </summary>
        /// <param name="placeHolder">placeholder to add into</param>
        /// <param name="items"> warehouse' data</param>
        /// <param name="ID">id of warehouse</param>
        /// <param name="divID">div id of placeholder</param>
        /// <param name="headerText">header of the placeholder</param>
        public static void AddTable(PlaceHolder placeHolder, List<Item> items,
       string ID, string divID, string headerText)
        {
            placeHolder.Controls.Clear();
            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl header = new HtmlGenericControl("h2");
            header.InnerText = headerText;
            div.ID = divID;
            placeHolder.Controls.Add(header);
            div.Controls.Add(MakeTable(items, ID));
            placeHolder.Controls.Add(div);
        }

        /// <summary>
        /// adds results table to the given placeholder with items list data
        /// </summary>
        /// <param name="placeHolder">placeholder to add into</param>
        /// <param name="items">given list of items</param>
        /// <param name="ID">id of table</param>
        /// <param name="divID">div id of placeholder</param>
        /// <param name="headerText">headertext of the div</param>
        /// <param name="control">target to unhide</param>
        public static void AddResultsTable(PlaceHolder placeHolder, List<Item> items, string ID, string divID, string headerText, Control control)
        {
            placeHolder.Controls.Clear();
            HtmlControl target = (HtmlControl)control;
            target.Style.Add("display", "initial");
            target.Style.Add("visibility", "visible");

            HtmlGenericControl div = new HtmlGenericControl("div");
            HtmlGenericControl header = new HtmlGenericControl("h2");
            header.InnerText = headerText;
            div.ID = divID;
            placeHolder.Controls.Add(header);
            List<string> lines = Tasks.MakeLinesWithItemsCount(items);
            Table table = new Table();
            table.GridLines = GridLines.Both;
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            cell.ColumnSpan = 2;
            cell.Text = Bold(ID);
            row.Cells.Add(cell);
            table.Rows.Add(row);
            List<string> headers = new List<string> { Bold("Vertybė"), Bold("Kiekis") };
            table.Rows.Add(CreateRow(headers));
            lines.ForEach(line =>
            {
                List<string> values = line.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                values.ForEach(value => value = value.Trim());
                table.Rows.Add(CreateRow(values));
            });
            row = new TableRow();
            cell = new TableCell();
            cell.Text = $"Suma, kurią reikia sumokėti: {Tasks.ItemsPricesSum(items)}";
            cell.ColumnSpan = 2;
            row.Cells.Add(cell);
            table.Rows.Add(row);
            placeHolder.Controls.Add(table);

        }

        /// <summary>
        /// creates row by Ienumerable string values
        /// </summary>
        /// <param name="values">values of cells</param>
        /// <returns>row of the table</returns>
        protected static TableRow CreateRow(IEnumerable<string> values)
        {
            TableRow row = new TableRow();
            foreach (string value in values)
            {
                row.Cells.Add(CreateCell(value));
            }
            return row;

        }

        /// <summary>
        /// creates a table cell with given string value
        /// </summary>
        /// <param name="value">cell text</param>
        /// <returns>new table cell</returns>
        protected static TableCell CreateCell(string value)
        {
            TableCell cell = new TableCell();
            cell.Text = value;
            return cell;
        }

        /// <summary>
        /// bolds given string
        /// </summary>
        /// <param name="str">string to bold</param>
        /// <returns>bold text</returns>
        protected static string Bold(string str)
        {
            return string.Format("<b>{0}</b>", str);
        }
    }
}