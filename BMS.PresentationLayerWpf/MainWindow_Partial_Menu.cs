using BMS.Entities;
using BMS.PresentationLayerWpf.ChildWindows;
using BMS.PresentationLayerWpf.UserManualDocumentation;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace BMS.PresentationLayerWpf
{
    public partial class MainWindow
    {
        #region Help Menu Event
        private void MenuItem_HelpAbout_Click(object sender, RoutedEventArgs e)
        {
            if (helpWindow == null) helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }


        private void MenuItem_HelpUserManual_Click(object sender, RoutedEventArgs e)
        {
            if (helpWindow == null) helpWindow = new HelpWindow();
            helpWindow.ShowDialog();
        }
        #endregion

        #region File Menu Event

        private void MenuItem_FileNewAdmin_Click(object sender, RoutedEventArgs e)
        {
            ManageData_Bank();

        }

        private void MenuItem_FileChangeAdmin_Click(object sender, RoutedEventArgs e)
        {
            ManageData_CurrentBank(ActionMode.UPDATE, MainWindow.CurrentAdminSystem);
        }



        private void MenuItem_FileBank_Click(object sender, RoutedEventArgs e)
        {
            AttachData_Bank();
        }

        private void MenuItem_FileView_Hospital_Click(object sender, RoutedEventArgs e)
        {
            List<BmsHospital> list = null;
            list = (balHospital.GetAll().Where(v => v.City == MainWindow.CurrentAdminSystem.City)).ToList();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Hospital(balHospital, sbTbAppStatus, list);
            AddTab("Hospital of City", windowChildren.childWindowHolder);
        }

        private void MenuItem_FileView_Event_Click(object sender, RoutedEventArgs e)
        {
            List<BmsBloodDonationCamp> list = null;
            list = (balCamp.GetAll().Where(v => v.BloodBank == MainWindow.CurrentAdminSystem.BloodBankName)).ToList();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Camp(balCamp, sbTbAppStatus, list);
            AddTab("Blood Donation Camps of Blood Bank", windowChildren.childWindowHolder);
        }

        private void MenuItem_FileView_Inventory_Click(object sender, RoutedEventArgs e)
        {
            List<BmsBloodInventory> list = null;
            list = (balInventory.GetAll().Where(v => v.BloodBankID == MainWindow.CurrentAdminSystem.BloodBankID)).ToList();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Inventory(balInventory, sbTbAppStatus, list);
            AddTab("Inventory of Blood Bank", windowChildren.childWindowHolder);
        }

        private void MenuItem_FileView_Donor_Click(object sender, RoutedEventArgs e)
        {
            List<BmsBloodDonor> list = null;
            list = (balDonor.GetAll().Where(v => v.City == MainWindow.CurrentAdminSystem.City)).ToList();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Donor(balDonor, sbTbAppStatus, list);
            AddTab("Blood Donor of City", windowChildren.childWindowHolder);
        }




        private void MenuItem_FileExit_Click(object sender, RoutedEventArgs e)
        {
            CloseApplication();
        }

        #endregion



        #region Transfer Menu Event

        private void MenuItem_Transfer_Inventory_Click(object sender, RoutedEventArgs e)
        {
            txtSearch_ExecuteQuery("add transaction");
        }

        #endregion


        #region Add Menu Event
        private void MenuItem_Add_Inventory_Click(object sender, RoutedEventArgs e)
        {
            txtSearch_ExecuteQuery("add inventory ");
        }

        private void MenuItem_Add_Hospital_Click(object sender, RoutedEventArgs e)
        {
            txtSearch_ExecuteQuery("add hospital ");
        }

        private void MenuItem_Add_Event_Click(object sender, RoutedEventArgs e)
        {
            txtSearch_ExecuteQuery("add event ");
        }

        private void MenuItem_Add_Donor_Click(object sender, RoutedEventArgs e)
        {
            txtSearch_ExecuteQuery("add donor ");
        }

        #endregion


        #region View Menu Event
        private void MenuItem_View_Inventory_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view inventory ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_View_Hospital_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view hospital ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_View_Event_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view event ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_View_Donor_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view donor ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        #endregion



        #region Update Menu Event

        private void MenuItem_Update_Inventory_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view inventory ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_Update_Hospital_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view hospital ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_Update_Event_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view event ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_Update_Donor_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "view donor ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }


        #endregion



        #region Remove Menu Event
        private void MenuItem_Remove_Inventory_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "delete inventory ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_Remove_Hospital_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "delete hospital ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_Remove_Event_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "delete event ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }

        private void MenuItem_Remove_Donor_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Focus();
            txtSearch.Text = "delete donor ";
            txtSearch.CaretIndex = txtSearch.Text.Length;
        }
        #endregion



        #region Menu HotKeys
        void AutoHotKeys()
        {

            List<HotKeysMap> maps = new List<HotKeysMap>();
            //maps.Add(new HotKeysMap() { Key = Key.N, Modifiers = ModifierKeys.Control, Events = Window_Loaded });
            maps.Add(new HotKeysMap() { Key = Key.Q, Modifiers = ModifierKeys.Control, Events = txtRemove_Click });

            ApplyAutoHotKeys(maps);
        }

        void ApplyAutoHotKeys(List<HotKeysMap> maps)
        {
            try
            {
                foreach (HotKeysMap map in maps)
                {
                    RoutedCommand command = new RoutedCommand();
                    command.InputGestures.Add(new KeyGesture(map.Key, map.Modifiers));
                    CommandBindings.Add(new CommandBinding(command, map.Events));

                }
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        #endregion
    }
}
