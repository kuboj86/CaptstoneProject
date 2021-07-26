using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaptstoneProject
{
    class Stock
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string TickerSymbol { get; set; }
        public double StockPrice { get; set; }
        public float movementValue { get; set; }
        public double PercentChange { get; set; }
        public double NewStockPrice { get; set; }

    }
}
