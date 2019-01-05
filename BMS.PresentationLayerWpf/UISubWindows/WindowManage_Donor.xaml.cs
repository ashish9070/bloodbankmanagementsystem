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
    /// Interaction logic for WindowManage_Donor.xaml
    /// </summary>
    public partial class WindowManage_Donor : Window
    {
        Button btnAdd = null;
        Button btnReset = null;

        Button btnUpdate = null;
        Button btnDelete = null;

        IBAL<BmsBloodDonor> bal = null;
        TextBlock applicationStatus = null;


        DispatcherTimer clock = null;

        public WindowManage_Donor()
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

        public void Attach(IBAL<BmsBloodDonor> bal, TextBlock applicationStatus, ActionMode mode = ActionMode.ADD, object value = null)
        {

            try
            {
                this.bal = bal;
                this.applicationStatus = applicationStatus;

                switch (mode)
                {
                    case ActionMode.ADD:
                        {
                            txbBloodDonorID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodDonorID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodDonorID.Text = "-1";

                            btnAdd = new Button();
                            btnAdd.Name = "btnAdd";
                            btnAdd.Click += btnAdd_Click;
                            btnAdd.Content = "Add Blood Donor";
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
                            PopulateFields((BmsBloodDonor)value);
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
                            PopulateFields((BmsBloodDonor)value);
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
                    applicationStatus.Text = "Blood Donor Information Deleted Successfully.";
                    applicationStatusMessage.Text = "Blood Donor Information Deleted Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Blood Donor Information.";
                    applicationStatusMessage.Text = "Failed Deleting Blood Donor Information.";
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
                    applicationStatus.Text = "Blood Donor Information Added Successfully.";
                    applicationStatusMessage.Text = "Blood Donor Information Added Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Adding Blood Donor Information.";
                    applicationStatusMessage.Text = "Blood Donor Information Added Successfully.";
                }
                clock.Start();

                ClearFields();
                txtBloodDonorID.Text = "-1";
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
                        applicationStatus.Text = "Blood Donor Information Updated Successfully.";
                        applicationStatusMessage.Text = "Blood Donor Information Updated Successfully.";
                    }
                    else
                    {
                        applicationStatus.Text = "Failed updating Blood Donor Information.";
                        applicationStatusMessage.Text = "Failed updating Blood Donor Information.";
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
                txtAddress.IsEnabled = state;
                txtBloodDonorID.IsEnabled = state;
                txtBloodGroup.IsEnabled = state;
                txtCity.IsEnabled = state;
                txtFirstName.IsEnabled = state;
                txtLastName.IsEnabled = state;
                txtMobileNo.IsEnabled = state;
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
                txtAddress.Text = "";
                txtBloodDonorID.Text = "";
                txtBloodGroup.Text = "";
                txtCity.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtMobileNo.Text = "";
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void PopulateFields(BmsBloodDonor value)
        {
            try
            {
                txtAddress.Text = value.Address;
                txtBloodDonorID.Text = value.BloodDonorID.ToString();
                txtBloodGroup.Text = value.BloodGroup;
                txtCity.Text = value.City;
                txtFirstName.Text = value.FirstName;
                txtLastName.Text = value.LastName;
                txtMobileNo.Text = value.MobileNo;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        BmsBloodDonor FetchData()
        {
            BmsBloodDonor value = new BmsBloodDonor();
            try
            {
                value.BloodDonorID = int.Parse(txtBloodDonorID.Text);
                value.Address = txtAddress.Text;
                value.BloodGroup = txtBloodGroup.Text;
                value.City = txtCity.Text;
                value.MobileNo = txtMobileNo.Text;
                value.CreationDate = DateTime.Now;
                value.FirstName = txtFirstName.Text;
                value.LastName = txtLastName.Text;
                
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            return value;
        }

    }
}
