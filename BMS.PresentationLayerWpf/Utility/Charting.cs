using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Media;

namespace BMS.PresentationLayerWpf.Utility
{
    public class Charting
    {

        public static KeyValuePair<string, int>[] bloodByGroupData = new KeyValuePair<string, int>[] {
                new KeyValuePair<string, int>("A+", 30),
                new KeyValuePair<string, int>("B+", 15),
                new KeyValuePair<string, int>("O+", 20),
                new KeyValuePair<string, int>("AB+", 30),
                new KeyValuePair<string, int>("A-", 8),
                new KeyValuePair<string, int>("B-", 19) ,
                new KeyValuePair<string, int>("O-", 12) };

        public static KeyValuePair<string, int>[] hospitalDemandData = new KeyValuePair<string, int>[] {
                new KeyValuePair<string, int>("Hospital A", 12),
                new KeyValuePair<string, int>("Hospital B", 15),
                new KeyValuePair<string, int>("Hospital C", 20),
                new KeyValuePair<string, int>("Hospital D", 15),
                new KeyValuePair<string, int>("Hospital E", 8),
                new KeyValuePair<string, int>("Others", 30) };


        public static List<KeyValuePair<string, int>> bloodTransactionData = new List<KeyValuePair<string, int>> {
                new KeyValuePair<string, int>("Hospital A", 12),
                new KeyValuePair<string, int>("Hospital B", 15),
                new KeyValuePair<string, int>("Hospital C", -20),
                new KeyValuePair<string, int>("Hospital D", 15),
                new KeyValuePair<string, int>("Hospital E", 8),
                new KeyValuePair<string, int>("Hospital F", -15),
                new KeyValuePair<string, int>("Hospital G", 20),
                new KeyValuePair<string, int>("Hospital H", 15),
                new KeyValuePair<string, int>("Hospital I", 8),
                new KeyValuePair<string, int>("Hospital J", -8),
                new KeyValuePair<string, int>("Hospital K", 15),
                new KeyValuePair<string, int>("Hospital L", 20),
                new KeyValuePair<string, int>("Hospital M", -15),
                new KeyValuePair<string, int>("Others", 30) };

        public static List<KeyValuePair<string, int>> bloodTransactionData2 = new List<KeyValuePair<string, int>> {
                new KeyValuePair<string, int>("Hospital A", -12),
                new KeyValuePair<string, int>("Hospital B", 15),
                new KeyValuePair<string, int>("Hospital C", -20),
                new KeyValuePair<string, int>("Hospital D", -15),
                new KeyValuePair<string, int>("Hospital E", 8),
                new KeyValuePair<string, int>("Hospital F", 15),
                new KeyValuePair<string, int>("Hospital G", -20),
                new KeyValuePair<string, int>("Hospital H", -15),
                new KeyValuePair<string, int>("Hospital I", 8),
                new KeyValuePair<string, int>("Hospital J", -8),
                new KeyValuePair<string, int>("Hospital K", 15),
                new KeyValuePair<string, int>("Hospital L", -20),
                new KeyValuePair<string, int>("Hospital M", -15),
                new KeyValuePair<string, int>("Others", 30) };

        public static void SetPieChart(Chart chart, List<string> colorShades, List<KeyValuePair<string, int>> data)
        {
            System.Windows.Controls.DataVisualization.ResourceDictionaryCollection pieSeriesPalette = new System.Windows.Controls.DataVisualization.ResourceDictionaryCollection();
            System.Windows.ResourceDictionary pieDataPointStyles = new ResourceDictionary();
            Style stylePie = new Style(typeof(PieDataPoint));

            for (int i = 0; i < colorShades.Count; i++)
            {
                Color color = (Color)ColorConverter.ConvertFromString(colorShades[i]);

                Brush currentBrush = new SolidColorBrush(color);
                stylePie.Setters.Add(new Setter(PieDataPoint.BackgroundProperty, currentBrush));
                pieDataPointStyles.Add("DataPointStyle" + i, stylePie);
                pieSeriesPalette.Add(pieDataPointStyles);
            }

            chart.DataContext = data;

            //chart.Palette = pieSeriesPalette;
            chart.Palette = MakePalette(colorShades);
        }

        public static void SetBarChart(Chart chart, List<List<KeyValuePair<string, int>>> data)
        {
            chart.DataContext = data;
        }

        public static System.Collections.ObjectModel.Collection<ResourceDictionary> MakePalette(List<string> redShades)
        {
            System.Collections.ObjectModel.Collection<ResourceDictionary> palette = new System.Collections.ObjectModel.Collection<ResourceDictionary>();
            foreach (string item in redShades)
            {
                ResourceDictionary rd = new ResourceDictionary();
                Style style = new Style(typeof(Control));
                SolidColorBrush brush = null;
                brush = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(item)
                    );

                style.Setters.Add(new Setter() { Property = Control.BackgroundProperty, Value = brush });
                rd.Add("DataPointStyle", style);
                palette.Add(rd);
            }
            return palette;
        }


    }
}
