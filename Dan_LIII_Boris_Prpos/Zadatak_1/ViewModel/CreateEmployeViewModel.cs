using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.Model;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    /// <summary>
    /// Class contains everything that is necesary for creating employe
    /// </summary>
    class CreateEmployeViewModel : ViewModelBase
    {
        CreateEmploye ce;
        Entity context = new Entity();

        public CreateEmployeViewModel(CreateEmploye ceOpen)
        {
            ce = ceOpen;
            All = new tblAll();
            Engagment = new tblEngagment();
            EngList = GetEng();
        }

        private tblAll all;
        public tblAll All
        {
            get
            {
                return all;
            }
            set
            {
                all = value;
                OnPropertyChanged("All");
            }
        }
        private string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private string surname;
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
        }
        private string mail;
        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
                OnPropertyChanged("Mail");
            }
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
        private int floor;
        public int Floor
        {
            get
            {
                return floor;
            }
            set
            {
                floor = value;
                OnPropertyChanged("Floor");
            }
        }
        private string gender;
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        private tblEngagment engagment;
        public tblEngagment Engagment
        {
            get
            {
                return engagment;
            }
            set
            {
                engagment = value;
                OnPropertyChanged("Engagment");
            }
        }
        private List<tblEngagment> engList;
        public List<tblEngagment> EngList
        {
            get
            {
                return engList;
            }
            set
            {
                engList = value;
                OnPropertyChanged("EngList");
            }
        }
        private string citizen;
        public string Citizen
        {
            get
            {
                return citizen;
            }
            set
            {
                citizen = value;
                OnPropertyChanged("Citizen");
            }
        }

        //Command for creating employe
        private ICommand createEmploye;
        public ICommand CreateEmploye
        {
            get
            {
                if (createEmploye == null)
                {
                    createEmploye = new RelayCommand(param => CreateEmployeExecute(), param => CanCreateEmployeExecute());
                }
                return createEmploye;
            }
        }

        private void CreateEmployeExecute()
        {
            try
            {
                using (Entity context = new Entity())
                {
                    CreateManager cm = new CreateManager();
                    tblAll newAll = new tblAll();
                    //collecting data from text boxes including validations
                    newAll.FirstName = Name;
                    newAll.Surname = Surname;
                    newAll.Email = Mail;
                    newAll.Username = Username;
                    newAll.Pasword = Password;
                    tblEmploye newEmploye = new tblEmploye();
                    newEmploye.Gender = Gender;
                    newEmploye.EmployeFlor = Floor;
                    newAll.DateOfBirth = All.DateOfBirth;
                    //email must be unique
                    if (CheckMail(newAll.Email) == false)
                    {
                        MessageBox.Show("E-mail already exists");
                    }
                    //username must be unique
                     else if (CheckCredentials(newAll.Username) == false)
                    {
                        MessageBox.Show("Username already exists");
                    }
                    //if everything is ok than proced with saving
                    else if (CheckCredentials(newAll.Username) == true && CheckGender(newEmploye.Gender) == true && CheckFloor(newEmploye.EmployeFlor.GetValueOrDefault()) == true && newAll.DateOfBirth < DateTime.Now.AddYears(-18) && CheckMail(newAll.Email)==true)
                    {
                        
                        context.tblAlls.Add(newAll);
                        context.SaveChanges();
                        //give ID from user table to foreign key in employe table
                        newEmploye.AllIDemp = newAll.All_ID;
                        newEmploye.Citizenship = Citizen;
                        newEmploye.Engagment = Engagment.engName;

                        context.tblEmployes.Add(newEmploye);
                        context.SaveChanges();
                        MessageBox.Show("Employe is created");
                      
                        Name = "";
                        Surname = "";
                        Mail = "";
                        Username = "";
                        Password = "";
                        Gender = "";
                        Floor = 0;
                        Citizen = "";

                    }
                    //NOTE: look at message boxes and you will realise what is the problem
                    else if (CheckFloor(newEmploye.EmployeFlor.GetValueOrDefault()) == false)
                    {
                        MessageBox.Show("Input for floor must be changed because selected floor does not have manager");
                    }
                    else if (CheckGender(newEmploye.Gender) == false)
                    {
                        MessageBox.Show("Invalid gender input");
                    }
                    else if (newAll.DateOfBirth > DateTime.Now.AddYears(-18))
                    {
                        MessageBox.Show("Employe must be at least 18 years old");
                    }
                    else
                    {
                        MessageBox.Show("Make sure that all fields contain valid values"); 
                    }
                   
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCreateEmployeExecute()
        {
            if (String.IsNullOrEmpty(Name) || String.IsNullOrEmpty(Surname) || String.IsNullOrEmpty(Mail) || String.IsNullOrEmpty(Username) || String.IsNullOrEmpty(Password) || String.IsNullOrEmpty(Floor.ToString()) || String.IsNullOrEmpty(Gender) || String.IsNullOrEmpty(Engagment.engName) || String.IsNullOrEmpty(Citizen))
            {
                return false;
            }
            else
            {
                return true;
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
            ce.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }
        /// <summary>
        /// Get list of engagments to combo_box
        /// </summary>
        /// <returns></returns>
        private List<tblEngagment> GetEng()
        {
            List<tblEngagment> list = new List<tblEngagment>();
            list = context.tblEngagments.ToList();

            return list;
        }

        /// <summary>
        /// MEthod checks if username is unique
        /// </summary>
        /// <param name="usernameInput"></param>
        /// <returns></returns>
        private bool CheckCredentials(string usernameInput)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblAll> allEmploye = context.tblAlls.ToList();

                    List<string> usernameList = new List<string>();
                  

                    foreach (tblAll item in allEmploye)
                    {
                        usernameList.Add(item.Username);
                       
                    }

                    if (!usernameList.Contains(usernameInput))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// validates gender
        /// </summary>
        /// <param name="gender"></param>
        /// <returns></returns>
        private bool CheckGender(string gender)
        {
            if (gender == "M" || gender == "Z" || gender == "m" || gender == "z")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Employe can select flor that has manager
        /// </summary>
        /// <param name="flor"></param>
        /// <returns></returns>
        private bool CheckFloor(int flor)
        {
            List<tblManager> managerList = context.tblManagers.ToList();
            List<int> flors = new List<int>();

            foreach (tblManager item in managerList)
            {
                flors.Add(item.ManagerFlor.GetValueOrDefault());
            }

            if (flors.Contains(flor))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Mail must be unique
        /// </summary>
        /// <param name="mailInput"></param>
        /// <returns></returns>
        private bool CheckMail(string mailInput)
        {
            try
            {
                using (Entity context = new Entity())
                {
                    List<tblAll> allEmploye = context.tblAlls.ToList();

                    List<string> mailList = new List<string>();


                    foreach (tblAll item in allEmploye)
                    {
                        mailList.Add(item.Email);
                    }

                    if (!mailList.Contains(mailInput))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

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
