using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptstoneProject
{
    class Company
    {
        public static List<Stock> GetCompanysList()
        {
            string file = $"Data/Companys.txt";
            string[] fileContents = File.ReadAllLines(file);
            List<Stock> companies = new List<Stock>();
            string[] fileArray;
            int id = 1;
            foreach (var item in fileContents)
            {
                if(item !="")
                {
                    Stock company = new Stock();
                    fileArray = item.Split(',');
                    company.Id = id;
                    company.CompanyName = fileArray[0];
                    company.TickerSymbol = fileArray[1];
                    company.StockPrice = Convert.ToDouble(fileArray[2]);


                    companies.Add(company);
                    id++;
                }
            }
            Calculations.GetAdjustedStockPrice(companies);

            return companies;
            
        }


    }

}
