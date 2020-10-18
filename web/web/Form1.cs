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
using web.Entities;
using web.MnbServiceReference;

namespace web
{
    public partial class Form1 : Form
    {
         BindingList<RateData> Rates;
        BindingList<String> Currencies;
        
        private void Currency()
        {
            Rates = new BindingList<RateData>();
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
                var rate = new RateData();
               
                rate.Date = DateTime.Parse(element.GetAttribute("date"));
                var childElement = (XmlElement)element.ChildNodes[0];
                rate.Currency = childElement.GetAttribute("curr");
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;
                Rates.Add(rate);

            }
        }
        private void chart()
        {
            chart1.DataSource = Rates;
            var series = chart1.Series[0];
            series.ChartType = SeriesChartType.Line;
            series.XValueMember = "Date";
            series.YValueMembers = "Value";
            series.BorderWidth = 2;
            var legend = chart1.Legends[0];
            legend.Enabled = false;

            var chartArea = chart1.ChartAreas[0];
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.Enabled = false;
            chartArea.AxisY.IsStartedFromZero = false;
        }
        private void Refreshdata()
        {
            // Rates.Clear();
             Currency();
             chart();
            dataGridView1.DataSource = Rates.ToList();
        }
        public Form1()
        {
            InitializeComponent();

            comboBox1.DataSource = Currencies;
            Refreshdata();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            Refreshdata();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            Refreshdata();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Refreshdata();
        }
    }
}
