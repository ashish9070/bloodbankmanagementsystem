using BMS.BusinessLayer;
using BMS.Entities;
using BMS.Exceptions;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
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

namespace BMS.PresentationLayerWpf.RegisterWndows
{
    /// <summary>
    /// Interaction logic for WindowRegisterAdmin.xaml
    /// </summary>
    public partial class WindowRegisterAdmin : Window
    {
        public WindowRegisterAdmin()
        {
            InitializeComponent();
            txtSysIdValue.Text = HelperMethods.SystemId();
        }




        IBAL<BmsBloodBank> bal = null;



        public void Attach(IBAL<BmsBloodBank> bal)
        {

            this.bal = bal;

        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

            this.DialogResult = false;
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bal == null) return;

                BmsBloodBank newAdminSystem = FetchData();
                BmsBloodBank currentAdminSystem = null;
                string ConfigSecurityMode = ConfigSecurityMode = ConfigurationManager.AppSettings.Get("security");
                ConfigSecurityMode = string.IsNullOrEmpty(ConfigSecurityMode) ? "low" : ConfigSecurityMode;

                if ( ConfigSecurityMode.Trim().ToLower() == "high")
                {
                    currentAdminSystem = bal.GetAll().FirstOrDefault(item => item.SysId == HelperMethods.SystemId() && item.UserID == newAdminSystem.UserID);
                
                }
                else
                {
                    currentAdminSystem = bal.GetAll().FirstOrDefault(item => item.UserID == newAdminSystem.UserID);
                
                }
                if( currentAdminSystem != null)
                {
                    // this user Id already exists
                    MessageHandler.ShowInfoMessage("User Id [ " + currentAdminSystem.UserID + " ] alrady exists." + Environment.NewLine +
                    "Please provide different User Id");
                    return;
                }
                
                bool state = bal.Add(newAdminSystem);

                if (state)
                {
                    MessageHandler.ShowInfoMessage("Successfully Added to the System");
                }
                else
                {
                    MessageHandler.ShowErrorMessage("Failed to Add you to the System");
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

            this.DialogResult = true;
        }


       

        void EditableFields(bool state = false)
        {
            try { 
            txtBloodBankName.IsEnabled = state;

            txtAddress.IsEnabled = state;
            txtCity.IsEnabled = state;

            txtContactNumber.IsEnabled = state;
            txtUserID.IsEnabled = state;

            txtPassword.IsEnabled = state;
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

        void ClearFields()
        {
            try { 
            txtBloodBankName.Text = "";

            txtAddress.Text = "";
            txtCity.Text = "";

            txtContactNumber.Text = "";
            txtUserID.Text = "";

            txtPassword.Password = "";
            txtSysIdValue.Text = "";
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

        void PopulateFields(BmsBloodBank value)
        {
           try{
            txtBloodBankName.Text = value.BloodBankName;

            txtAddress.Text = value.Address;
            txtCity.Text = value.City;

            txtContactNumber.Text = value.ContactNumber;
            txtUserID.Text = value.UserID;

            txtPassword.Password = value.SysId;
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


        BmsBloodBank FetchData()
        {
            BmsBloodBank value = new BmsBloodBank();
            try { 
            value.BloodBankID = -1;
            value.Address = txtAddress.Text;
            value.BloodBankName = txtBloodBankName.Text;
            value.City = txtCity.Text;
            value.ContactNumber = txtContactNumber.Text;
            value.CreationDate = DateTime.Now;
            value.Password = txtPassword.Password;
            value.SysId = txtSysIdValue.Text;
            value.UserID = txtUserID.Text;
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

            return value;
        }

    }
}
