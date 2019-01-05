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

namespace BMS.PresentationLayerWpf.ChildWindows
{
    /// <summary>
    /// Interaction logic for WindowStartup.xaml
    /// </summary>
    public partial class WindowStartup : Window
    {
        public WindowStartup()
        {
            InitializeComponent();
            Design();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello");
        }

        private void cal_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Calendar defines four events: DisplayModeChanged, DisplayDateChanged, SelectionModeChanged, and SelectedDatesChanged.
            SelectedDatesCollection cc = cal.SelectedDates;

            foreach (var item in cc)
            {

                list.Items.Add(item.ToShortDateString());
            }

        }

        void Design()
        {
            List<DateTime> dates = new List<DateTime>() {
                new DateTime(2017, 11, 22),
                new DateTime(2017, 11, 13),
                new DateTime(2017, 11, 12),
                new DateTime(2017, 11, 23),
                new DateTime(2017, 11, 21)
            
            };

            Style s = (Style)Resources["styleCalendarDayButtonStyle"];
            foreach (var holidayDates in dates)
            {
                DataTrigger dataTrigger = new DataTrigger() { Binding = new Binding("Date"), Value = holidayDates };
                dataTrigger.Setters.Add(new Setter(System.Windows.Controls.Primitives.CalendarDayButton.BackgroundProperty, Brushes.OrangeRed));
                s.Triggers.Add(dataTrigger);
            }

        }
    }
}
