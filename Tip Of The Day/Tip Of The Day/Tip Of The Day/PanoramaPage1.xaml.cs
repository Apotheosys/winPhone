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
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Media.Imaging;

namespace Tip_Of_The_Day
{
    public partial class PanoramaPage1 : PhoneApplicationPage
    {
        public PanoramaPage1()
        {
            InitializeComponent();


            string linkUri = "http://how-to-tarot.com/WebSite2/foo/Tip%281%29";
            WebClient client = new WebClient();
            client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(DownloadStringAsync);
            client.DownloadStringAsync(new Uri(linkUri));

        }

        private static Grid setGrid()
        {
            Grid ContentPanel = new Grid();

            for (int i = 0; i < 9; i++)
            {
                RowDefinition rDef = new RowDefinition();
                rDef.Height = new GridLength(0.5, GridUnitType.Star);

                ContentPanel.RowDefinitions.Add(rDef);

            }

            return ContentPanel;
        }

        private TextBlock getTextBlock(int s, string text, Style style)
        {

            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Style = style;
            textBlock.SetValue(Grid.RowProperty, s);

            return textBlock;

        }

        private void DownloadStringAsync(Object sender, DownloadStringCompletedEventArgs e)
        {


            Panorama pan = new Panorama();
            
            
            

            if (!e.Cancelled && e.Error == null)
            {


                string xmlString = (string)e.Result;
                XDocument xml = XDocument.Parse(xmlString);
                XNamespace envelopeNs = xml.Root.GetDefaultNamespace();

                var parseTips = from JedanTip in xml.Descendants() where JedanTip.Name.LocalName.Equals("JedanTip") select JedanTip;

                int z = parseTips.Count();

                int count = 0;

                foreach (var parseTip in parseTips)
                {
                    count++;
                    XNode sadrzajNode = parseTip.FirstNode;

                    string abt = getValueFromNode(ref sadrzajNode);
                    string dbtn = getValueFromNode(ref sadrzajNode);
                    string izb = getValueFromNode(ref sadrzajNode);
                    string kfc = getValueFromNode(ref sadrzajNode);
                    string kld = getValueFromNode(ref sadrzajNode);
                    string lg = getValueFromNode(ref sadrzajNode);
                    string objv = getValueFromNode(ref sadrzajNode);
                    string pctk = getValueFromNode(ref sadrzajNode);
                    string prmtr = getValueFromNode(ref sadrzajNode);
                    string sprt = getValueFromNode(ref sadrzajNode);
                    string tm1 = getValueFromNode(ref sadrzajNode);
                    string tm2 = getValueFromNode(ref sadrzajNode);
                    string vrst = getValueFromNode(ref sadrzajNode);
                    string zmlj = getValueFromNode(ref sadrzajNode);
                    string aKfc = getValueFromNode(ref sadrzajNode);
                    string mKfc = getValueFromNode(ref sadrzajNode);

                    DateTime dt = DateTime.Parse(pctk);

                    PanoramaItem panoramaCtrlItem = SetPanoramaItem(dt);

                    Style s = (Style)Application.Current.Resources["PhoneTextNormalStyle"];


                    Grid ContentPanel = setGrid();

                    ContentPanel.Children.Add(getTextBlock(0, sprt, s));
                    ContentPanel.Children.Add(getTextBlock(1, zmlj, s));
                    ContentPanel.Children.Add(getTextBlock(2, lg, s));
                    ContentPanel.Children.Add(getTextBlock(3, tm1 + " v " + tm2 , s));

                    if (prmtr == null || prmtr.Length == 0)
                    {
                        ContentPanel.Children.Add(getTextBlock(4, vrst + ", Pick: " + izb, s));                      
                    }
                    else
                    {
                        ContentPanel.Children.Add(getTextBlock(4, vrst + ", " + prmtr.Trim() + ", Pick: " + izb, s)); 
                    }
                    ContentPanel.Children.Add(getTextBlock(5, "Odds: " + kfc, s));
                    ContentPanel.Children.Add(getTextBlock(6, "Min odds: " + mKfc + " " + "Avg odds: " + aKfc, s));
                    ContentPanel.Children.Add(getTextBlock(7, "Bookmaker: " + kld, s));
                    ContentPanel.Children.Add(GetImage(7, kld));
                    ContentPanel.Children.Add(getTextBlock(8, abt, s));
                    

                    panoramaCtrlItem.Content = ContentPanel;


                    pan.Items.Add(panoramaCtrlItem);
                }


                string tipsTitle = "";
                if (count == 1)
                { 
                    tipsTitle = "Tip Of The Day";
                }
                else if (count == 0)
                {
                    tipsTitle = "No active tips yet...";
                }
                else
                {
                    tipsTitle = count + " Tips Of The Day";
                }
               

                pan.Title = tipsTitle;
                pan.TitleTemplate = (DataTemplate)Application.Current.Resources["MyPanoramaTitleTemplate"];
                this.LayoutRoot.Children.Add(pan);
            }
            else
            {
                //neko upozorenje
            }
        }

        private static Image GetImage(int s, string kld)
        {
            string prefiks = "slike/";
            string slika = "";

            if (kld.Equals("William Hill"))
            {
                slika = "williamhill.png";
            }
            else if (kld.Equals("Bet Victor"))
            {
                slika = "betvictor.png";
            }
            else if (kld.Equals("Bet Victor"))
            {
                slika = "betvictor.png";
            }
            else 
            {
                slika = kld.ToLower() + ".png";
            }


            Image image = new Image();
            image.Source = new BitmapImage(new Uri(prefiks+slika, UriKind.Relative));
            image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            image.VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
            image.SetValue(Image.HeightProperty, (double)20);
            image.SetValue(Image.WidthProperty, (double)89);
            image.SetValue(Grid.RowProperty, s);
            image.Margin = new Thickness(15, 1, 1, 22);
            return image;
        }

        private PanoramaItem SetPanoramaItem(DateTime dt)
        {
            PanoramaItem panoramaCtrlItem = new PanoramaItem();
            panoramaCtrlItem.Header = dt.ToLocalTime().ToShortDateString() + " " + dt.ToLocalTime().ToShortTimeString();
            panoramaCtrlItem.HeaderTemplate = (DataTemplate)Application.Current.Resources["MyPanoramaItemHeaderTemplate"];
            panoramaCtrlItem.Orientation = System.Windows.Controls.Orientation.Horizontal;
            panoramaCtrlItem.Margin = new Thickness(25, 0, 0, 0);
            return panoramaCtrlItem;
        }

        public string getValueFromNode(ref XNode x)
        {
            string temp = (x as XElement).Value; 
            x = x.NextNode;
            return temp;
        }
    }
}
