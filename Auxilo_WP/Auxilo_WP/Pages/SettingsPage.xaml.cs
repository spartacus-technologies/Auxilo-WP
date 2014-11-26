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
using System.Windows.Navigation;

namespace Auxilo_WP
{
    public partial class SettingsPage : PhoneApplicationPage
    {

        private static string customerID = "eetu1993";
        private static string deviceID = "WPTest";
        private static string serverIPAddress = "95.85.11.190";
        private static int portNumber = 8081;

        public static string CustomerID
        {
            get
            {
                return customerID;
            }
        }

        public static string DeviceID
        {
            get
            {
                return deviceID;
            }
        }

        public static string ServerIPAddress
        {
            get
            {
                return serverIPAddress;
            }
        }

        public static int PortNumber
        {
            get
            {
                return portNumber;
            }
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Constructor.
        /// </summary>
        /// 
        //////////////////////////////////////////////////////////////
        public SettingsPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            TextBoxCustomerId.Text = CustomerID;
            TextBoxDeviceId.Text = DeviceID;
            TextBoxServerIPAddress.Text = ServerIPAddress;
            TextBoxPortNumber.Text = PortNumber.ToString();

            // Other code you would have otherwise run in a parameterised constructor
        }

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Click handler for saving the values.
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            customerID = TextBoxCustomerId.Text;
            deviceID = TextBoxDeviceId.Text;
            serverIPAddress = TextBoxServerIPAddress.Text;
            portNumber = int.Parse(TextBoxPortNumber.Text);

            NavigationService.GoBack();
        }
    }
}