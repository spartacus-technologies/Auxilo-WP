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
using auxilo;

namespace Auxilo_WP
{
    public partial class GraphControl : UserControl
    {
        public GraphControl()
        {
            InitializeComponent();
        }

        public static void ParseSensorListData(SensDataList info)
        {
            foreach (DataMessage dataMessage in info.SensorDataList)
            {
                
            }
        }
    }
}
