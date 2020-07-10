using Projekt_Rezerwacje.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projekt_Rezerwacje.DAL.Repositories
{
    class ClientRepository
    {
        private const string ALL_CLIENTS = "SELECT * FROM klienci";
        private const string ADD_CLIENT = "INSERT INTO `klienci`(`imię`, `nazwisko`, `telefon`) VALUES ";

        //Zwraca wszystkich klientów z bazy
        public static List<Client> GetClients()
        {
            List<Client> clients = new List<Client>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_CLIENTS, connection);
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    clients.Add(new Client(reader));
                connection.Close();
            }
            return clients;
        }

        //Dodaje danego klienta do bazy
        public static bool AddClient(Client client)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_CLIENT} {client.ToInsert()}", connection);
                connection.Open();
                var id = command.ExecuteNonQuery();
                state = true;
                client.ID = (int)command.LastInsertedId;
                connection.Close();
            }
            return state;
        }

        //Zmienia dane klienta o podanym id
        public static bool EditClient(Client client, int clientID)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_CLIENT = $"UPDATE klienci SET imię='{client.Name}', nazwisko='{client.LastName}', " +
                    $"telefon={client.PhoneNumber} WHERE id_k={clientID}";

                MySqlCommand command = new MySqlCommand(EDIT_CLIENT, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }

        //Usuwa klienta o podanym id
        public static bool DeleteClient(int clientID)
        {
            bool state = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string DELETE_CLIENT = $"DELETE FROM klienci WHERE id_k={clientID}";

                MySqlCommand command = new MySqlCommand(DELETE_CLIENT, connection);
                connection.Open();
                var n = command.ExecuteNonQuery();
                if (n == 1) state = true;

                connection.Close();
            }
            return state;
        }

        //Wyszukuje klientów o nazwisku zawierającym podane wyrażenie
        public static List<Client> SearchClient(string Lastname)
        {
            List<Client> clients = new List<Client>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command;
                if (!string.IsNullOrWhiteSpace(Lastname))
                {
                    command = new MySqlCommand("SELECT * FROM klienci WHERE NAZWISKO LIKE " + "'%" + Lastname + "%'", connection);
                }
                else
                {
                    command = new MySqlCommand(ALL_CLIENTS, connection);
                }
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                    clients.Add(new Client(reader));
                connection.Close();
            }
            
            return clients;
        }
    }
}
