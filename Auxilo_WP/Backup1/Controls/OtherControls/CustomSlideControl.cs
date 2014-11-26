using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using auxilo;

namespace Auxilo_WP
{
    public class CustomSlideControl : Button, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged settings

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        /// <summary>
        /// Maintains info if the device is set on or off.
        /// </summary>
        public bool IsOn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private Grid grid;

        /// <summary>
        /// 
        /// </summary>
        private ResourceDictionary resourceDict;

        /// <summary>
        /// 
        /// </summary>
        private Storyboard sBoard;


        private string deviceId = "";
        public string DeviceId
        {
            get 
            { 
                return deviceId; 
            }

            set 
            { 
                deviceId = value;
            }
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Constructor.
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        public CustomSlideControl()
        {
            IsOn = false;
            this.DefaultStyleKey = typeof(CustomSlideControl);
            this.Click += new RoutedEventHandler(CustomSlideControl_Click);
        }

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Clickhandler for a device switch control
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        void CustomSlideControl_Click(object sender, RoutedEventArgs e)
        {
            if (grid == null)
            {
                Grid outerGrid = (Grid)VisualTreeHelper.GetChild((Button)sender, 0);
                grid = (Grid)VisualTreeHelper.GetChild(outerGrid, 0);
                resourceDict = grid.Resources;
            }

            // Get the device id through UI components. If someone has a better way to do this, by all means!
            Grid parentGrid = (Grid)VisualTreeHelper.GetParent((Button)sender);
            ContentPresenter content = (ContentPresenter)VisualTreeHelper.GetParent(parentGrid);
            DeviceViewModel testDevice = (DeviceViewModel)content.DataContext;

            if (!IsOn) // Put device on
            {
                sBoard = resourceDict["mainAnimator"] as Storyboard;
                sBoard.Begin();
                IsOn = true;

                // Send message to turn specific device on
                byte[] messageDeviceState = CreateMessage.CreateDeviceCommandSetMessage(testDevice.BoxID, testDevice.DeviceID, deviceState.on);
                MainPage.communication.Send(messageDeviceState);
                // Should we receive something...??? DeviceStatus deviceStatus = ReceiveMessage.ParseDeviceStatus(MainPage.communication.Receive());
            }
            else // Put device off
            {
                sBoard = resourceDict["reverseAnimator"] as Storyboard;
                sBoard.Begin();
                IsOn = false;

                // Send message to turn specific device off
                byte[] messageDeviceState = CreateMessage.CreateDeviceCommandSetMessage(testDevice.BoxID, testDevice.DeviceID, deviceState.off);
                MainPage.communication.Send(messageDeviceState);
                // Should we receive something...??? DeviceStatus deviceStatus = ReceiveMessage.ParseDeviceStatus(MainPage.communication.Receive());            
            }
        }
    }
}
