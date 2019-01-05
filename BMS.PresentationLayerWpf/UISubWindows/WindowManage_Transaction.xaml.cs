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
    /// Interaction logic for WindowManage_Transaction.xaml
    /// </summary>
    public partial class WindowManage_Transaction : Window
    {
        Button btnAdd = null;
        Button btnReset = null;

        Button btnUpdate = null;
        Button btnDelete = null;

        IBAL<BmsTransaction> balTransaction = null;
        IBAL<BmsHospital> balHospital = null;
        IBAL<BmsBloodInventory> balInventory = null;


        TextBlock applicationStatus = null;



        DispatcherTimer clock = null;

        public WindowManage_Transaction()
        {
            try
            {
                InitializeComponent();

                clock = new DispatcherTimer();
                clock.Tick += new EventHandler(clock_Tick);
                clock.Interval = new TimeSpan(0, 0, 3);
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        public void Attach(IBAL<BmsTransaction> balTransaction, IBAL<BmsHospital> balHospital,
            IBAL<BmsBloodInventory> balInventory, TextBlock applicationStatus, ActionMode mode = ActionMode.ADD, object value = null)
        {

            try
            {
                this.balTransaction = balTransaction;
                this.balInventory = balInventory;
                this.balHospital = balHospital;

                this.applicationStatus = applicationStatus;

                switch (mode)
                {
                    case ActionMode.ADD:
                        {
                            txbTransactionID.Visibility = System.Windows.Visibility.Hidden;
                            txtTransactionID.Visibility = System.Windows.Visibility.Hidden;
                            txtTransactionID.Text = "-1";

                            var hospitalList = balHospital.GetAll().Where(c => c.City == MainWindow.CurrentAdminSystem.City);
                            foreach (var item in hospitalList)
                            {
                                txtHospitalID.Items.Add(item.HospitalID);
                            }

                            var inentoryList = balInventory.GetAll().Where(c => c.BloodBankID == MainWindow.CurrentAdminSystem.BloodBankID);
                            foreach (var item in inentoryList)
                            {
                                txtBloodInventoryID.Items.Add(item.BloodInventoryID);
                            }

                            txtCreationDate.Text = DateTime.Now.ToShortDateString();

                            btnAdd = new Button();
                            btnAdd.Name = "btnAdd";
                            btnAdd.Click += btnAdd_Click;
                            btnAdd.Content = "Transaction";
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
                            PopulateFields((BmsTransaction)value);
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
                            PopulateFields((BmsTransaction)value);
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
                if (balTransaction == null) return;
                bool state = balTransaction.Remove(FetchData());
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
                if (balTransaction == null) return;
                BmsTransaction transaction = FetchData();
                bool state = balTransaction.Add(transaction);

                if (state)
                {
                    // Change Inventory value
                    BmsBloodInventory oldValue = balInventory.GetAll().
                FirstOrDefault(c => c.BloodInventoryID == transaction.BloodInventoryID);

                    // Make transaction
                    oldValue.NumberofBottles += transaction.NumberofBottles;
                    BmsBloodInventory newValue = oldValue;
                    state = balInventory.Modify(oldValue);


                }

                if (state)
                {

                    applicationStatus.Text = "Blood Transaction Information Added Successfully.";
                    applicationStatusMessage.Text = "Blood Transaction Information Added Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Adding Blood Transaction Information.";
                    applicationStatusMessage.Text = "Blood Transaction Information Added Successfully.";
                }
                clock.Start();
                EditableFields(false);
                btnAdd.Visibility = System.Windows.Visibility.Hidden;
                btnReset.Visibility = System.Windows.Visibility.Hidden;
                //ClearFields();
                txtTransactionID.Text = "-1";
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
                    if (balTransaction == null) return;
                    bool state = balTransaction.Modify(FetchData());
                    if (state)
                    {
                        applicationStatus.Text = "Blood Transaction Information Updated Successfully.";
                        applicationStatusMessage.Text = "Blood Transaction Information Updated Successfully.";
                    }
                    else
                    {
                        applicationStatus.Text = "Failed updating Blood Transaction Information.";
                        applicationStatusMessage.Text = "Failed updating Blood Transaction Information.";
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
                txtBloodInventoryID.IsEnabled = state;
                txtCreationDate.IsEnabled = state;
                txtHospitalID.IsEnabled = state;
                txtNumberofBottles.IsEnabled = state;
                txtTransactionID.IsEnabled = state;
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

                //txtBloodInventoryID.Text = "";
                txtCreationDate.Text = DateTime.Now.ToShortDateString();
                //txtHospitalID.Text = "";
                txtNumberofBottles.Text = "";
                txtTransactionID.Text = "";
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void PopulateFields(BmsTransaction value)
        {
            try
            {
                txtBloodInventoryID.Text = value.BloodInventoryID.ToString();
                txtCreationDate.Text = value.CreationDate.ToShortDateString();
                txtHospitalID.Text = value.HospitalID.ToString();
                txtNumberofBottles.Text = value.NumberofBottles.ToString();
                txtTransactionID.Text = value.TransactionID.ToString();
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        BmsTransaction FetchData()
        {
            try
            {
                BmsTransaction value = new BmsTransaction();
                value.BloodInventoryID = int.Parse(txtBloodInventoryID.Text);
                value.HospitalID = int.Parse(txtHospitalID.Text);
                value.TransactionID = int.Parse(txtTransactionID.Text);

                value.CreationDate = Convert.ToDateTime(txtCreationDate.Text);
                value.NumberofBottles = int.Parse(txtNumberofBottles.Text);

                return value;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            return null;
        }

        private void txtBloodInventoryID_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtBloodInventoryID == null) return;
            int selectedInventoryId = (int)txtBloodInventoryID.SelectedValue;
            //MessageBox.Show(selectedText);
            txtNumberofBottles.Text = balInventory.GetAll().
                FirstOrDefault(c => c.BloodInventoryID == selectedInventoryId).NumberofBottles.ToString();
        }

    }
}
