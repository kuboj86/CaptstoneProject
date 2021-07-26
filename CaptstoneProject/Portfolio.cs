using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptstoneProject
{
    class Portfolio
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string TickerSymbol { get; set; }
        public double PurchasedStockPrice { get; set; }
        public int movementValue { get; set; }
        public double NewStockPrice { get; set; }
        public double ProfitLoss { get; set; }
        public double PercentChange { get; set; }

        public static List<Portfolio> GetPortfolio(List<Stock> companyList)
        {
            string file = $"Data/Portfolio.txt";
            string[] fileContents = File.ReadAllLines(file);
            List<Portfolio> myPortfolio = new List<Portfolio>();
            List<Company> companies1 = new List<Company>();
            string[] fileArray;
            int id = 1;
            foreach (var item in fileContents)
            {
                Portfolio portfolio = new Portfolio();
                fileArray = item.Split(',');
                portfolio.Id = Convert.ToInt32(fileArray[0]); //ID
                portfolio.CompanyName = fileArray[1]; //Company
                portfolio.TickerSymbol = fileArray[2]; //Ticker
                portfolio.PurchasedStockPrice = Convert.ToDouble(fileArray[3]);//price of stock at purchase


                Stock comp = companyList.Find(x => x.Id == portfolio.Id);
                //portfolio.NewStockPrice = Convert.ToDouble(fileArray[4]);//current stock price
                portfolio.NewStockPrice = Convert.ToDouble(comp.NewStockPrice);
                portfolio.ProfitLoss = Calculations.GetProfitLoss(portfolio.PurchasedStockPrice, portfolio.NewStockPrice);
                myPortfolio.Add(portfolio);
                id++;
            }

            return myPortfolio;
        }
        //public static List<Portfolio> GetPortfolio(List<Stock> companyList)
        //{
        //    string file = $"Data/Portfolio.txt";
        //    string[] fileContents = File.ReadAllLines(file);
        //    List<Portfolio> myPortfolio = new List<Portfolio>();
        //    List<Company> companies1 = new List<Company>();
        //    string[] fileArray;
        //    int id = 1;
        //    foreach (var item in fileContents)
        //    {
        //        Portfolio portfolio = new Portfolio();
        //        fileArray = item.Split(',');
        //        portfolio.Id = Convert.ToInt32(fileArray[0]); //ID
        //        portfolio.CompanyName = fileArray[1]; //Company
        //        portfolio.TickerSymbol = fileArray[2]; //Ticker
        //        portfolio.PurchasedStockPrice = Convert.ToDouble(fileArray[3]);//price of stock at purchase

        //        Stock comp = companyList.Find(x => x.Id == portfolio.Id);
        //        //portfolio.NewStockPrice = Convert.ToDouble(fileArray[4]);//current stock price
        //        portfolio.NewStockPrice = Convert.ToDouble(comp.NewStockPrice);

        //        myPortfolio.Add(portfolio);
        //        id++;
        //    }

        //    return myPortfolio;
        //}
    }
}
