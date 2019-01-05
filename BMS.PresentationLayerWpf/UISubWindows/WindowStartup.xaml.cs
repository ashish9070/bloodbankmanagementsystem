using BMS.BusinessLayer;
using BMS.Entities;
using BMS.Exceptions;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
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



namespace BMS.PresentationLayerWpf.UISubWindows
{
    /// <summary>
    /// Interaction logic for WindowStartup.xaml
    /// </summary>
    public partial class WindowStartup : Window
    {

        IBAL<BmsBloodBank> balBank = null;
        IBAL<BmsBloodDonationCamp> balCamp = null;
        IBAL<BmsBloodDonor> balDonor = null;
        IBAL<BmsBloodDonorDonation> balDonation = null;
        IBAL<BmsBloodInventory> balInventory = null;
        IBAL<BmsHospital> balHospital = null;
        IBAL<BmsTransaction> balTransaction = null;
        TextBlock applicationStatus = null;


        DispatcherTimer clock = null;

        int MonthRange = 12;

        public WindowStartup()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        public void Attach(IBAL<BmsBloodBank> balBank, IBAL<BmsBloodDonationCamp> balCamp, IBAL<BmsBloodDonor> balDonor,
            IBAL<BmsBloodDonorDonation> balDonation, IBAL<BmsBloodInventory> balInventory, IBAL<BmsHospital> balHospital,
             IBAL<BmsTransaction> balTransaction, TextBlock applicationStatus)
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

                this.applicationStatus = applicationStatus;

                RenderGraphs();

                //clock = new DispatcherTimer();
               // clock.Tick += new EventHandler(clock_Tick);
                //clock.Interval = new TimeSpan(0, 0, 0, 2);
                //clock.Start();

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
                //RenderGraphs();
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

