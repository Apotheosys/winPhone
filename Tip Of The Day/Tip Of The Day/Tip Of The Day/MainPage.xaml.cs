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

namespace Tip_Of_The_Day
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
		
		protected void funkcijaTip(object sender, EventArgs e)
		{
            NavigationService.Navigate(new Uri("/PanoramaPage1.xaml", UriKind.Relative));
		}
		
		protected void funkcijaLast(object sender, EventArgs e)
		{
			
		}
		
		protected void funkcijaStatistika(object sender, EventArgs e)
		{

		}
    }
}
