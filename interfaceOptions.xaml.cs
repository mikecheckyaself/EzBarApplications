using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MySql.Data.MySqlClient;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EzBarApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class interfaceOptions : Page
    {
        public interfaceOptions()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MixedDrinks));                   
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MySqlConnection connection;
            connection = App.ConnectionFactory.Create();
            if (connection.Ping() == true)
            {
                App.isConnected = "Connected!";
                currentStatus.Text = App.isConnected;
            }
            else
            {
                App.isConnected = "Not Connected!";
                currentStatus.Text = App.isConnected;
            }


            base.OnNavigatedTo(e);
        }
    }
}
