using MySql.Data.MySqlClient;
using Projekt_Rezerwacje.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Rezerwacje.DAL.Repositories
{
    class HotelRepository
    {
        private const string ALL_HOTELS = "SELECT * FROM hotele";

        //Zwraca wszystkie hotele w bazie
        public static List<Hotel> GetHotels()
        {
            List<Hotel> hotels = new List<Hotel>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_HOTELS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    hotels.Add(new Hotel(reader));
                connection.Close();
            }
            return hotels;
        }
    }
}
