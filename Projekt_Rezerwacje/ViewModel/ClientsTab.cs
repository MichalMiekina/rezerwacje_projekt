using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekt_Rezerwacje.ViewModel
{
    using Model;
    using DAL.Entities;
    using System.Collections.ObjectModel;

    class ClientsTab : ViewModelBase
    {
        private Model model = null;

        #region Properties
        public ObservableCollection<Client> ListOfClients { get; set; }

        public ClientsTab(Model model)
        {
            this.model = model;
            ListOfClients = model.Clients;
        }

        public Client CurrentClient { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                onPropertyChanged(nameof(Name));
            }
        }

        private string lastname;
        public string LastName
        {
            get { return lastname; }
            set
            {
                lastname = value;
                onPropertyChanged(nameof(LastName));
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                phone = value;
                onPropertyChanged(nameof(Phone));
            }
        }

        private int selectedID = -1;
        public int SelectedID
        {
            get { return selectedID; }
            set
            {
                selectedID = value;
                onPropertyChanged(nameof(SelectedID));
            }
        }
        #endregion

        #region Commands

        // dodaje użytkownika na podstawie danych zawartych w kontrolkach i bindingu
        private ICommand _addClient = null;
        public ICommand AddClient
        {
            get
            {
                if (_addClient == null)
                {
                    _addClient = new RelayCommand(
                        arg =>
                        {
                            var client = new Client(Name, LastName, Phone);

                            if (model.AddClient(client))
                            {
                                System.Windows.MessageBox.Show($"Pomyślnie dodano klienta {Name} {LastName} do bazy!");
                                ClearClient();
                            }
                            else
                                System.Windows.MessageBox.Show($"Klient {Name} {LastName} jest już w bazie!");
                        },
                        arg => !(string.IsNullOrWhiteSpace(Name)) && !(string.IsNullOrWhiteSpace(LastName)) && !(string.IsNullOrWhiteSpace(Phone))
                     );
                }
                return _addClient;
            }
        }
        // usuwa z bazy danych klienta zaznaczonego na liście
        private ICommand _deleteClient = null;
        public ICommand DeleteClient
        {
            get
            {
                if (_deleteClient == null)
                {
                    _deleteClient = new RelayCommand(
                        arg =>
                        {
                            if (model.DeleteClient(CurrentClient, (int)CurrentClient.ID))
                            {
                                System.Windows.MessageBox.Show($"Pomyślnie usunięto klienta z bazy!");
                                SelectedID = -1;
                            }
                        },
                        arg => SelectedID > -1
                     ) ;
                }
                return _deleteClient;
            }
        }
        // edytuje dane klienta w bazie danych który jest zaznacozny na liście
        private ICommand _editClient = null;
        public ICommand EditClient
        {
            get
            {
                if (_editClient == null)
                {
                    _editClient = new RelayCommand(
                        arg =>
                        {
                            var client = new Client(Name, LastName, Phone);

                            if (model.EditClient(client, (int)CurrentClient.ID))
                            {
                                System.Windows.MessageBox.Show($"Pomyślnie edytowano klienta!");
                                SelectedID = -1;
                            }
                        },
                        arg => ((CurrentClient?.Name != Name) || (CurrentClient?.LastName != LastName) || (CurrentClient?.PhoneNumber != Phone)) &&
                        !(string.IsNullOrWhiteSpace(Name)) && !(string.IsNullOrWhiteSpace(LastName)) && !(string.IsNullOrWhiteSpace(Phone))
                     );
                }
                return _editClient;
            }
        }

        //pobiera klienta z bazy danych
        private ICommand _loadClient = null;
        public ICommand LoadClient
        {
            get
            {
                if (_loadClient == null)
                    _loadClient = new RelayCommand(
                        arg =>
                        {
                            if (SelectedID > -1)
                            {
                                Name = CurrentClient.Name;
                                LastName = CurrentClient.LastName;
                                Phone = CurrentClient.PhoneNumber;
                            }
                            else
                            {
                                ClearClient();
                            }
                        }
                        ,
                        arg => true
                        );
                return _loadClient;
            }
        }
        #endregion
        // czyści kontrolki z danych o kliencie
        public void ClearClient()
        {
            Name = "";
            LastName = "";
            Phone = "";
        }
    }
}
