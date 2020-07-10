using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Rezerwacje.DAL.Repositories
{
    using MySql.Data.MySqlClient;
    using DAL.Entities;
    class RoomRepository
    {
        //Wybranie wszystkich pokoi w danym hotelu i o podanym standardzie 
        public static List<Room> GetRooms(int id_h, string package)
        {
            List<Room> rooms = new List<Room>();
            string MATCHING_ROOMS = $"SELECT * FROM pokoje WHERE id_h={id_h} AND pakiet='{package}'";

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(MATCHING_ROOMS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    rooms.Add(new Room(reader));
                connection.Close();
            }
            return rooms;
        }
    }
}
