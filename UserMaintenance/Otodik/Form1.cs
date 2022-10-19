using Otodik.Entities;
using Otodik.MnbServiceReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace Otodik
{
    public partial class Form1 : Form
    {
        BindingList<RateDate> Rates = new BindingList<RateDate>();
        
        public Form1()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();
            Elso();
            dataGridView1.DataSource = Rates.ToList();
            
        }
        public void Add()
        {
            List<Currency> currencies = new List<Currency>();
            currencies.Add(new Currency() { CurrencyA = "EUR" });
            currencies.Add(new Currency() { CurrencyA = "HUF" });
            currencies.Add(new Currency() { CurrencyA = "USD" });

            comboBox1.DataSource = currencies;
            comboBox1.DisplayMember = "CurrencyA";
        }
        public void Elso()
        {
            Add();
            var mnbService = new MNBArfolyamServiceSoapClient();

            var request = new GetExchangeRatesRequestBody()
            {
                currencyNames = "EUR",
                startDate = dateTimePicker1.Value.ToString(),
                endDate = dateTimePicker2.Value.ToString()
            };

            
            var response = mnbService.GetExchangeRates(request);

           
            var result = response.GetExchangeRatesResult;


            var xml = new XmlDocument();
            xml.LoadXml(result);


            foreach (XmlElement element in xml.DocumentElement)
            {

                var rate = new RateDate();
                Rates.Add(rate);

                //Date
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                //Currency
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");

                //Value
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;

                chartRateDate.DataSource = Rates;

                var series = chartRateDate.Series[0];
                series.ChartType = SeriesChartType.Line;
                series.XValueMember = "Date";
                series.YValueMembers = "Value";
                series.BorderWidth = 2;

                var legend = chartRateDate.Legends[0];
                legend.Enabled = false;

                var chartArea = chartRateDate.ChartAreas[0];
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisY.MajorGrid.Enabled = false;
                chartArea.AxisY.IsStartedFromZero = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
