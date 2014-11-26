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
using System;

namespace Auxilo_WP
{

    public class DeviceViewModel : INotifyPropertyChanged
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



        private string deviceName;

        // This property is used in the view to display the name of the device using Binding.
        public string DeviceName
        {
            get
            {
                return deviceName;
            }
            set
            {
                if (value != deviceName)
                {
                    deviceName = value;
                    NotifyPropertyChanged("DeviceName");
                }
            }
        }


        private string deviceID;

        // This property is used to identificate the switch's deviceID.
        public string DeviceID
        {
            get
            {
                return deviceID;
            }
            set
            {
                if (value != deviceID)
                {
                    deviceID = value;
                    // No need for property changed notification, since this value is not currently displayed
                }
            }
        }


        private string boxID;

        // This property is used to identificate the switch's devices boxID.
        public string BoxID
        {
            get
            {
                return boxID;
            }
            set
            {
                if (value != boxID)
                {
                    boxID = value;
                    // No need for property changed notification, since this value is not currently displayed
                }
            }
        }
    }
}