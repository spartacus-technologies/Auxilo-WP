﻿#pragma checksum "D:\Spartacus\Koodit\Auxilo-wp\Auxilo_WP\Auxilo_WP\Controls\MainViewUserControls\UcDevices.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8C2F53F17519FFFD22A42E2842376F32"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Auxilo_WP {
    
    
    public partial class UcDevices : System.Windows.Controls.UserControl {
        
        internal System.Windows.Controls.Grid Devices;
        
        internal System.Windows.Controls.ListBox ListBoxDevices;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Auxilo_WP;component/Controls/MainViewUserControls/UcDevices.xaml", System.UriKind.Relative));
            this.Devices = ((System.Windows.Controls.Grid)(this.FindName("Devices")));
            this.ListBoxDevices = ((System.Windows.Controls.ListBox)(this.FindName("ListBoxDevices")));
        }
    }
}

