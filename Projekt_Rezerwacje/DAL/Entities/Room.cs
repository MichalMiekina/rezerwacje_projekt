using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_Rezerwacje.DAL.Entities
{
    class Room
    {
        #region Properties
        public int? ID { set; get; }
        public int Number { set; get; }
        public string Package { set; get; }
        public uint Price { set; get; }
        public int ID_H { set; get; }
        #endregion

        #region Constructors
        public Room(MySqlDataReader reader)
        {
            ID = int.Parse(reader["id_p"].ToString());
            Number = int.Parse(reader["nr"].ToString());
            Package = reader["pakiet"].ToString();
            Price = uint.Parse(reader["cena"].ToString());
            ID_H = int.Parse(reader["id_h"].ToString());
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Numer: {Number} Cena: {Price}zł";
        }
        #endregion
    }
}
