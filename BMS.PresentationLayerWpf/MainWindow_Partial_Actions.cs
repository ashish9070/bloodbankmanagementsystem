using BMS.BusinessLayer;
using BMS.Entities;
using BMS.PresentationLayerWpf.ChildWindows;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BMS.PresentationLayerWpf
{
    public partial class MainWindow
    {



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_BankView_Click(object sender, RoutedEventArgs e)
        {
            AttachData_Bank();
        }//end ApplyAutoHotKeys


        private void WindowAdd_StartupWindow()
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.LoadWindow_StartupTab(balBank, balCamp, balDonor, balDonation, balInventory, balHospital, balTransaction, sbTbAppStatus);
            AddTab("Welcome Page", windowChildren.childWindowHolder);
        }

        private void WindowAdd_PageNotFound()
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.LoadWindow_PageNotFound();
            AddTab("Page Not Found", windowChildren.childWindowHolder);
        }



        ////////////////////////////////////////////////

        public void AttachData_Bank()
        {
            List<BmsBloodBank> list = null;
            list = balBank.GetAll();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Bank(balBank, sbTbAppStatus, list);
            AddTab("Blood Bank", windowChildren.childWindowHolder);
        }

        public void AttachData_Camp()
        {
            List<BmsBloodDonationCamp> list = null;
            list = balCamp.GetAll();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Camp(balCamp, sbTbAppStatus, list);
            AddTab("Blood Donation Camps", windowChildren.childWindowHolder);
        }

        public void AttachData_Donor()
        {
            List<BmsBloodDonor> list = null;
            list = balDonor.GetAll();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Donor(balDonor, sbTbAppStatus, list);
            AddTab("Blood Donor", windowChildren.childWindowHolder);
        }

        public void AttachData_Donation()
        {
            List<BmsBloodDonorDonation> list = null;
            list = balDonation.GetAll();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Donation(balDonation, sbTbAppStatus, list);
            AddTab("Blood Donation", windowChildren.childWindowHolder);
        }

        public void AttachData_Inventory()
        {
            List<BmsBloodInventory> list = null;
            list = balInventory.GetAll();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Inventory(balInventory, sbTbAppStatus, list);
            AddTab("Inventory", windowChildren.childWindowHolder);
        }

        public void AttachData_Hospital()
        {
            List<BmsHospital> list = null;
            list = balHospital.GetAll();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Hospital(balHospital, sbTbAppStatus, list);
            AddTab("Hospital", windowChildren.childWindowHolder);
        }

        public void AttachData_Transaction()
        {
            List<BmsHospital> list = null;
            list = balHospital.GetAll();
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.AttachData_Transaction(balTransaction, sbTbAppStatus, list);
            AddTab("Transaction", windowChildren.childWindowHolder);
        }


        /////////////////

        public void ManageData_Bank(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Bank(balBank, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Blood Bank";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Blood Bank Record";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Blood Bank Record";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }



        public void ManageData_CurrentBank(ActionMode mode = ActionMode.UPDATE, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Bank(balBank, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Blood Bank";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Blood Bank Record";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Blood Bank Record";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }

        public void ManageData_Camp(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Camp(balCamp, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Event";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Event Record";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Event Record";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }

        public void ManageData_Donor(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Donor(balDonor, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Donor";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Donor Record";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Donor Record";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }

        public void ManageData_Donation(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Donation(balDonation, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Donation";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Donation Record";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Donation Record";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }

        public void ManageData_Inventory(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Inventory(balInventory, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Inventory";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Inventory";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Inventory";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }

        public void ManageData_Hospital(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Hospital(balHospital, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Hospital";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Hospital Record";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Hospital Record";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }


        public void ManageData_Transaction(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            WindowChildren windowChildren = new WindowChildren();
            windowChildren.ManageData_Transaction(balTransaction, balHospital, balInventory, sbTbAppStatus, mode, value);

            if (tabHeader == null)
            {
                if (mode == ActionMode.ADD)
                {
                    tabHeader = "Add Transaction";
                }
                else if (mode == ActionMode.DELETE)
                {
                    tabHeader = "Delete Transaction Record";
                }

                else if (mode == ActionMode.UPDATE)
                {
                    tabHeader = "Update Transaction Record";
                }
            }
            AddTab(tabHeader, windowChildren.childWindowHolder);
        }




    }
}
