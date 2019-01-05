using BMS.BusinessLayer;
using BMS.Entities;
using BMS.Exceptions;
using BMS.PresentationLayerWpf;
using BMS.PresentationLayerWpf.RegisterWndows;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
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

namespace BMS.PresentationLayer.SplashScreenNS
{
    /// <summary>
    /// Interaction logic for WindowSplashScreen.xaml
    /// </summary>
    public partial class WindowSplashScreen : Window
    {
        DispatcherTimer clock = null;
        Random random = null;

        BloodBankBAL balBank = null;
        BloodDonationCampBAL balCamp = null;
        BloodDonorBAL balDonor = null;
        BloodDonorDonationBAL balDonation = null;
        BloodInventoryBAL balInventory = null;
        HospitalBAL balHospital = null;
        TransactionBAL balTransaction = null;

        string ConfigSecurityMode = null;
   
        void InitBal()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
           
            balBank = new BloodBankBAL(connectionString);
            balCamp = new BloodDonationCampBAL(connectionString);
            balDonor = new BloodDonorBAL(connectionString);
            balDonation = new BloodDonorDonationBAL(connectionString);
            balInventory = new BloodInventoryBAL(connectionString);
            balHospital = new HospitalBAL(connectionString);
            balTransaction = new TransactionBAL(connectionString);
        }

        bool CheckBal()
        {
            bool state = (balBank != null) && (balBank != null) && (balBank != null) && (balBank != null) && (balBank != null) &&
                (balBank != null) && (balBank != null);
            return state;
        }
        public WindowSplashScreen()
        {
            InitializeComponent();
            clock = new DispatcherTimer();
            clock.Tick += new EventHandler( clock_Tick);
            clock.Interval = new TimeSpan(0, 0, 0, 0, 200);
            clock.Start();

            random = new Random();
            InitBal();

            ConfigSecurityMode = ConfigurationManager.AppSettings.Get("security");
            ConfigSecurityMode = string.IsNullOrEmpty(ConfigSecurityMode) ? "low" : ConfigSecurityMode;

            //MessageBox.Show(BMS.PresentationLayerWpf.Utility.HelperMethods.UniqueSystemId());
            

        }
        bool? IsSuccessAdminRegisterLogin = null;
        void clock_Tick(object sender, EventArgs e)
        {
            try
            {
                int tick = (int)psbProgramStartup.Value;
                tick += random.Next(20);
                psbProgramStartup.Value = tick;
                if (tick >= 95 && CheckBal())
                {

                    BmsBloodBank currentAdminSystem = balBank.GetAll().FirstOrDefault(item => item.SysId.Trim() == HelperMethods.SystemId());


                    if (currentAdminSystem == null)
                    {
                        // Show Admin Registration Forms
                        WindowRegisterAdmin regWindow = new WindowRegisterAdmin();
                        regWindow.Attach(balBank);
                        IsSuccessAdminRegisterLogin = regWindow.ShowDialog();

                        if (IsSuccessAdminRegisterLogin == false)
                        {
                            AccessDeny();
                        }

                        currentAdminSystem = balBank.GetAll().FirstOrDefault(item => item.SysId == HelperMethods.SystemId());

                    }

                    MainWindow mainWindow = new MainWindow();
                    MainWindow.CurrentAdminSystem = currentAdminSystem;
                    mainWindow.InitBal(balBank, balCamp, balDonor, balDonation, balInventory, balHospital, balTransaction);
                    mainWindow.Show();



                    clock.Stop();

                    this.Close();
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


        void AccessDeny()
        {
            MessageBoxResult result = MessageBox.Show("ACCESS DENY", "Unauthorized User", MessageBoxButton.OK, MessageBoxImage.Stop);
            if (result == MessageBoxResult.OK)
            {
                Application.Current.Shutdown();
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
