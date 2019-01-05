using BMS.BusinessLayer;
using BMS.Entities;
using BMS.Exceptions;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BMS.PresentationLayerWpf.UISubWindows
{
    /// <summary>
    /// Interaction logic for WindowManage_Inventory.xaml
    /// </summary>
    public partial class WindowManage_Inventory : Window
    {
        Button btnAdd = null;
        Button btnReset = null;

        Button btnUpdate = null;
        Button btnDelete = null;

        IBAL<BmsBloodInventory> bal = null;
        TextBlock applicationStatus = null;



        DispatcherTimer clock = null;
        public WindowManage_Inventory()
        {
            InitializeComponent();

            clock = new DispatcherTimer();
            clock.Tick += new EventHandler(clock_Tick);
            clock.Interval = new TimeSpan(0, 0, 3);
        }

        public void Attach(IBAL<BmsBloodInventory> bal, TextBlock applicationStatus, ActionMode mode = ActionMode.ADD, object value = null)
        {

            try
            {
                this.bal = bal;
                this.applicationStatus = applicationStatus;

                switch (mode)
                {
                    case ActionMode.ADD:
                        {
                            txbBloodInventoryID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodInventoryID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodInventoryID.Text = "-1";

                            btnAdd = new Button();
                            btnAdd.Name = "btnAdd";
                            btnAdd.Click += btnAdd_Click;
                            btnAdd.Content = "Add Inventory";
                            stackButtons.Children.Add(btnAdd);

                            btnReset = new Button();
                            btnReset.Name = "btnReset";
                            btnReset.Click += btnReset_Click;
                            btnReset.Content = "Reset";
                            stackButtons.Children.Add(btnReset);

                        }
                        break;

                    case ActionMode.UPDATE:
                        {
                            PopulateFields((BmsBloodInventory)value);
                            EditableFields();
                            btnUpdate = new Button();
                            btnUpdate.Name = "btnUpdate";
                            btnUpdate.Click += btnUpdate_Click;
                            btnUpdate.Content = "Edit";
                            stackButtons.Children.Add(btnUpdate);

                        }
                        break;

                    case ActionMode.DELETE:
                        {
                            EditableFields();
                            PopulateFields((BmsBloodInventory)value);
                            btnDelete = new Button();
                            btnDelete.Name = "btnDelete";
                            btnDelete.Click += btnDelete_Click;
                            btnDelete.Content = "Delete";
                            stackButtons.Children.Add(btnDelete);

                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }

        }

        void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bal == null) return;
                bool state = bal.Remove(FetchData());
                if (state)
                {
                    applicationStatus.Text = "Inventory Information Deleted Successfully.";
                    applicationStatusMessage.Text = "Inventory Information Deleted Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Inventory Information.";
                    applicationStatusMessage.Text = "Failed Deleting Inventory Information.";
                }
                //clock.Start();
                applicationStatus.Text = "Ready";
                ClearFields();
                EditableFields();
                btnDelete.IsEnabled = false;
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



        void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bal == null) return;
                BmsBloodInventory value = FetchData();
                value.CreationDate = DateTime.Now;
                bool state = bal.Add(value);

                if (state)
                {
                    applicationStatus.Text = "Inventory Information Added Successfully.";
                    applicationStatusMessage.Text = "Inventory Information Added Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Adding Inventory Information.";
                    applicationStatusMessage.Text = "Inventory Information Added Successfully.";
                }
                clock.Start();

                ClearFields();
                txtBloodInventoryID.Text = "-1";
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

        void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnUpdate == null) return;

                if ((string)btnUpdate.Content.ToString() == "Edit")
                {
                    EditableFields(true);
                    btnUpdate.Content = "Update";
                }
                else
                {
                    //EditableFields();
                    //btnUpdate.Content = "Edit";
                    if (bal == null) return;
                    bool state = bal.Modify(FetchData());
                    if (state)
                    {
                        applicationStatus.Text = "Inventory Information Updated Successfully.";
                        applicationStatusMessage.Text = "Inventory Information Updated Successfully.";
                    }
                    else
                    {
                        applicationStatus.Text = "Failed updating Inventory Information.";
                        applicationStatusMessage.Text = "Failed updating Inventory Information.";
                    }
                    clock.Start();

                    //ClearFields();
                }

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

        void btnReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ClearFields();
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

        void clock_Tick(object sender, EventArgs e)
        {
            try
            {
                applicationStatusMessage.Text = "";
                applicationStatus.Text = "Ready";
                clock.Stop();
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void EditableFields(bool state = false)
        {
            try
            {
                txtBloodBankID.IsEnabled = state;
                txtBloodGroup.IsEnabled = state;
                txtBloodInventoryID.IsEnabled = state;
                txtExpiryDate.IsEnabled = state;
                txtNumberofBottles.IsEnabled = state;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void ClearFields()
        {
            try
            {
                txtBloodBankID.Text = "";
                txtBloodGroup.Text = "";
                txtBloodInventoryID.Text = "";
                txtExpiryDate.Text = "";
                txtNumberofBottles.Text = "";
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void PopulateFields(BmsBloodInventory value)
        {
            try
            {
                txtBloodBankID.Text = value.BloodBankID.ToString();
                txtBloodGroup.Text = value.BloodGroup;
                txtBloodInventoryID.Text = value.BloodInventoryID.ToString();
                txtExpiryDate.Text = value.ExpiryDate.ToShortDateString();
                txtNumberofBottles.Text = value.NumberofBottles.ToString();
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        BmsBloodInventory FetchData()
        {
            try
            {
                BmsBloodInventory value = new BmsBloodInventory();

                value.BloodGroup = txtBloodGroup.Text;
                value.BloodBankID = int.Parse(txtBloodBankID.Text);
                value.BloodInventoryID = int.Parse(txtBloodInventoryID.Text);
                value.NumberofBottles = int.Parse(txtNumberofBottles.Text);

                value.ExpiryDate = Convert.ToDateTime(txtExpiryDate.Text);
                value.CreationDate = DateTime.Now;

                return value;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            return null;
        }

    }
}
