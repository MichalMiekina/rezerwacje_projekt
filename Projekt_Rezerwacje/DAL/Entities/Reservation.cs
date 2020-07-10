using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Rezerwacje.DAL.Entities
{
    class Reservation
    {
        #region Properties
        public int? ID { set; get; }
        public string Discount { set; get; }
        public string Ended { set; get; }
        public Client Client { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        #endregion

        #region Constructors
        public Reservation(MySqlDataReader reader)
        {
            ID = int.Parse(reader["id_r"].ToString());
            Discount = reader["zniżka"].ToString();
            Ended = reader["zakończono"].ToString();
            StartDate = DateTime.Parse(reader["od"].ToString());
            EndDate = DateTime.Parse(reader["do"].ToString());
            Client = new Client(reader);
        }

        public Reservation(string discount, string ended, Client client, DateTime startDate, DateTime endDate)
        {
            ID = null;
            Discount = discount;
            Ended = ended;
            Client = client;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Reservation(Reservation reservation)
        {
            ID = reservation.ID;
            Discount = reservation.Discount;
            Ended = reservation.Ended;
            Client = reservation.Client;
            StartDate = reservation.StartDate;
            EndDate = reservation.EndDate;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"{Client.Name} {Client.LastName} {Discount} {Ended} {StartDate:dd.MM.yyyy} {EndDate:dd.MM.yyyy}";
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Reservation reservation)) return false;
            if (Discount != reservation.Discount) return false;
            if (Ended != reservation.Ended) return false;
            if (Client != reservation.Client) return false;
            if (StartDate != reservation.StartDate) return false;
            if (EndDate != reservation.EndDate) return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string ToInsert()
        {
            return $"('{Client.ID}', '{StartDate:yyyy-MM-dd}', '{EndDate:yyyy-MM-dd}')";
        }
        #endregion
    }
}
