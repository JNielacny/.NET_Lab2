using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using lab2;

namespace GUI
{
    public partial class Form1 : Form
    {
        // Zmienne do przechowywania danych o walucie i dacie
        private string currencyName;
        private string selectedDate;
        private bool opcja1;
        private bool opcja2;

        public Form1()
        {
            InitializeComponent();
            // Pokazywanie przycisk�w
            monthCalendar1.Visible = true; // Zak�adam, �e "przycisk1" to jeden z przycisk�w do ukrycia

            // Ukrywanie przycisk�w
            comboBox2.Visible = false; // Zak�adam, �e "przycisk3" to jeden z przycisk�w do pokazania
            comboBox3.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            comboBox4.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            label5.Visible = false; // Zak�adam, �e "przycisk3" to jeden z przycisk�w do pokazania
            label6.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            label7.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
        }

        private async void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {
            // Aktualizacja daty po zmianie w monthCalendar
            selectedDate = monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
        }

        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Aktualizacja nazwy waluty po zmianie w comboBox
            currencyName = comboBox1.SelectedItem.ToString();
        }

        private async void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Pobierz warto�� wprowadzon� przez u�ytkownika w combobox
            string name = comboBox1.SelectedItem.ToString();
            string data = "2023-01-01";
            // Pobierz dat� z monthCalendar1
            if (opcja1)
            {
                data = monthCalendar1.SelectionStart.ToString("yyyy-MM-dd");
            }
            else if (opcja2)
            {
                data = $"{comboBox4.SelectedItem.ToString()}-{comboBox3.SelectedItem.ToString()}-{comboBox2.SelectedItem.ToString()}";
            }
            var appId = "17044b1f9234417a9c4fea3981117b5c";
            var url = $"https://openexchangerates.org/api/historical/{data}.json?app_id={appId}";
            selectedDate = data;
            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.GetStringAsync(url);
                    var exchangeRates = JsonSerializer.Deserialize<ExchangeRatesResponse>(response);
                    textBox1.Text = exchangeRates.NewToString(name, selectedDate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Wyst�pi� b��d: {ex.Message}");
                }
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://openexchangerates.org";

            try
            {
                // Dla .NET Core i nowszych (.NET 5/6/7/8)
                System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // Wa�ne, aby umo�liwi� otwieranie URL w przegl�darce
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                // Logowanie b��du lub wy�wietlenie komunikatu, np. MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            opcja1 = false;
            opcja2 = true;
            // Ukrywanie przycisk�w
            monthCalendar1.Visible = false; // Zak�adam, �e "przycisk1" to jeden z przycisk�w do ukrycia

            // Pokazywanie przycisk�w
            comboBox2.Visible = true; // Zak�adam, �e "przycisk3" to jeden z przycisk�w do pokazania
            comboBox3.Visible = true; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            comboBox4.Visible = true; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            label5.Visible = true; // Zak�adam, �e "przycisk3" to jeden z przycisk�w do pokazania
            label6.Visible = true; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            label7.Visible = true; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            opcja1 = true;
            opcja2 = false;

            // Pokazywanie przycisk�w
            monthCalendar1.Visible = true; // Zak�adam, �e "przycisk1" to jeden z przycisk�w do ukrycia

            // Ukrywanie przycisk�w
            comboBox2.Visible = false; // Zak�adam, �e "przycisk3" to jeden z przycisk�w do pokazania
            comboBox3.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            comboBox4.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            label5.Visible = false; // Zak�adam, �e "przycisk3" to jeden z przycisk�w do pokazania
            label6.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
            label7.Visible = false; // Zak�adam, �e "przycisk4" to kolejny przycisk do pokazania
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
