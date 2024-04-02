using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


namespace lab2
{
    public class ExchangeRatesResponse
    {
        [JsonPropertyName("rates")] // Dla atrybutu rates
        public Dictionary<string, double> Rates { get; set; } // Słownik par - dla walut i kursów

        [JsonPropertyName("base")] // Dla atrybutu base
        public string Base { get; set; } // Usd
        public string Date { get; set; } // Data

        public ExchangeRatesResponse() // Konstruktor...
        {
            Rates = new Dictionary<string, double>(); // Inicjalizacja słownika
        }
        public string NewToString(string name, string data)
        {

            // Pobranie wybranego kursu, jeśli jest dostępny
            double value;
            if (Rates.TryGetValue(name, out value)) // Sprawdzamy czy dla podanej waluty jest dostępny kurs w słowniku rates
            {
                return $"W dniu {data}, 1 {Base} kosztował {value} {name}"; // Jeśli jest
            }
            else
            {
                return $"W dniu {data}, kurs {name} nie jest dostępny dla {Base}."; // Jeśli nie ma
            }
        }
    }
    /// <summary>
    /// Nic pod spodem nie ma znaczenia, bo nie używamy bazy danych
    /// </summary>

    internal class Exchange
    {
        public int Id { get; set; }
        public required string CurrencyName { get; set; }

        public ICollection<ExchangeRate> ExchangeRates { get; set; }
    }

    internal class ExchangeRate
    {
        public int ExchangeRateId { get; set; }
        public int Id { get; set; }
        public Exchange Currency { get; set; }
        public required double Rate { get; set; }
        public required DateTime Date { get; set; }
    }


    internal class exchange_db : DbContext
    {
        public DbSet<Exchange> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Skonfiguruj połączenie z bazą danych SQLite
            optionsBuilder.UseSqlite("Data Source=dbexchange.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Zdefiniuj klucze główne i relacje między tabelami, jeśli są potrzebne
            modelBuilder.Entity<ExchangeRate>()
                .HasKey(e => new { e.Id, e.Date });

            modelBuilder.Entity<ExchangeRate>()
                .HasOne(e => e.Currency)
                .WithMany(c => c.ExchangeRates)
                .HasForeignKey(e => e.Id);
        }
    }
}
