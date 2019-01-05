using BMS.BusinessLayer;
using BMS.Entities;
using BMS.Exceptions;
using BMS.PresentationLayerWpf.ChildWindows;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BMS.PresentationLayerWpf
{
    delegate void ManageData_Delegates(ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null);
    delegate void AttachData_Delegates<T>(IBAL<T> bal, TextBlock applicationStatus, object values);

    public partial class MainWindow
    {
        string[] keyWords_add = new string[] { "add", "a", "+" };
        string[] keyWords_view = new string[] { "view", "v", "*" };
        string[] keyWords_delete = new string[] { "delete", "del", "remove", "rem", "d", "-" };

        string[] keyWords_bank = new string[] { "bank", "b", "admin", "bnk", "banks", "bs", "admins", "bnks" };
        string[] keyWords_camp = new string[] { "camp", "c", "event", "camps", "cam", "cs", "events", "evnt" };
        string[] keyWords_donation = new string[] { "donation", "f", "dn", "don", "dona" };
        string[] keyWords_donor = new string[] { "donor", "e", "dnr", "dr", "donr" };
        string[] keyWords_hospital = new string[] { "hospital", "hos", "h", "hosp" };
        string[] keyWords_inventory = new string[] { "inventory", "i", "in", "inve" };
        string[] keyWords_transaction = new string[] { "transaction", "t", "tr", "trans" };


        void CallManageFunction(ManageData_Delegates manager, ActionMode mode = ActionMode.ADD, object value = null, string tabHeader = null)
        {
            manager(mode, value, tabHeader);
        }


        private void txtSearch_ExecuteQuery(string query)
        {
            try
            {
                query = RemoveSpecialCharacters(query.Trim().ToLower()).Trim();
                string[] words = query.Split(' ');

                if (TestContains(words[0], keyWords_add))
                {
                    if (TestContains(words[1], keyWords_bank))
                    {
                        //ManageData_Bank(ActionMode.ADD, null, "Add New Bank Details");
                        CallManageFunction(ManageData_Bank, ActionMode.ADD, null, "Add New Bank Details");
                    }

                    if (TestContains(words[1], keyWords_camp))
                    {
                        //ManageData_Camp(ActionMode.ADD, null, "Add New Event Details");
                        CallManageFunction(ManageData_Camp, ActionMode.ADD, null, "Add New Event Details");
                    }
                    if (TestContains(words[1], keyWords_donation))
                    {
                        //ManageData_Donation(ActionMode.ADD, null, "Add Donation Details");
                        CallManageFunction(ManageData_Donation, ActionMode.ADD, null, "Add Donation Details");
                    }
                    if (TestContains(words[1], keyWords_donor))
                    {
                        //ManageData_Donor(ActionMode.ADD, null, "Add Donor Details");
                        CallManageFunction(ManageData_Donor, ActionMode.ADD, null, "Add Donor Details");
                    }

                    if (TestContains(words[1], keyWords_hospital))
                    {
                        //ManageData_Hospital(ActionMode.ADD, null, "Add Hospital Details");
                        CallManageFunction(ManageData_Hospital, ActionMode.ADD, null, "Add Hospital Details");
                    }
                    if (TestContains(words[1], keyWords_inventory))
                    {
                        //ManageData_Inventory(ActionMode.ADD, null, "Add Inventory Details");
                        CallManageFunction(ManageData_Inventory, ActionMode.ADD, null, "Add Inventory Details");
                    }
                    if (TestContains(words[1], keyWords_transaction))
                    {
                        //ManageData_Transaction(ActionMode.ADD, null, "Add Transaction");
                        CallManageFunction(ManageData_Transaction, ActionMode.ADD, null, "Make Transaction");
                    }

                }

                if (TestContains(words[0], keyWords_view))
                {
                    if (TestContains(words[1], keyWords_bank))
                    {
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balBank.GetAll().FirstOrDefault(c => c.BloodBankID == id);
                                ManageData_Bank(ActionMode.UPDATE, value, "View Bank Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodBank> list = null;
                            list = balBank.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Bank(balBank, sbTbAppStatus, list);
                            AddTab("View Blood Bank", windowChildren.childWindowHolder);
                        }
                    }

                    if (TestContains(words[1], keyWords_camp))
                    {
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balCamp.GetAll().FirstOrDefault(c => c.BloodDonationCampID == id);
                                ManageData_Camp(ActionMode.UPDATE, value, "View Donation Camps Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodDonationCamp> list = null;
                            list = balCamp.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Camp(balCamp, sbTbAppStatus, list);
                            AddTab("View Donation Camps Details", windowChildren.childWindowHolder);
                        }
                    }

                    if (TestContains(words[1], keyWords_donation))
                    {
                        //ManageData_Donation(ActionMode.ADD, null, "Add Donation Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balDonation.GetAll().FirstOrDefault(c => c.BloodDonationCampID == id);
                                ManageData_Donation(ActionMode.UPDATE, value, "View Donation Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodDonorDonation> list = null;
                            list = balDonation.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Donation(balDonation, sbTbAppStatus, list);
                            AddTab("View Donation Details", windowChildren.childWindowHolder);
                        }

                    }
                    if (TestContains(words[1], keyWords_donor))
                    {
                        //ManageData_Donor(ActionMode.ADD, null, "Add Donor Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balDonor.GetAll().FirstOrDefault(c => c.BloodDonorID == id);
                                ManageData_Donor(ActionMode.UPDATE, value, "View Donor Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodDonor> list = null;
                            list = balDonor.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Donor(balDonor, sbTbAppStatus, list);
                            AddTab("View Donor Details", windowChildren.childWindowHolder);
                        }
                    }

                    if (TestContains(words[1], keyWords_hospital))
                    {
                        //ManageData_Hospital(ActionMode.ADD, null, "Add Hospital Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balHospital.GetAll().FirstOrDefault(c => c.HospitalID == id);
                                ManageData_Hospital(ActionMode.UPDATE, value, "View Hospital for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsHospital> list = null;
                            list = balHospital.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Hospital(balHospital, sbTbAppStatus, list);
                            AddTab("View Hospital Details", windowChildren.childWindowHolder);
                        }
                    }
                    if (TestContains(words[1], keyWords_inventory))
                    {
                        //ManageData_Inventory(ActionMode.ADD, null, "Add Inventory Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balInventory.GetAll().FirstOrDefault(c => c.BloodInventoryID == id);
                                ManageData_Inventory(ActionMode.UPDATE, value, "View Inventory for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodInventory> list = null;
                            list = balInventory.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Inventory(balInventory, sbTbAppStatus, list);
                            AddTab("View Inventory Details", windowChildren.childWindowHolder);
                        }
                    }
                    if (TestContains(words[1], keyWords_transaction))
                    {
                        //ManageData_Transaction(ActionMode.ADD, null, "Add Transaction");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balTransaction.GetAll().FirstOrDefault(c => c.BloodInventoryID == id);
                                ManageData_Transaction(ActionMode.UPDATE, value, "View Transaction for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsTransaction> list = null;
                            list = balTransaction.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Transaction(balTransaction, sbTbAppStatus, list);
                            AddTab("View Transaction Details", windowChildren.childWindowHolder);
                        }
                    }

                }

                if (TestContains(words[0], keyWords_delete))
                {
                    if (TestContains(words[1], keyWords_bank))
                    {
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balBank.GetAll().FirstOrDefault(c => c.BloodBankID == id);
                                ManageData_Bank(ActionMode.DELETE, value, "View Bank Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodBank> list = null;
                            list = balBank.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Bank(balBank, sbTbAppStatus, list);
                            AddTab("View Blood Bank", windowChildren.childWindowHolder);
                        }
                    }

                    if (TestContains(words[1], keyWords_camp))
                    {
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balCamp.GetAll().FirstOrDefault(c => c.BloodDonationCampID == id);
                                ManageData_Camp(ActionMode.DELETE, value, "View Donation Camps Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodDonationCamp> list = null;
                            list = balCamp.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Camp(balCamp, sbTbAppStatus, list);
                            AddTab("View Donation Camps Details", windowChildren.childWindowHolder);
                        }
                    }

                    if (TestContains(words[1], keyWords_donation))
                    {
                        //ManageData_Donation(ActionMode.ADD, null, "Add Donation Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balDonation.GetAll().FirstOrDefault(c => c.BloodDonationCampID == id);
                                ManageData_Donation(ActionMode.DELETE, value, "View Donation Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodDonorDonation> list = null;
                            list = balDonation.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Donation(balDonation, sbTbAppStatus, list);
                            AddTab("View Donation Details", windowChildren.childWindowHolder);
                        }

                    }
                    if (TestContains(words[1], keyWords_donor))
                    {
                        //ManageData_Donor(ActionMode.ADD, null, "Add Donor Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balDonor.GetAll().FirstOrDefault(c => c.BloodDonorID == id);
                                ManageData_Donor(ActionMode.DELETE, value, "View Donor Details for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodDonor> list = null;
                            list = balDonor.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Donor(balDonor, sbTbAppStatus, list);
                            AddTab("View Donor Details", windowChildren.childWindowHolder);
                        }
                    }

                    if (TestContains(words[1], keyWords_hospital))
                    {
                        //ManageData_Hospital(ActionMode.ADD, null, "Add Hospital Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balHospital.GetAll().FirstOrDefault(c => c.HospitalID == id);
                                ManageData_Hospital(ActionMode.DELETE, value, "View Hospital for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsHospital> list = null;
                            list = balHospital.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Hospital(balHospital, sbTbAppStatus, list);
                            AddTab("View Hospital Details", windowChildren.childWindowHolder);
                        }
                    }
                    if (TestContains(words[1], keyWords_inventory))
                    {
                        //ManageData_Inventory(ActionMode.ADD, null, "Add Inventory Details");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balInventory.GetAll().FirstOrDefault(c => c.BloodInventoryID == id);
                                ManageData_Inventory(ActionMode.DELETE, value, "View Inventory for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsBloodInventory> list = null;
                            list = balInventory.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Inventory(balInventory, sbTbAppStatus, list);
                            AddTab("View Inventory Details", windowChildren.childWindowHolder);
                        }
                    }
                    if (TestContains(words[1], keyWords_transaction))
                    {
                        //ManageData_Transaction(ActionMode.ADD, null, "Add Transaction");
                        if (words.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(words[2]))
                            {
                                int id = int.Parse(words[2]);
                                object value = balTransaction.GetAll().FirstOrDefault(c => c.BloodInventoryID == id);
                                ManageData_Transaction(ActionMode.DELETE, value, "View Transaction for ID " + id);
                            }
                        }
                        else
                        {
                            List<BmsTransaction> list = null;
                            list = balTransaction.GetAll();
                            WindowChildren windowChildren = new WindowChildren();
                            windowChildren.AttachData_Transaction(balTransaction, sbTbAppStatus, list);
                            AddTab("View Transaction Details", windowChildren.childWindowHolder);
                        }
                    }

                }
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }

            catch (ValidationException ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            catch (ConnectedDalException ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        string RemoveSpecialCharacters(string query)
        {
            query = query.Replace("+", " + ");
            query = query.Replace("-", " - ");
            query = query.Replace("*", " * ");
            query = query.Replace("/", " / ");
            query = Regex.Replace(query, @"[^0-9a-zA-Z\+\-\*\/ ]+", "");

            //query = Regex.Replace(query, @"[^0-9a-zA-Z ]+", "");
            query = Regex.Replace(query, @"[  ]+", " ");
            return query;
        }

        bool TestContains(params string[] words)
        {
            bool found = false;
            string searchWord = words[0];
            for (int i = 1; i < words.Length; i++)
            {
                if (searchWord == words[i])
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

        bool TestContains(string testWith, string[] words)
        {
            bool found = false;
            string searchWord = testWith.Trim();
            for (int i = 0; i < words.Length; i++)
            {
                if (searchWord == words[i].Trim())
                {
                    found = true;
                    break;
                }
            }

            return found;
        }

    }
}
