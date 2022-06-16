using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using labar5.Classes;
using System.IO;

namespace labar5
{



    public partial class lab5 : System.Web.UI.Page
    {
        private const string resultsDir = "App_Data/Results/";
        private const string warehousesDir = "App_Data/WarehousesData/";
        private const string ordersDir = "App_Data/OrderData/";


        protected void Page_Load(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di1 = new DirectoryInfo(Server.MapPath(resultsDir));

            foreach (FileInfo file in di1.GetFiles())
            {
                file.Delete();
            }
            if (Session["Warehouses"]!= null)
            {
                InOut.PrintMultipleItemsTXT(Server.MapPath(resultsDir) + "Pradiniai.txt", (List<List<Item>>)Session["Warehouses"],
                    (List<string>)Session["IDS"]);
                AddMultipleTables(PlaceHolder1, (List<List<Item>>)Session["Warehouses"], (List<string>)Session["IDS"], "container", "SANDĖLIŲ PRADINIAI DUOMENYS");
            }
            if(Session["Orders"] != null)
            {
                InOut.PrintItemsTXT(Server.MapPath(resultsDir) + "Pradiniai.txt", (List<Item>)Session["Orders"], "Pradiniai užsakymai");
                AddTable(PlaceHolder2, (List<Item>)Session["Orders"], "Užsakymai", "container2", "UŽSAKYMŲ DUOMENYS");
            }
            if(Session["AddedPrices"]!= null)
            {
                InOut.PrintResults(Server.MapPath(resultsDir) + "Rezultatai.txt", (List<Item>)Session["AddedPrices"], "Pridėtos kainos:");
                AddResultsTable(PlaceHolder3, (List<Item>)Session["AddedPrices"], "Atrinktos vertybės:", "container3", "REZULTATAI", FindControl("remove"));

            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            List<string> WarehousesIDs = new List<string>();
            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(warehousesDir));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
         
            if (FileUpload1.HasFile)
            {
                string pathToDir = Server.MapPath(warehousesDir);
                string file = Path.GetExtension(FileUpload1.FileName);
                if (file == ".txt")
                {
                    Tasks.SaveFiles(FileUpload1, pathToDir);
                }
            }
            Session["Warehouses"] = InOut.ReadItems(Server.MapPath(warehousesDir), WarehousesIDs, excError);
            Session["IDS"] = WarehousesIDs;
            InOut.PrintMultipleItemsTXT(Server.MapPath(resultsDir) + "Pradiniai.txt", (List<List<Item>>)Session["Warehouses"],
                    (List<string>)Session["IDS"]);
            AddMultipleTables(PlaceHolder1, (List<List<Item>>)Session["Warehouses"], (List<string>)Session["IDS"], "container", "SANDĖLIŲ PRADINIAI DUOMENYS");
            
        }



        protected void Button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(ordersDir));

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            if (FileUpload2.HasFile)
            {
                string pathToDir = Server.MapPath(ordersDir);
                string file = Path.GetExtension(FileUpload2.FileName);
                if (file == ".txt")
                {
                    Tasks.SaveFiles(FileUpload2, pathToDir);
                }
            }
            Session["Orders"] = InOut.ReadOrders(Server.MapPath(ordersDir), excError);
            InOut.PrintItemsTXT(Server.MapPath(resultsDir) + "Pradiniai.txt", (List<Item>)Session["Orders"], "Pradiniai užsakymai");
            AddTable(PlaceHolder2, (List<Item>)Session["Orders"], "Užsakymai", "container2", "UŽSAKYMŲ DUOMENYS");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Session["AddedPrices"] = Tasks.AddPrices((List<List<Item>>)Session["Warehouses"], (List<Item>)Session["Orders"]);
            InOut.PrintResults(Server.MapPath(resultsDir) + "Rezultatai.txt", (List<Item>)Session["AddedPrices"], "Pridėtos kainos:");
            AddResultsTable(PlaceHolder3, (List<Item>)Session["AddedPrices"], "Atrinktos vertybės:", "container3", "REZULTATAI", FindControl("remove"));

        }

        protected void Button4_Click1(object sender, EventArgs e)
        {
            if(Decimal.TryParse(Text1.Value, out decimal value))
            {
                Error.Text = "";
                Tasks.RemoveByPrice((List<Item>)Session["AddedPrices"], value);
                InOut.PrintResults(Server.MapPath(resultsDir) + "Rezultatai.txt", (List<Item>)Session["AddedPrices"], "Pridėtos kainos:");
                AddResultsTable(PlaceHolder3, (List<Item>)Session["AddedPrices"], "Atrinktos vertybės:", "container3", "REZULTATAI", FindControl("remove"));

            }
            else
            {
                Error.Text = "Prašome įvesti skaičių.";
            }
        }
    }
}