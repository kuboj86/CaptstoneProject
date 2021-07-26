using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaptstoneProject
{
    class Updates
    {
        public static void InsertCompanyRecord(List<Stock> updatedPriceList)
        {
            int i = 0;
            using (StreamWriter stream = new StreamWriter("Data/Companys.txt",false))
            {
                foreach (var stock in updatedPriceList)
                {
                    i++;
                    if(i == updatedPriceList.Count)
                    {
                        string equity = $"{stock.CompanyName},{stock.TickerSymbol},{stock.NewStockPrice}";
                        stream.Write(equity);
                    }
                    else
                    {
                        string equity = $"{stock.CompanyName},{stock.TickerSymbol},{stock.NewStockPrice}\n";
                        stream.Write(equity);
                    }

                }
                stream.Close();
            }
        }
        public static void InsertPortfolioRecord(Portfolio port)
        {
            string equity = $"{port.Id},{port.CompanyName},{port.TickerSymbol},{port.PurchasedStockPrice},{port.NewStockPrice}";
            File.AppendAllText("Data/Portfolio.txt",$"{equity}\n");
            //using (StreamWriter stream = File.AppendText("Data/Portfolio.txt"))
            //{
            //    stream.WriteLine("71, hello, hi, 174, 999.0");
            //    stream.Close();
            //}
        }

        internal static void InsertBalanceRecord(string newBalance)
        {
            File.WriteAllText("Data/Users.txt",newBalance);
        }
        public static void RemoveRecords()
        {
            File.WriteAllText("Data/Companys.txt", String.Empty);
        }
    }
}
