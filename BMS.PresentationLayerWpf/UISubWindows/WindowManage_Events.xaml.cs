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
    /// Interaction logic for WindowManage_Events.xaml
    /// </summary>
    public partial class WindowManage_Events : Window
    {
        Button btnAdd = null;
        Button btnReset = null;

        Button btnUpdate = null;
        Button btnDelete = null;

        IBAL<BmsBloodDonationCamp> bal = null;
        TextBlock applicationStatus = null;


        DispatcherTimer clock = null;

        public WindowManage_Events()
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

        public void Attach(IBAL<BmsBloodDonationCamp> bal, TextBlock applicationStatus, ActionMode mode = ActionMode.ADD, object value = null)
        {

            try
            {
                this.bal = bal;
                this.applicationStatus = applicationStatus;

                switch (mode)
                {
                    case ActionMode.ADD:
                        {
                            txbBloodDonationCampID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodDonationCampID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodDonationCampID.Text = "-1";

                            btnAdd = new Button();
                            btnAdd.Name = "btnAdd";
                            btnAdd.Click += btnAdd_Click;
                            btnAdd.Content = "Add Blood Donation Camp";
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
                            PopulateFields((BmsBloodDonationCamp)value);
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
                            PopulateFields((BmsBloodDonationCamp)value);
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
                    applicationStatus.Text = "Blood Donation Camp Information Deleted Successfully.";
                    applicationStatusMessage.Text = "Blood Donation Camp Information Deleted Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Blood Donation Camp Information.";
                    applicationStatusMessage.Text = "Failed Deleting Blood Donation Camp Information.";
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
                    applicationStatus.Text = "Blood Donation Camp Information Added Successfully.";
                    applicationStatusMessage.Text = "Blood Donation Camp Information Added Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Adding Blood Donation Camp Information.";
                    applicationStatusMessage.Text = "Blood Donation Camp Information Added Successfully.";
                }
                clock.Start();

                ClearFields();
                txtBloodDonationCampID.Text = "-1";
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
                        applicationStatus.Text = "Blood Donation Camp Information Updated Successfully.";
                        applicationStatusMessage.Text = "Blood Donation Camp Information Updated Successfully.";
                    }
                    else
                    {
                        applicationStatus.Text = "Failed updating Blood Donation Camp Information.";
                        applicationStatusMessage.Text = "Failed updating Blood Donation Camp Information.";
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
                txtBloodBank.IsEnabled = state;
                txtAddress.IsEnabled = state;
                txtCity.IsEnabled = state;
                txtBloodDonationCampID.IsEnabled = state;
                txtCampEndDate.IsEnabled = state;
                txtCampName.IsEnabled = state;
                txtCampStartDate.IsEnabled = state;
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
                txtBloodBank.Text = "";
                txtBloodDonationCampID.Text = "";
                txtCity.Text = "";
                txtCampEndDate.Text = "";
                txtCampName.Text = "";
                txtCampStartDate.Text = "";
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void PopulateFields(BmsBloodDonationCamp value)
        {
            try
            {
                txtAddress.Text = value.Address;
                txtCity.Text = value.City;
                txtBloodBank.Text = value.BloodBank;
                txtBloodDonationCampID.Text = value.BloodDonationCampID.ToString();
                txtCampEndDate.Text = value.CampEndDate.ToShortDateString();
                txtCampName.Text = value.CampName;
                txtCampStartDate.Text = value.CampStartDate.ToShortDateString();
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        BmsBloodDonationCamp FetchData()
        {
            BmsBloodDonationCamp value = new BmsBloodDonationCamp();
            try
            {
                value.Address = txtAddress.Text;
                value.City = txtCity.Text;
                value.BloodBank = txtBloodBank.Text;
                value.BloodDonationCampID = int.Parse(txtBloodDonationCampID.Text);
                value.CampName = txtCampName.Text;
                value.CampEndDate = Convert.ToDateTime(txtCampEndDate.Text);
                value.CampStartDate = Convert.ToDateTime(txtCampStartDate.Text);
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            return value;
        }

    }
}
