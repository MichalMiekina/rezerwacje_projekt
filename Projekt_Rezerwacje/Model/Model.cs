using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Projekt_Rezerwacje.Model
{
    using DAL.Entities;
    using DAL.Repositories;
    using System.Windows;
    using System.Windows.Controls;

    class Model
    {
        #region Properties
        public ObservableCollection<Room> Rooms { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<Reservation> Reservations { get; set; } = new ObservableCollection<Reservation>();
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();
        public ObservableCollection<Client> SearchedClients { get; set; } = new ObservableCollection<Client>();
        public string SearchedClient { get; set; }
        #endregion

        public Model()
        {
            var clients = ClientRepository.GetClients();
            foreach (var c in clients)
                Clients.Add(c);
        }

        #region Methods
        //Zwraca wszystkie pokoje w danym hotelu i o podanym standardzie 
        public void GetRooms(int id_h, string package)
        {
            Rooms.Clear();
            var rooms = RoomRepository.GetRooms(id_h, package);
            foreach (var r in rooms)
                Rooms.Add(r);
        }

        //Wyszukuje klientów których nazwisko zawiera wyrażenie
        public void SearchForClient(string SearchedClient)
        {
            SearchedClients.Clear();
            var clients = ClientRepository.SearchClient(SearchedClient);
            foreach (var c in clients)
                SearchedClients.Add(c);
        }

        //Sprawdza czy klient jest w bazie
        public bool IsClientInDataBase(Client client) => Clients.Contains(client);

        //Dodaje klientów do bazy
        public bool AddClient(Client client)
        {
            if (!IsClientInDataBase(client))
            {
                if (ClientRepository.AddClient(client))
                {
                    Clients.Add(client);
                    return true;
                }
            }
            return false;    
        }

        //Zmienia dane klienta w bazie
        public bool EditClient(Client client, int clientID)
        {
            if (ClientRepository.EditClient(client, clientID))
            {
                for (int i = 0; i < Clients.Count; i++)
                {
                    if (Clients[i].ID == clientID)
                    {
                        client.ID = clientID;
                        Clients[i] = new Client(client);
                    }
                }
                return true;
            }
            return false;
        }

        //Usuwa klienta z bazy
        public bool DeleteClient(Client client, int clientID)
        {
            if (IsClientInDataBase(client))
            {
                if (ClientRepository.DeleteClient(clientID))
                {
                    Clients.Remove(client);
                    return true;
                }
            }
            return false;
        }

        //Sprawdza czy rezerwacja jest w bazie
        public bool IsReservationInDataBase(Reservation reservation) => Reservations.Contains(reservation);

        //Zwraca rezerwacje dotyczące danego pokoju
        public void GetReservations(int id_p)
        {
            Reservations.Clear();
            var reserv = ReservationRepository.GetReservations(id_p);
            foreach (var r in reserv)
            {
                Reservations.Add(r);
            }
        }

        //Dodaje rezerwacje do bazy
        public bool AddReservation(Reservation reservation, int id_p)
        {
            if (!IsReservationInDataBase(reservation))
            {
                if (ReservationRepository.AddReservation(reservation, id_p))
                {
                    Reservations.Add(reservation);
                    return true;
                }
            }
            else
                System.Windows.MessageBox.Show($"Rezerwacja jest już w bazie!");
            return false;
        }

        //Usuwa daną rezerwację z bazy danych
        public bool DeleteReservation(Reservation reservation, int reservationID)
        {
            if (IsReservationInDataBase(reservation))
            {
                if (ReservationRepository.DeleteReservation(reservationID))
                {
                    Reservations.Remove(reservation);
                    return true;
                }
            }
            return false;
        }

        //Kończy daną rezerwację
        public bool EndReservation(Reservation reservation)
        {
            if (ReservationRepository.EndReservation((int)reservation.ID))
            {
                for (int i = 0; i < Reservations.Count; i++)
                {
                    if (Reservations[i].ID == reservation.ID)
                    {
                        if (reservation.Ended == "F")
                            reservation.Ended = "T";
                        else
                            reservation.Ended = "F";

                        Reservations.RemoveAt(i);
                        Reservations.Insert(i, reservation);
                    }
                }
                return true;
            }
            return false;
        }
        #endregion
    }
}