        void RenderGraphs()
        {
            try
            {
                List<List<KeyValuePair<string, int>>> list = new List<List<KeyValuePair<string, int>>>();
                list.Add(Charting.bloodTransactionData);
                list.Add(Charting.bloodTransactionData2);

                Charting.SetBarChart(bloodTransactionBar, list);

                CalanderReg();
                CalanderEvent();
                CalanderTrans();

                PieBloodByGroup();
                PieAllDonorHBCounts();

                BarDonorWeightDistribution();
                BarChartTransaction();
                BarChartVolumeOfBloodInventory();

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

        private void CalanderReg()
        {
            try
            {
                List<DateTime> list = null;
                list = (from e in balDonor.GetAll() where e.CreationDate < DateTime.Now.AddMonths(MonthRange) && e.CreationDate > DateTime.Now.AddMonths(-MonthRange) select e.CreationDate).ToList();

                Style s = (Style)Resources["styleCalendarDayButtonStyleReg"];
                foreach (var holidayDates in list)
                {
                    DataTrigger dataTrigger = new DataTrigger() { Binding = new Binding("Date"), Value = holidayDates };
                    dataTrigger.Setters.Add(new Setter(System.Windows.Controls.Primitives.CalendarDayButton.BackgroundProperty, Brushes.GreenYellow));
                    s.Triggers.Add(dataTrigger);
                }
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }

        }

        private void CalanderEvent()
        {
            try
            {
                List<DateTime> list = null;
                list = (from e in balCamp.GetAll() where e.CampStartDate < DateTime.Now.AddMonths(MonthRange) && e.CampStartDate > DateTime.Now.AddMonths(-MonthRange) select e.CampStartDate).ToList();

                Style s = (Style)Resources["styleCalendarDayButtonStyleEvent"];
                foreach (var holidayDates in list)
                {
                    DataTrigger dataTrigger = new DataTrigger() { Binding = new Binding("Date"), Value = holidayDates };
                    dataTrigger.Setters.Add(new Setter(System.Windows.Controls.Primitives.CalendarDayButton.BackgroundProperty, Brushes.DeepSkyBlue));
                    s.Triggers.Add(dataTrigger);
                }
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        private void CalanderTrans()
        {
            try
            {
                List<DateTime> list = null;
                //list = (from e in balTransaction.GetAll() where e.CreationDate < DateTime.Now.AddMonths(MonthRange) && e.CreationDate > DateTime.Now.AddMonths(-MonthRange) select e.CreationDate).ToList();
                list = (from e in balTransaction.GetAll() where e.CreationDate < DateTime.Now.AddMonths(MonthRange) && e.CreationDate > DateTime.Now.AddMonths(-MonthRange) select e.CreationDate).ToList();

                Style s = (Style)Resources["styleCalendarDayButtonStyleTrans"];
                foreach (var holidayDates in list)
                {
                    DataTrigger dataTrigger = new DataTrigger() { Binding = new Binding("Date"), Value = holidayDates };
                    dataTrigger.Setters.Add(new Setter(System.Windows.Controls.Primitives.CalendarDayButton.BackgroundProperty, Brushes.OrangeRed));
                    s.Triggers.Add(dataTrigger);
                }
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }

        }

        private void PieBloodByGroup()
        {
            try
            {
                List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

                var inventories = balInventory.GetAll().Where(item => item.BloodBankID == MainWindow.CurrentAdminSystem.BloodBankID);
                var inventoryByGroup = inventories.GroupBy(inventory => inventory.BloodGroup).Select(group => new { Key = group.Key, Count = group.Sum(bottles => bottles.NumberofBottles) }).OrderBy(x => x.Key);
                foreach (var item in inventoryByGroup)
                {
                    list.Add(new KeyValuePair<string, int>(item.Key, item.Count));

                }
                List<string> colors = ColorShades.blueRedShades.ToList();
                colors.Reverse();
                Charting.SetPieChart(bloodByGroup, colors, list);
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        private void BarChartVolumeOfBloodInventory()
        {
            try
            {
                List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
                var inventories = balInventory.GetAll().Where(item => item.BloodBankID == MainWindow.CurrentAdminSystem.BloodBankID);
                var inventoryByGroup = inventories.GroupBy(inventory => inventory.BloodInventoryID).Select(group => new { Key = group.Key, Count = group.Sum(bottles => bottles.NumberofBottles) }).OrderBy(x => x.Key);
                int inventoryCount = 1;
                foreach (var item in inventoryByGroup)
                {
                    list.Add(new KeyValuePair<string, int>((inventoryCount++).ToString(), item.Count));

                }
                VolumeOfBloodInventory.DataContext = list;
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }




        private void BarDonorWeightDistribution()
        {
            try
            {
                List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();

                var donorsWeights = balDonation.GetAll().GroupBy(donor => donor.Weight).Select(group => new { Key = group.Key, Count = group.Count() }).OrderBy(x => x.Key);


                foreach (var item in donorsWeights)
                {
                    list.Add(new KeyValuePair<string, int>(item.Key.ToString(), item.Count));

                }
                donorWeightDistribution.DataContext = list;
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        private void PieAllDonorHBCounts()
        {
            try
            {
                List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
                var donations = balDonation.GetAll().GroupBy(donor => donor.HBCount).Select(group => new { Key = group.Key, Count = group.Count() }).OrderBy(x => x.Key);


                foreach (var item in donations)
                {
                    list.Add(new KeyValuePair<string, int>(item.Key.ToString(), item.Count));

                }
                List<string> colors = ColorShades.blueRedShades.ToList();
                //colors.Reverse();
                Charting.SetPieChart(pieChartAllDonorHBCounts, colors, list);
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }


        private void BarChartTransaction()
        {
            try
            {
                List<KeyValuePair<string, int>> list = new List<KeyValuePair<string, int>>();
                var inventories = (from item in balInventory.GetAll() where item.BloodBankID == MainWindow.CurrentAdminSystem.BloodBankID select item.BloodInventoryID).ToList();

                var transactions = balTransaction.GetAll().Where(item => inventories.Contains(item.BloodInventoryID));
                var transactionGroupBy = transactions.GroupBy(trans => trans.HospitalID).Select(group => new { Key = group.Key, Count = group.Sum(bottles => bottles.NumberofBottles) }).OrderBy(x => x.Key);

                int inventoryCount = 1;
                foreach (var item in transactionGroupBy)
                {
                    list.Add(new KeyValuePair<string, int>((inventoryCount++).ToString(), item.Count));

                }



                bloodTransactionBar.DataContext = list;
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }





        private void calDonorReg_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                SelectedDatesCollection cc = calDonorReg.SelectedDates;

                foreach (var item in cc)
                {
                    Debug.WriteLine(item.ToShortDateString());
                    //list.Items.Add(item.ToShortDateString());
                }
            }
            catch (Exception ex)
            {

                MessageHandler.ShowErrorMessage(ex.Message);
            }
        }

        private void calDonorReg_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            MessageBox.Show(calDonorReg.SelectedDate.ToString());
        }




    }
}
