using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaptstoneProject
{
    class Calculations
    {
        public static List<Stock> GetAdjustedStockPrice(List<Stock> companies)
        {
            List<Stock> newList = new List<Stock>();
            double decValue = 0;
            foreach (var item in companies)
            {
                try
                {
                    decValue = Math.Round(GetRandomPercent(), 2);
                    GetNewStockPrice(item, decValue);
                    
                    Thread.Sleep(100);
                    newList.Add(item);

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            Updates.InsertCompanyRecord(newList);
            return newList;
        }

        public static double GetRandomPercent()
        {
            double percent;
            Random r = new Random();
            percent = r.NextDouble() * 1.5;

            return percent;
        }
        public static Stock GetNewStockPrice(Stock companyRecord, double decValue)
        {
            float upOrDOwn = GetPriceMovement();
            switch (upOrDOwn)
            {
                case -1:
                    companyRecord.NewStockPrice = CalculateNegative(companyRecord.StockPrice, decValue);
                    companyRecord.PercentChange = decValue;
                    companyRecord.movementValue = upOrDOwn;
                    break;
                case 0:
                    companyRecord.NewStockPrice = companyRecord.StockPrice;
                    companyRecord.movementValue = upOrDOwn;

                    break;
                case 1:
                    companyRecord.NewStockPrice = CalculatePositive(companyRecord.StockPrice, decValue);
                    companyRecord.PercentChange = decValue;
                    companyRecord.movementValue = upOrDOwn;

                    break;
            }
            return companyRecord;
        }

        internal static double GetProfitLoss(double purchasedStockPrice, double newStockPrice)
        {

            double total;
            return total = Math.Round(purchasedStockPrice - newStockPrice, 2);
        }

        internal static string GetBuyingPower()
        {
            string text = File.ReadAllText($"Data/Users.txt");

            return text;
        }

        public static float GetPriceMovement()
        {
            Random r = new Random();
            float upDownNeutral = r.Next(-1, 2);

            return upDownNeutral;
        }

        public static double CalculatePositive(double stockPrice, double percentChange)
        {
            double newstockPrice;
            double value = Math.Round((stockPrice * percentChange) / 100, 2);

            return newstockPrice = Math.Round(stockPrice + value , 2);
        }

        public static double CalculateNegative(double stockPrice, double percentChange)
        {
            double newstockPrice;
            double value = Math.Round( (stockPrice * percentChange) / 100,2);

            return newstockPrice = Math.Round(stockPrice - value , 2);
        }

        internal static string CalculateSubtractFromBalance(double purchasedStockPrice)
        {
            string stringBalance = GetBuyingPower();
            double balance = Convert.ToDouble(stringBalance);

            string newBalance = Math.Round((balance - purchasedStockPrice), 2).ToString();
            return newBalance;

        }
        internal static string CalculateAddToBalance(double purchasedStockPrice)
        {
            string stringBalance = GetBuyingPower();
            double balance = Convert.ToDouble(stringBalance);

            string newBalance = Math.Round((balance + purchasedStockPrice), 2).ToString();
            return newBalance;

        }
    }
}
