using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Projekt_Rezerwacje.DAL.Entities
{
    class Client
    {
        #region Properties
        public int? ID { set; get; }
        public string Name { set; get; }
        public string LastName { set; get; }
        public string PhoneNumber { set; get; }
        #endregion

        #region Constructors
        public Client(MySqlDataReader reader)
        {
            ID = int.Parse(reader["id_k"].ToString());
            Name = reader["imię"].ToString();
            LastName = reader["nazwisko"].ToString();
            PhoneNumber = reader["telefon"].ToString();
        }

        public Client(string name, string lastName, string phoneNumber)
        {
            ID = null;
            Name = name.Trim();
            LastName = lastName.Trim();
            PhoneNumber = phoneNumber;
        }

        public Client(Client client)
        {
            ID = client.ID;
            Name = client.Name;
            LastName = client.LastName;
            PhoneNumber = client.PhoneNumber;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Name} {LastName} {PhoneNumber}";
        }


        public string ToInsert()
        {
            return $"('{Name}', '{LastName}', '{PhoneNumber}')";
        }

        public override bool Equals(object obj)
        {
            var client = obj as Client;
            if (client is null) return false;
            if (Name.ToLower() != client.Name.ToLower()) return false;
            if (LastName.ToLower() != client.LastName.ToLower()) return false;
            if (PhoneNumber != client.PhoneNumber) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        #endregion
    }
}

