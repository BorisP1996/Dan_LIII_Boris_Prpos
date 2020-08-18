using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MainWIndowViewModel : ViewModelBase
    {
        MainWindow main;
        Entity context = new Entity();

        public MainWIndowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
        }

        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }
        }
        private void CloseExecute()
        {
            main.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand login;
        public ICommand Login
        {
            get
            {
                if (login == null)
                {
                    login = new RelayCommand(param => LoginExecute(), param => CanLoginExecute());
                }
                return login;
            }
        }
        /// <summary>
        /// It is possible to log in as: master account (credentials in file), employe or manager
        /// </summary>
        private void LoginExecute()
        {
            try
            {
                //take credentials from file and compare with input
                StreamReader sr = new StreamReader(@"..\..\OwnerAcces.txt");
                string line = "";
                List<string> list = new List<string>();

                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
                sr.Close();

                //if master credentials equals input
                if (Username == list[0] && Password == list[1])
                {
                    MasterView mv = new MasterView();
                    mv.ShowDialog();
                    Username = "";
                    Password = "";
                }
                //if manager is logged
                else if (ManagerLoged(Username,Password)==true)
                {
                    MessageBox.Show("Welcome manager");
                    Username = "";
                    Password = "";
                }
                //if employe is logged
                else if (EmployeLoged(Username,Password)==true)
                {
                    MessageBox.Show("Welcome employe");
                    Username = "";
                    Password = "";
                }
                else
                {
                    MessageBox.Show("Invalid Parametres");
                    Username = "";
                    Password = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                throw;
            }
        }
        private bool CanLoginExecute()
        {
            if (String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EmployeLoged(string username,string pasword)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblAll> allUsers = context.tblAlls.ToList();
                    List<tblEmploye> allEmploye = context.tblEmployes.ToList();

                    foreach (tblAll item in allUsers)
                    {
                        if (item.Username==username && item.Pasword==pasword)
                        {
                            foreach (tblEmploye item1 in allEmploye)
                            {
                                if (item1.AllIDemp==item.All_ID)
                                {
                                    return true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }
        public bool ManagerLoged(string username, string pasword)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblAll> allUsers = context.tblAlls.ToList();
                    List<tblManager> allManager = context.tblManagers.ToList();

                    foreach (tblAll item in allUsers)
                    {
                        if (item.Username == username && item.Pasword == pasword)
                        {
                            foreach (tblManager item1 in allManager)
                            {
                                if (item1.AllIDman == item.All_ID)
                                {
                                    return true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }


    }
}
