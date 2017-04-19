﻿using System;
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
using System.Threading;
using Windows.System.Threading;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EzBarApplication
{
   // delegate void UI_Interface();
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    
    public partial class MainPage : Page
    {

        public MainPage()
        {

            var frame = Window.Current.Content as Frame;
            
            this.InitializeComponent();
            App.ConnectionFactory.Create();
        }


    
        private void TempButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(interfaceOptions));
            EzBarLogo.Visibility = Visibility.Collapsed;
        }
    }
}
