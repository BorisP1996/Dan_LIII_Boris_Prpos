using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Command;
using Zadatak_1.View;

namespace Zadatak_1.ViewModel
{
    class MasterViewModel : ViewModelBase
    {
        MasterView master;

        public MasterViewModel(MasterView masterOpen)
        {
            master = masterOpen;
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
            master.Close();
        }
        private bool CanCloseExecute()
        {
            return true;
        }

        private ICommand createManager;
        public ICommand CreateManager
        {
            get
            {
                if (createManager == null)
                {
                    createManager = new RelayCommand(param => CreateManagerExecute(), param => CanCreateManagerExecute());
                }
                return createManager;
            }
        }

        private void CreateManagerExecute()
        {
            try
            {
                CreateManager cm = new CreateManager();
                cm.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCreateManagerExecute()
        {
            return true;
        }

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
                CreateEmploye ce = new CreateEmploye();
                ce.ShowDialog();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private bool CanCreateEmployeExecute()
        {
            return true;
        }
    }
}
