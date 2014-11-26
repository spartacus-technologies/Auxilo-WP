using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using auxilo;


namespace Auxilo_WP
{
    public class MainViewModel : INotifyPropertyChanged
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
        /// A collection for NotificationViewModel and DeviceViewModel objects.
        /// </summary>
        public ObservableCollection<NotificationViewModel> Notifications { get; private set; }
        public ObservableCollection<DeviceViewModel> Devices { get; private set; }

        /// <summary>
        /// Property for info if the Notifications are loaded
        /// </summary>
        public bool IsNotificationDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Property for info if the Devices are loaded
        /// </summary>
        public bool IsDeviceDataLoaded
        {
            get;
            private set;
        }

        ///////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Constructor.
        /// </summary>
        /// 
        ////////////////////////////////////////////////////////////
        public MainViewModel()
        {
            this.Notifications = new ObservableCollection<NotificationViewModel>();
            this.Devices = new ObservableCollection<DeviceViewModel>();
        }


        ////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        /// 
        ////////////////////////////////////////////////////////////
        public void LoadNotificationData()
        {
            // Sample data; replace with real data
            this.Notifications.Add(new NotificationViewModel() { LineOne = "Alarm!", LineTwo = "Dog is barking! Check the camera!" });
            this.Notifications.Add(new NotificationViewModel() { LineOne = "Notification:", LineTwo = "Room temperature raised to 24C degrees." });
            this.Notifications.Add(new NotificationViewModel() { LineOne = "Greetings!", LineTwo = "Good morning! Did you sleep well?" });
            this.Notifications.Add(new NotificationViewModel() { LineOne = "Greetings!", LineTwo = "Good night! How about a lullaby?" });
            this.Notifications.Add(new NotificationViewModel() { LineOne = "Alarm!", LineTwo = "Temperature dropped to 20C degrees? Would you like to turn on the heater?" });

            this.IsNotificationDataLoaded = true;
        }

        ////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Loads device data on the screen.
        /// </summary>
        /// 
        /// <param name="devices">List of devices gotten from server</param>
        /// 
        ////////////////////////////////////////////////////////////
        public void LoadDeviceData(DeviceList devices)
        {
            //this.Devices.Add(new ItemViewModel() { LineOne = "Device"});
            foreach (DeviceInfo device in devices.DeviceList_List)
            {
                if (device.Type == deviceType.deviceNexa)
                {
                    this.Devices.Add(new DeviceViewModel() { DeviceName = device.Description, 
                        DeviceID = device.DeviceID, BoxID = device.BoxID });

                }
            }
            this.IsDeviceDataLoaded = true;
        }
    }
}