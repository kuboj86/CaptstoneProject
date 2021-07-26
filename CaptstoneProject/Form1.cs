using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaptstoneProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            List<Stock> companyList = Company.GetCompanysList();
            List<Portfolio> portfolioList = Portfolio.GetPortfolio(companyList);

            SetStockGridView(companyList);
            SetPortfolioGridView(portfolioList);
            Cursor.Current = Cursors.Default;
        }
        private void SetStockGridView(List<Stock> companyList)
        {


            dataGridView1.Visible = true;
            dataGridView2.Visible = true;

            textBox3.Visible = true;
            textBox4.Visible = true;

            buyingPower_label.Visible = true;
            buyingPowerValue.Visible = true;
            buyingPowerValue.Text = Calculations.GetBuyingPower();


            BuyButton.Visible = true;
            sellButton.Visible = true;

            dataGridView1.DataSource = null;
            dataGridView1.Update();
            dataGridView1.Refresh();

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = companyList;
            this.dataGridView1.Columns["Id"].Visible = false;
            this.dataGridView1.Columns["StockPrice"].DefaultCellStyle.Format = "c";
            this.dataGridView1.Columns["CompanyName"].Width = 200;
            this.dataGridView1.Columns["movementValue"].Visible = false;
            this.dataGridView1.Columns["PercentChange"].DefaultCellStyle.Format = " #.00\\%";
            this.dataGridView1.Columns["NewStockPrice"].DefaultCellStyle.Format = "c";
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToInt32(row.Cells[4].Value) < 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
                else if (Convert.ToInt32(row.Cells[4].Value) > 0)
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }
            }
            dataGridView1.ClearSelection();
        }
        private void SetPortfolioGridView(List<Portfolio> portfolioList)
        {
            dataGridView2.DataSource = null;
            dataGridView2.Update();
            dataGridView2.Refresh();

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToResizeRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.DataSource = portfolioList;
            this.dataGridView2.Columns["Id"].Visible = false;
            this.dataGridView2.Columns["PercentChange"].Visible = false;
            this.dataGridView2.Columns["PurchasedStockPrice"].DefaultCellStyle.Format = "c";
            this.dataGridView2.Columns["ProfitLoss"].DefaultCellStyle.Format = "c";
            this.dataGridView2.Columns["NewStockPrice"].DefaultCellStyle.Format = "c";
            this.dataGridView2.Columns["CompanyName"].Width = 200;
            this.dataGridView2.Columns["movementValue"].Visible = false;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {

                if (Convert.ToInt32(row.Cells[3].Value) < Convert.ToInt32(row.Cells[5].Value))
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                }
                else if (Convert.ToInt32(row.Cells[3].Value) > Convert.ToInt32(row.Cells[5].Value))
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                }
            }
            dataGridView2.ClearSelection();
        }
        public void BuyButton_Click(object sender, EventArgs e)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            List<Stock> companyList = Company.GetCompanysList();
            if (dataGridView1.SelectedRows.Count != 0)
            {
                Portfolio port = new Portfolio();
                DataGridViewRow row = this.dataGridView1.SelectedRows[0];
                port.Id = (int)row.Cells["Id"].Value;
                port.CompanyName = (string)row.Cells["CompanyName"].Value;
                port.TickerSymbol = (string)row.Cells["TickerSymbol"].Value;
                port.NewStockPrice = (double)row.Cells["NewStockPrice"].Value;
                port.PurchasedStockPrice = (double)row.Cells["NewStockPrice"].Value;

                List<Portfolio> portfolioList = Portfolio.GetPortfolio(companyList);
                portfolioList.Add(port);
                string newBalance = Calculations.CalculateSubtractFromBalance(port.PurchasedStockPrice);

                Updates.InsertBalanceRecord(newBalance);
                Updates.InsertPortfolioList(portfolioList);


                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.AllowUserToResizeRows = false;
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = portfolioList;
                buyingPowerValue.Text = Calculations.GetBuyingPower();

                SetPortfolioGridView(portfolioList);
            }
            else
            {

            }
            SetStockGridView(companyList);

            List<Portfolio> updatedPortfolioList = Portfolio.GetPortfolio(companyList);
            SetPortfolioGridView(updatedPortfolioList);

            Cursor.Current = Cursors.Default;
        }
        private void sellButton_Click(object sender, EventArgs e)
        {
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            List<Stock> companyList = Company.GetCompanysList();
            if (dataGridView2.SelectedRows.Count != 0)
            {
                List<Portfolio> portfolioList = Portfolio.GetPortfolio(companyList);

                DataGridViewRow row = this.dataGridView2.SelectedRows[0];

                int stockID = (int)row.Cells["Id"].Value;

                var removeStock = portfolioList.Single(x => x.Id == stockID);
                if(removeStock != null)
                {
                    double currentPrice = (double)row.Cells["NewStockPrice"].Value;
                    string newBalance = Calculations.CalculateAddToBalance(currentPrice);
                    Updates.InsertBalanceRecord(newBalance);
                    portfolioList.Remove(removeStock);
                }
                dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView2.ReadOnly = true;
                dataGridView2.DataSource = portfolioList;
                buyingPowerValue.Text = Calculations.GetBuyingPower();

                Updates.InsertPortfolioList(portfolioList);
                SetPortfolioGridView(portfolioList);
            }
            SetStockGridView(companyList);
            Cursor.Current = Cursors.Default;
        }
    }
}
