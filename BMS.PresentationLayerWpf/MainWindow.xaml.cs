using BMS.BusinessLayer;
using BMS.Entities;
using BMS.Exceptions;
using BMS.PresentationLayer.LoginScreenNS;
using BMS.PresentationLayerWpf.ChildWindows;
using BMS.PresentationLayerWpf.UserManualDocumentation;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMS.PresentationLayerWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Data Members

        bool? IsSuccessfulLogin = null;
        HelpWindow helpWindow = null;


        // Business Access References
        BloodBankBAL balBank = null;
        BloodDonationCampBAL balCamp = null;
        BloodDonorBAL balDonor = null;
        BloodDonorDonationBAL balDonation = null;
        BloodInventoryBAL balInventory = null;
        HospitalBAL balHospital = null;
        TransactionBAL balTransaction = null;

        static BmsBloodBank currentAdminSystem = null;

        public static BmsBloodBank CurrentAdminSystem
        {
            get { return currentAdminSystem; }
            set { currentAdminSystem = value; }
        }

        #endregion


        public MainWindow()
        {
            InitializeComponent();
            AutoHotKeys();

        }


        public void InitBal(BloodBankBAL balBank, BloodDonationCampBAL balCamp, BloodDonorBAL balDonor,
            BloodDonorDonationBAL balDonation, BloodInventoryBAL balInventory, HospitalBAL balHospital, TransactionBAL balTransaction)
        {
            try
            {
                this.balBank = balBank;
                this.balCamp = balCamp;
                this.balDonor = balDonor;
                this.balDonation = balDonation;
                this.balInventory = balInventory;
                this.balHospital = balHospital;
                this.balTransaction = balTransaction;
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

        #region Windows Event

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            WindowLogin windowLogin = new WindowLogin();
            windowLogin.Attach(balBank);
            IsSuccessfulLogin = windowLogin.ShowDialog();

            if (IsSuccessfulLogin == false || IsSuccessfulLogin == null)
            {
                AccessDeny();
            }
            else
            {
                WindowAdd_StartupWindow();
            }

        }

        protected override void OnClosed(EventArgs e)
        {
            CloseApplication();
            base.OnClosed(e);

        }




        private void CloseApplication()
        {
            // Add logic to update database
            Application.Current.Shutdown();
        }

        void AccessDeny()
        {
            MessageBoxResult result = MessageBox.Show("ACCESS DENY", "Unauthorized User", MessageBoxButton.OK, MessageBoxImage.Stop);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }


        #endregion

        private void MenuItem_FileStartupPage_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd_StartupWindow();
        }



    }
}
