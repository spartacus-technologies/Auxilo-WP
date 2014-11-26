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

namespace Auxilo_WP
{
    public partial class UcNotifications : UserControl
    {
        public UcNotifications()
        {
            // Load Notifications
            if (!App.ViewModel.IsNotificationDataLoaded)
            {
                App.ViewModel.LoadNotificationData();
            }

            InitializeComponent();
        }
    }
}
