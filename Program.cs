using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using lab2;

class Program
{
    static async Task Main(string[] args)
   {
        Console.WriteLine("Podaj walute:"); // Podawanie waluty
        string name = Console.ReadLine();

        Console.WriteLine("Podaj rok:"); // Podawanie roku
        string rok = Console.ReadLine();

        Console.WriteLine("Podaj miesiąc:"); // Podawanie miesiąca
        string miesiac = Console.ReadLine();

        Console.WriteLine("Podaj dzień:"); // Podawanie dnia
        string dzien = Console.ReadLine();

        // Dodajemy wiodące zera do miesiąca i dnia, jeśli jest to konieczne
        miesiac = miesiac.PadLeft(2, '0'); // Pierwszy arg całkowita długość, drugi co dodajemy w brakujące miejsca po lewej stronie
        dzien = dzien.PadLeft(2, '0'); // -||-

        // Łączymy rok, miesiąc i dzień w jeden ciąg tekstowy
        string data = $"{rok}-{miesiac}-{dzien}";

        var appId = "17044b1f9234417a9c4fea3981117b5c"; // Aktualne id dla API
        var url = $"https://openexchangerates.org/api/historical/{data}.json?app_id={appId}"; // Tworzenie url


        using (var httpClient = new HttpClient()) // Umożliwanie wysyłania żądań HTTP - tworzymy nową instancję klasy HttpClient
        {
            try
            {
                var response = await httpClient.GetStringAsync(url); // Wysyłamy żadanie get do podanego URL i oczekujemy na odpowiedź
                Console.WriteLine(response); 
                var exchangeRates = JsonSerializer.Deserialize<ExchangeRatesResponse>(response); // Deserializujemy odpowiedź JSON do obiektu ExcgangeRatesResponse
                //                var response = await httpClient.GetStringAsync(url);
                //              var exchangeRates = JsonSerializer.Deserialize<ExchangeRatesResponse>(response);

                Console.WriteLine(exchangeRates.NewToString(name, data)); // Wywołanie NewToStringa
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystapil blad: {ex.Message}"); // W razie błędu wyświetla komunikat błędu
            }
        }
    }
}
