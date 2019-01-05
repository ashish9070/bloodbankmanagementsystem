using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BMS.PresentationLayerWpf.CustomControls
{
    public class HintedTextBox : TextBox
    {
        string placeHolderText;
        bool foundPlaceHolderText = false;

        public delegate void QueryEventHandler(string query);

        public event QueryEventHandler ExecuteQuery;

        public void QueryProccess(string query)
        {
            Debug.WriteLine(query);

            // Your logic
            if (ExecuteQuery != null)
            {
                ExecuteQuery(query);
            }

        }

        public string PlaceHolderText 
        {
            get { return placeHolderText; }
            set
            {
                placeHolderText = value;
                proccessPlaceHolderText();
            }
        }

        private void proccessPlaceHolderText()
        {
            if (foundPlaceHolderText == false && string.IsNullOrEmpty(this.Text))
            {
                this.Text = PlaceHolderText;
                this.Foreground = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                this.FontStyle = FontStyles.Italic;
                foundPlaceHolderText = true;
            }
        }

        private void removePlaceHolderText()
        {
            if(foundPlaceHolderText == true)
            {
                this.Text = "";
                this.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.FontStyle = FontStyles.Normal;
                foundPlaceHolderText = false;
            }
        }

        

        public HintedTextBox()
        {
            this.GotFocus += HintedTextBox_GotFocus;
            this.LostFocus += HintedTextBox_LostFocus;
            //this.KeyUp += new KeyEventHandler (HintedTextBox_KeyUp);
            this.PreviewKeyDown += HintedTextBox_PreviewKeyDown;
            //this.AcceptsReturn = true;
            this.MouseEnter += HintedTextBox_MouseEnter;
            this.MouseLeave += HintedTextBox_MouseLeave;
        }

        void HintedTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            proccessPlaceHolderText();
        }

        void HintedTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            removePlaceHolderText();
            if (this.Text.Trim() == this.PlaceHolderText)
            {
                this.Text = "";
                this.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                this.FontStyle = FontStyles.Normal;
                foundPlaceHolderText = false;
            }
        }

        #region User Events
        void HintedTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(this.Text.Trim() == this.PlaceHolderText)
            {
                this.Text = "";
            }

            if(e.Key == Key.Return)
            {
                QueryProccess(this.Text);
                this.Text = "";
                e.Handled = true;
                Keyboard.ClearFocus();
                
                proccessPlaceHolderText();
                this.Text = PlaceHolderText;
                this.Foreground = new SolidColorBrush(Color.FromRgb(100, 100, 100));
                this.FontStyle = FontStyles.Italic;
                foundPlaceHolderText = true;
            }
        }


        void HintedTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            proccessPlaceHolderText();
        }

        void HintedTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            removePlaceHolderText();
        }

        #endregion
    }
}
