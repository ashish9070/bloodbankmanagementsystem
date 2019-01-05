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
    /// Interaction logic for WindowDataViewer_BloodBank.xaml
    /// </summary>
    public partial class WindowDataViewer_BloodBank : Window
    {
        IBAL<BmsBloodBank> bal = null;
        TextBlock applicationStatus = null;
        List<BmsBloodBank> list = null;

        public WindowDataViewer_BloodBank()
        {
            InitializeComponent();

        }

        public void AttachData(IBAL<BmsBloodBank> bal, TextBlock applicationStatus, object values)
        {
            try
            {
                this.bal = bal;
                this.applicationStatus = applicationStatus;
                this.list = (List<BmsBloodBank>)values;
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
            BmsBloodBank value = dataGridPrimary.SelectedItem as BmsBloodBank;

            try
            {
                if (bal == null) return;
                bool state = bal.Remove(value);
                if (state)
                {
                    applicationStatus.Text = "Blood Bank Information Deleted Successfully.";
                    list.Remove(value);
                    dataGridPrimary.Items.Refresh();

                }
                else
                {
                    applicationStatus.Text = "Failed Deleting Blood Bank Information.";
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

            //dataGridPrimary.ItemsSource = (List<BmsBloodBank>)bal.GetAll();

            //MessageBox.Show(value.BloodBankName);

        }
    }
}
