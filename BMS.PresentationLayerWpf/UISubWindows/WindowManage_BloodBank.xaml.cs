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
    /// Interaction logic for WindowManage_BloodBank.xaml
    /// </summary>
    public partial class WindowManage_BloodBank : Window
    {

        Button btnAdd = null;
        Button btnReset = null;

        Button btnUpdate = null;
        Button btnDelete = null;

        IBAL<BmsBloodBank> bal = null;
        TextBlock applicationStatus = null;


        DispatcherTimer clock = null;


        public WindowManage_BloodBank()
        {
            InitializeComponent();

            clock = new DispatcherTimer();
            clock.Tick += new EventHandler(clock_Tick);
            clock.Interval = new TimeSpan(0, 0, 3);


        }

        public void Attach(IBAL<BmsBloodBank> bal, TextBlock applicationStatus, ActionMode mode = ActionMode.ADD, object value = null)
        {
            try
            {
                string uid = BMS.PresentationLayerWpf.Utility.HelperMethods.UniqueSystemId();
                uid = BMS.PresentationLayerWpf.Utility.HelperMethods.SHA256(uid);
                txtSysIdValue.Text = uid;


                this.bal = bal;
                this.applicationStatus = applicationStatus;

                switch (mode)
                {
                    case ActionMode.ADD:
                        {
                            txbBloodBankID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodBankID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodBankID.Text = "-1";

                            btnAdd = new Button();
                            btnAdd.Name = "btnAdd";
                            btnAdd.Click += btnAdd_Click;
                            btnAdd.Content = "Add Blood Bank";
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
                            PopulateFields((BmsBloodBank)value);
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
                            PopulateFields((BmsBloodBank)value);
                            btnDelete = new Button();
                            btnDelete.Name = "btnDelete";
                            btnDelete.Click += btnDelete_Click;
                            btnDelete.Content = "Delete";
                            stackButtons.Children.Add(btnDelete);

                        }
                        break;
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

        void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bal == null) return;
                bool state = bal.Remove(FetchData());
                if (state)
                {
                    applicationStatus.Text = "Blood Bank Information Deleted Successfully.";
                    applicationStatusMessage.Text = "Blood Bank Information Deleted Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Blood Bank Information.";
                    applicationStatusMessage.Text = "Failed Deleting Blood Bank Information.";
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
                bool state = bal.Add(FetchData());

                if (state)
                {
                    applicationStatus.Text = "Blood Bank Information Added Successfully.";
                    applicationStatusMessage.Text = "Blood Bank Information Added Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Adding Blood Bank Information.";
                    applicationStatusMessage.Text = "Blood Bank Information Added Successfully.";
                }
                clock.Start();

                ClearFields();
                txtBloodBankID.Text = "-1";
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
                        applicationStatus.Text = "Blood Bank Information Updated Successfully.";
                        applicationStatusMessage.Text = "Blood Bank Information Updated Successfully.";
                    }
                    else
                    {
                        applicationStatus.Text = "Failed updating Blood Bank Information.";
                        applicationStatusMessage.Text = "Failed updating Blood Bank Information.";
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
                txtBloodBankName.IsEnabled = state;

                txtAddress.IsEnabled = state;
                txtCity.IsEnabled = state;

                txtContactNumber.IsEnabled = state;
                txtUserID.IsEnabled = state;

                txtPassword.IsEnabled = state;
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
                txtBloodBankName.Text = "";

                txtAddress.Text = "";
                txtCity.Text = "";

                txtContactNumber.Text = "";
                txtUserID.Text = "";

                txtPassword.Password = "";
                txtSysIdValue.Text = "";
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void PopulateFields(BmsBloodBank value)
        {
            try
            {
                txtBloodBankID.Text = value.BloodBankID.ToString();
                txtBloodBankName.Text = value.BloodBankName;

                txtAddress.Text = value.Address;
                txtCity.Text = value.City;

                txtContactNumber.Text = value.ContactNumber;
                txtUserID.Text = value.UserID;

                txtPassword.Password = value.SysId;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        BmsBloodBank FetchData()
        {
            BmsBloodBank value = new BmsBloodBank();
            try
            {
                value.BloodBankID = int.Parse(txtBloodBankID.Text);
                value.Address = txtAddress.Text;
                value.BloodBankName = txtBloodBankName.Text;
                value.City = txtCity.Text;
                value.ContactNumber = txtContactNumber.Text;
                value.CreationDate = DateTime.Now;
                value.Password = txtPassword.Password;
                value.SysId = txtSysIdValue.Text;
                value.UserID = txtUserID.Text;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            return value;
        }

    }
}
