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
    /// Interaction logic for WindowManage_Hospital.xaml
    /// </summary>
    public partial class WindowManage_Hospital : Window
    {

        Button btnAdd = null;
        Button btnReset = null;

        Button btnUpdate = null;
        Button btnDelete = null;

        IBAL<BmsHospital> bal = null;
        TextBlock applicationStatus = null;


        DispatcherTimer clock = null;

        public WindowManage_Hospital()
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


        public void Attach(IBAL<BmsHospital> bal, TextBlock applicationStatus, ActionMode mode = ActionMode.ADD, object value = null)
        {

            try
            {
                this.bal = bal;
                this.applicationStatus = applicationStatus;

                switch (mode)
                {
                    case ActionMode.ADD:
                        {
                            txbHospitalID.Visibility = System.Windows.Visibility.Hidden;
                            txtHospitalID.Visibility = System.Windows.Visibility.Hidden;
                            txtHospitalID.Text = "-1";

                            btnAdd = new Button();
                            btnAdd.Name = "btnAdd";
                            btnAdd.Click += btnAdd_Click;
                            btnAdd.Content = "Add Hospital";
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
                            PopulateFields((BmsHospital)value);
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
                            PopulateFields((BmsHospital)value);
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
                    applicationStatus.Text = "Hospital Information Deleted Successfully.";
                    applicationStatusMessage.Text = "Hospital Information Deleted Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Hospital Information.";
                    applicationStatusMessage.Text = "Failed Deleting Hospital Information.";
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
                    applicationStatus.Text = "Hospital Information Added Successfully.";
                    applicationStatusMessage.Text = "Hospital Information Added Successfully.";
                }
                else
                {
                    applicationStatus.Text = "Failed Adding Hospital Information.";
                    applicationStatusMessage.Text = "Hospital Information Added Successfully.";
                }
                clock.Start();

                ClearFields();
                txtHospitalID.Text = "-1";
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
                        applicationStatus.Text = "Hospital Information Updated Successfully.";
                        applicationStatusMessage.Text = "Hospital Information Updated Successfully.";
                    }
                    else
                    {
                        applicationStatus.Text = "Failed updating Hospital Information.";
                        applicationStatusMessage.Text = "Failed updating Hospital Information.";
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
                txtCity.IsEnabled = state;
                txtContactNo.IsEnabled = state;
                txtHospitalID.IsEnabled = state;
                txtHospitalName.IsEnabled = state;
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
                txtContactNo.Text = "";
                txtHospitalID.Text = "";
                txtCity.Text = "";
                txtHospitalName.Text = "";
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        void PopulateFields(BmsHospital value)
        {
            try
            {
                txtAddress.Text = value.Address;
                txtCity.Text = value.City;
                txtContactNo.Text = value.ContactNo;
                txtHospitalID.Text = value.HospitalID.ToString();
                txtHospitalName.Text = value.HospitalName;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        BmsHospital FetchData()
        {
            BmsHospital value = new BmsHospital();
            try
            {
                value.Address = txtAddress.Text;
                value.City = txtCity.Text;
                value.ContactNo = txtContactNo.Text;
                value.HospitalID = int.Parse(txtHospitalID.Text);
                value.HospitalName = txtHospitalName.Text;
            }
            catch (Exception ex)
            {
                MessageHandler.ShowErrorMessage(ex.Message);
            }
            return value;
        }


    }
}
