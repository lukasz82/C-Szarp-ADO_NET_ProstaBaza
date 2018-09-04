using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.Common;
using static System.Console;

namespace DataProviderFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uzyskuję łańcuch połączenia i dostawcę pliku *.config
            string dataProvider = ConfigurationManager.AppSettings["provider"];
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(dataProvider);

            using (DbConnection connection = factory.CreateConnection())
            {
                if (connection == null)
                {
                    ShowError("Connection");
                    return;
                }

                WriteLine($"Twoim obiektem połączenia jest {connection.GetType().Name}");
                connection.ConnectionString = connectionString;
                connection.Open();

                //Tworze obiekt polecenia
                DbCommand command = factory.CreateCommand();
                if (command == null)
                {
                    ShowError("Command");
                    return;
                }

                WriteLine($"Twoim obiektem połączenia jest {command.GetType().Name}");
                command.Connection = connection;
                command.CommandText = "Select * from Inventory";

                using (DbDataReader dataReader = command.ExecuteReader())
                {
                    WriteLine($"Twój obiekt odczytujący to : {dataReader.GetType().Name}");
                    WriteLine("\n**** Wyświetlam wyniki zapytania sql ****");
                    while (dataReader.Read())
                        WriteLine($" Id samochodu {dataReader["CarId"]} jest typu {dataReader["Make"]}");
                }

                ReadLine();
                
            }
        }

        private static void ShowError(string v)
        {
            WriteLine($"Coś poszło nie tak -> {v}");
            ReadLine();
        }
    }
}
