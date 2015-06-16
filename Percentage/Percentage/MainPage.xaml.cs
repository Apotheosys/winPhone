using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Percentage
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {

            InitializeComponent();
        }

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            panorama_SelectionChanged(null, null);
        }
        private void prvi2_KeyUp(object sender, KeyEventArgs e)
        {
            mask((TextBox)sender, e);
            calc(prvi2, drugi2, rez2, 1);

        }
        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            mask((TextBox)sender, e);
            calc(prvi2, drugi2, rez2, 1);
        }
        private void prvi1_KeyUp(object sender, KeyEventArgs e)
        {
            mask((TextBox)sender, e);
            calc(prvi1, drugi1, rez1, 0);
        }
        private void drugi1_KeyUp(object sender, KeyEventArgs e)
        {
            mask((TextBox)sender, e);
            calc(prvi1, drugi1, rez1, 0);
        }
        private void prvi3_KeyUp(object sender, KeyEventArgs e)
        {
            mask((TextBox)sender, e);
            calc(prvi3, drugi3, rez3, 2);
        }
        private void drugi3_KeyUp(object sender, KeyEventArgs e)
        {
            mask((TextBox)sender, e);
            calc(prvi3, drugi3, rez3, 2);
        }







        private void mask(TextBox tb, KeyEventArgs e)
        {
            string[] invalidCharacters = { "*", "#", ",", "(", ")", "x", "-", "+", " ", "@" };

            for (int i = 0; i < invalidCharacters.Length; i++)
            {
                tb.Text = tb.Text.Replace(invalidCharacters[i], "");
            }

            tb.SelectionStart = tb.Text.Length;
        }

        private void panorama_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (panorama.SelectedIndex == 0)
            {
                prvi1.Focus();
            }
            if (panorama.SelectedIndex == 1)
            {
                prvi2.Focus();
            }
            if (panorama.SelectedIndex == 2)
            {
                prvi3.Focus();
            }
        }

        private void calc(TextBox prvi2, TextBox drugi2, TextBox rez1, int mode)
        {
            double prvi = 0.0;
            double drugi = 0.0;
            if (Double.TryParse(prvi2.Text, out prvi) && Double.TryParse(drugi2.Text, out drugi))
            {
                if (mode == 0)
                {
                    rez1.Text = ((prvi / drugi) * 100).ToString();
                    red1.Text = ""; red2.Text = ""; red3.Text = "";
                }
                if (mode == 1)
                {
                    rez2.Text = ((prvi * drugi) / 100).ToString();
                    red1.Text = ""; red2.Text = ""; red3.Text = "";

                }
                if (mode == 2)
                {
                    rez3.Text = ((prvi / drugi) * 100).ToString();
                    red1.Text = ""; red2.Text = ""; red3.Text = "";

                }
            }
            else
            {
                if (mode == 0)
                {
                    red1.Text = "Not calculated. Enter only valid numbers.";
                }
                if (mode == 1)
                {
                    red2.Text = "Not calculated. Enter only valid numbers.";
                }
                if (mode == 2)
                {
                    red2.Text = "Not calculated. Enter only valid numbers.";
                }
            
            }

        }
    }
}
