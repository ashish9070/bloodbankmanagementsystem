using BMS.BusinessLayer;
using BMS.Entities;
using BMS.Exceptions;
using BMS.PresentationLayerWpf;
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

namespace BMS.PresentationLayer.LoginScreenNS
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        BloodBankBAL balBank = null;

       

        public void Attach(BloodBankBAL balBank)
        {
            this.balBank = balBank;
        }

        public WindowLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try { 
            string ConfigSecurityMode = ConfigSecurityMode = ConfigurationManager.AppSettings.Get("security");
            ConfigSecurityMode = string.IsNullOrEmpty(ConfigSecurityMode) ? "low" : ConfigSecurityMode;

            if (MainWindow.CurrentAdminSystem != null && ConfigSecurityMode.Trim().ToLower() == "high")
            {
                if (txtUserName.Text == MainWindow.CurrentAdminSystem.UserID && txtPassword.Password == MainWindow.CurrentAdminSystem.Password)
                {
                    this.DialogResult = true;
                }
                else
                {
                    this.DialogResult = false;
                }
            
            }
            else
            {
                this.DialogResult = ValidUser();
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

        bool ValidUser()
        {
            bool valid = false;
            var list = balBank.GetAll();
            foreach (var item in list)
            {
                if (txtUserName.Text == item.UserID && txtPassword.Password == item.Password)
                {
                    valid = true;
                    MainWindow.CurrentAdminSystem = item;
                    break;
                }
            }
            return valid;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            
            this.DialogResult = false;
        }

       
    }
}
