using BMS.Exceptions;
using BMS.PresentationLayerWpf.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BMS.PresentationLayerWpf
{
    public partial class MainWindow
    {

        private void txtPrev_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tbcPrimary.SelectedIndex - 1;
            if (newIndex < 0)
            {
                newIndex = tbcPrimary.Items.Count - 1;
            }
            tbcPrimary.SelectedIndex = newIndex;
        }

        private void txtSelect_Click(object sender, RoutedEventArgs e)
        {
            this.Title = "Selected Tab : " + (tbcPrimary.SelectedItem as TabItem).Header;
        }

        private void txtNext_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tbcPrimary.SelectedIndex + 1;
            if (newIndex >= tbcPrimary.Items.Count)
            {
                newIndex = 0;
            }
            tbcPrimary.SelectedIndex = newIndex;
        }
        protected int eventCounter = 0;
        private void tabHeader_BubbledClickEvent(object sender, RoutedEventArgs e)
        {
            Label source = e.Source as Label;
            TabItem dest = sender as TabItem;

            if (source != null && dest != null && source.Content.ToString().Trim().ToLower().Equals("x"))
            {
                tbcPrimary.Items.Remove(dest);
            }
        }

        private void AddTab(string tabHeading = null, DockPanel mainContent = null)
        {
            try
            {
                Random rnd = new Random();
                int rndvalue = rnd.Next(1000, 9000);



                /////////////////// START : TAB HEADER ///////////////////
                Label lblHeaderLabel = new Label();


                lblHeaderLabel.Content = (tabHeading == null) ? string.Format("Tab {0}", rndvalue) : tabHeading;
                lblHeaderLabel.Name = string.Format("lblTab{0}", rndvalue);
                lblHeaderLabel.Style = Resources["styleLblTabHeader"] as Style;

                Label lblHeaderTabClose = new Label();
                lblHeaderTabClose.Content = " x ";
                lblHeaderTabClose.Name = string.Format("btnTab{0}", rndvalue);
                lblHeaderTabClose.Style = Resources["styleBtnTabHeader"] as Style;
                lblHeaderTabClose.MouseUp += tabHeader_BubbledClickEvent;
                lblHeaderTabClose.Tag = string.Format("tab{0}", rndvalue); // will help to close the corresponding tab


                StackPanel headerStackPanel = new StackPanel();
                headerStackPanel.Orientation = Orientation.Horizontal;
                headerStackPanel.Children.Add(lblHeaderLabel);
                headerStackPanel.Children.Add(lblHeaderTabClose);
                headerStackPanel.Name = string.Format("spHeadTab{0}", rndvalue);
                headerStackPanel.MouseUp += tabHeader_BubbledClickEvent;

                /////////////////// END : TAB HEADER ///////////////////

                TabItem tab = new TabItem();

                tab.Header = headerStackPanel;
                tab.Name = string.Format("tab{0}", rndvalue);
                tab.MouseUp += tabHeader_BubbledClickEvent;
                tab.Content = mainContent;
                tab.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                tab.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;

                tbcPrimary.Items.Add(tab);

                // Select current Tab
                int newIndex = tbcPrimary.Items.Count;
                if (newIndex >= tbcPrimary.Items.Count)
                {
                    --newIndex;
                }
                if (newIndex < 0) newIndex = 0;
                tbcPrimary.SelectedIndex = newIndex;

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

        private void txtInsert_Click(object sender, RoutedEventArgs e)
        {
            WindowAdd_PageNotFound();
        }

        private void txtRemove_Click(object sender, RoutedEventArgs e)
        {
            int newIndex = tbcPrimary.SelectedIndex;
            if (newIndex <= tbcPrimary.Items.Count && newIndex >= 0)
            {
                tbcPrimary.Items.RemoveAt(newIndex);
            }
        }



    }
}
