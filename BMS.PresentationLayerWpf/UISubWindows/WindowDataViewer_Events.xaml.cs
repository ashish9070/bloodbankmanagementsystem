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

namespace BMS.PresentationLayerWpf.UISubWindows
{
    /// <summary>
    /// Interaction logic for WindowDataViewer_Events.xaml
    /// </summary>
    public partial class WindowDataViewer_Events : Window
    {
        IBAL<BmsBloodDonationCamp> bal = null;
        TextBlock applicationStatus = null;
        List<BmsBloodDonationCamp> list = null;


        public WindowDataViewer_Events()
        {
            InitializeComponent();
        }


        public void AttachData(IBAL<BmsBloodDonationCamp> bal, TextBlock applicationStatus, object values)
        {
            try
            {
                this.bal = bal;
                this.applicationStatus = applicationStatus;
                this.list = (List<BmsBloodDonationCamp>)values;
                dataGridPrimary.ItemsSource = list;
                dataGridPrimary.Items.Refresh();
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

        private void btnControl_Click(object sender, RoutedEventArgs e)
        {
            object selected = dataGridPrimary.SelectedItem;
            BmsBloodDonationCamp value = dataGridPrimary.SelectedItem as BmsBloodDonationCamp;

            try
            {
                if (bal == null) return;
                bool state = bal.Remove(value);
                if (state)
                {
                    applicationStatus.Text = "Blood Donation Camp Information Deleted Successfully.";
                    list.Remove(value);
                    dataGridPrimary.Items.Refresh();

                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Blood Donation Camp Information.";
                }
                //clock.Start();
                applicationStatus.Text = "Ready";

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

            //dataGridPrimary.ItemsSource = (List<BmsBloodDonationCamp>)bal.GetAll();

            //MessageBox.Show(value.BloodBankName);

        }

    }
}
