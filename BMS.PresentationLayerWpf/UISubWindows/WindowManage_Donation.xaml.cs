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
    /// Interaction logic for WindowManage_Donation.xaml
    /// </summary>
    public partial class WindowManage_Donation : Window
    {
        Button btnAdd = null;
        Button btnReset = null;

        Button btnUpdate = null;
        Button btnDelete = null;

        IBAL<BmsBloodDonorDonation> bal = null;
        TextBlock applicationStatus = null;


        DispatcherTimer clock = null;

        public WindowManage_Donation()
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

        public void Attach(IBAL<BmsBloodDonorDonation> bal, TextBlock applicationStatus, ActionMode mode = ActionMode.ADD, object value = null)
        {
            try
            {
                this.bal = bal;
                this.applicationStatus = applicationStatus;

                switch (mode)
                {
                    case ActionMode.ADD:
                        {
                            txbBloodDonationID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodDonationID.Visibility = System.Windows.Visibility.Hidden;
                            txtBloodDonationID.Text = "-1";

                            btnAdd = new Button();
                            btnAdd.Name = "btnAdd";
                            btnAdd.Click += btnAdd_Click;
                            btnAdd.Content = "Add Blood Donation";
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
                            PopulateFields((BmsBloodDonorDonation)value);
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
                            PopulateFields((BmsBloodDonorDonation)value);
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
                    applicationStatus.Text = "Blood Donation Information Deleted Successfully.";
                    applicationStatusMessage.Text = "Blood Donation Information Deleted Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Blood Donation Information.";
                    applicationStatusMessage.Text = "Failed Deleting Blood Donation Information.";
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
                    applicationStatus.Text = "Blood Donation Information Added Successfully.";
                    applicationStatusMessage.Text = "Blood Donation Information Added Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Adding Blood Donation Information.";
                    applicationStatusMessage.Text = "Blood Donation Information Added Successfully.";
                }
                clock.Start();

                ClearFields();
                txtBloodDonationID.Text = "-1";
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
                        applicationStatus.Text = "Blood Donation Information Updated Successfully.";
                        applicationStatusMessage.Text = "Blood Donation Information Updated Successfully.";
                    }
                    else
                    {
                        applicationStatus.Text = "Failed updating Blood Donation Information.";
                        applicationStatusMessage.Text = "Failed updating Blood Donation Information.";
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
                txtBloodDonationCampID.IsEnabled = state;
                txtBloodDonationDate.IsEnabled = state;
                txtBloodDonationID.IsEnabled = state;
                txtBloodDonorID.IsEnabled = state;
                txtBloodInventoryID.IsEnabled = state;
                txtHBCount.IsEnabled = state;
                txtNumberofBottle.IsEnabled = state;
                txtWeight.IsEnabled = state;
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
                txtBloodDonationCampID.Text = "";
                txtBloodDonationDate.Text = "";
                txtBloodDonationID.Text = "";
                txtBloodDonorID.Text = "";
                txtBloodInventoryID.Text = "";
                txtHBCount.Text = "";
                txtNumberofBottle.Text = "";
                txtWeight.Text = "";
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void PopulateFields(BmsBloodDonorDonation value)
        {
            try
            {
                txtBloodDonationCampID.Text = value.BloodDonationCampID.ToString();
                txtBloodDonationDate.Text = value.BloodDonationDate.ToShortDateString();
                txtBloodDonationID.Text = value.BloodDonationID.ToString();
                txtBloodDonorID.Text = value.BloodDonorID.ToString();
                txtBloodInventoryID.Text = value.BloodInventoryID.ToString();
                txtHBCount.Text = value.HBCount.ToString();
                txtNumberofBottle.Text = value.NumberofBottle.ToString();
                txtWeight.Text = value.Weight.ToString(); ;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        BmsBloodDonorDonation FetchData()
        {
            BmsBloodDonorDonation value = new BmsBloodDonorDonation();
            try
            {
                value.BloodDonationCampID = int.Parse(txtBloodDonationCampID.Text);
                value.BloodDonationDate = Convert.ToDateTime(txtBloodDonationDate.Text);
                value.BloodDonationID = int.Parse(txtBloodDonationID.Text);
                value.BloodDonorID = int.Parse(txtBloodDonorID.Text);
                value.BloodInventoryID = int.Parse(txtBloodInventoryID.Text);
                value.HBCount = Convert.ToDecimal(txtHBCount.Text);
                value.NumberofBottle = int.Parse(txtNumberofBottle.Text);
                value.Weight = Convert.ToDecimal(txtWeight.Text);
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            return value;
        }

    }
}
