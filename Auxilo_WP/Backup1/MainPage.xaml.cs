using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
//using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Media.Imaging;
using auxilo;
using System.Windows.Input;

namespace Auxilo_WP
{
    public partial class MainPage : PhoneApplicationPage
    {
        private BitmapImage homeImage;
        private BitmapImage homeImageSelected;
        private BitmapImage devicesImage;
        private BitmapImage devicesImageSelected;
        private BitmapImage infoImage;
        private BitmapImage infoImageSelected;
        private BitmapImage videoImage;
        private BitmapImage videoImageSelected;

        public static MessageWrapper communication;

        public static DeviceList allDevices;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            this.homeImage = new BitmapImage(new Uri(@"Images/navi_home_inactive.png", UriKind.Relative));

            this.homeImageSelected = new BitmapImage(new Uri(@"Images/navi_home_active.png", UriKind.Relative));

            this.devicesImage = new BitmapImage(new Uri(@"Images/navi_nexxa_inactive.png", UriKind.Relative));

            this.devicesImageSelected = new BitmapImage(new Uri(@"Images/navi_nexxa_active.png", UriKind.Relative));

            this.infoImage = new BitmapImage(new Uri(@"Images/navi_temp_inactive.png", UriKind.Relative));

            this.infoImageSelected = new BitmapImage(new Uri(@"Images/navi_temp_active.png", UriKind.Relative));

            this.videoImage = new BitmapImage(new Uri(@"Images/navi_cam_inactive.png", UriKind.Relative));

            this.videoImageSelected = new BitmapImage(new Uri(@"Images/navi_cam_active.png", UriKind.Relative));

            communication = new MessageWrapper(SettingsPage.ServerIPAddress, SettingsPage.PortNumber);

            TestTextBlock.Text = communication.Initialize();

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);

        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Load data for the ViewModel Items
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            
            /*
            // Get devices and initialize the position of the switches
            DoInitialization();

            // Get sensor values
            GetSensorValues();
            */
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void ImageSettings_Tap(object sender, GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/SettingsPage.xaml", UriKind.Relative));
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void ImageHome_Tap(object sender, GestureEventArgs e)
        {
            PanoramaCarousel.DefaultItem = PanoramaCarousel.Items[0];
            ImageHome.Source = homeImageSelected;
            ImageDevices.Source = devicesImage;
            ImageInfo.Source = infoImage;
            ImageVideo.Source = videoImage;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void ImageDevices_Tap(object sender, GestureEventArgs e)
        {
            PanoramaCarousel.DefaultItem = PanoramaCarousel.Items[1];
            ImageHome.Source = homeImage;
            ImageDevices.Source = devicesImageSelected;
            ImageInfo.Source = infoImage;
            ImageVideo.Source = videoImage;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void ImageInfo_Tap(object sender, GestureEventArgs e)
        {
            PanoramaCarousel.DefaultItem = PanoramaCarousel.Items[2];
            ImageHome.Source = homeImage;
            ImageDevices.Source = devicesImage;
            ImageInfo.Source = infoImageSelected;
            ImageVideo.Source = videoImage;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void ImageVideo_Tap(object sender, GestureEventArgs e)
        {
            PanoramaCarousel.DefaultItem = PanoramaCarousel.Items[3];
            ImageHome.Source = homeImage;
            ImageDevices.Source = devicesImage;
            ImageInfo.Source = infoImage;
            ImageVideo.Source = videoImageSelected;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void PanoramaCarousel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PanoramaCarousel.SelectedIndex == 0)
            {
                ImageHome.Source = homeImageSelected;
                ImageDevices.Source = devicesImage;
                ImageInfo.Source = infoImage;
                ImageVideo.Source = videoImage;
            }
            else if (PanoramaCarousel.SelectedIndex == 1)
            {
                ImageHome.Source = homeImage;
                ImageDevices.Source = devicesImageSelected;
                ImageInfo.Source = infoImage;
                ImageVideo.Source = videoImage;
            }
            else if (PanoramaCarousel.SelectedIndex == 2)
            {
                ImageHome.Source = homeImage;
                ImageDevices.Source = devicesImage;
                ImageInfo.Source = infoImageSelected;
                ImageVideo.Source = videoImage;
            }
            else if (PanoramaCarousel.SelectedIndex == 3)
            {
                ImageHome.Source = homeImage;
                ImageDevices.Source = devicesImage;
                ImageInfo.Source = infoImage;
                ImageVideo.Source = videoImageSelected;
            }
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Testcases part 1
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void DoInitialization()
        {
            try
            {
                // Create HelloRequest message
                byte[] messageHelloRequest = CreateMessage.CreateHelloRequest(SettingsPage.CustomerID, SettingsPage.DeviceID);

                // Send the HelloRequest message
                TestTextBlock.Text = communication.Send(messageHelloRequest);

                // Receive the DeviceList Parse the protobuf information from the message
                allDevices = ReceiveMessage.ParseDeviceList(communication.Receive());

                byte[] messageAck = CreateMessage.CreateAckMessage();
                communication.Send(messageAck);

                // List all the devices
                if (!App.ViewModel.IsDeviceDataLoaded)
                {
                    App.ViewModel.LoadDeviceData(allDevices);
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error!", MessageBoxButton.OK);
            }

        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Testcases part 2
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void ButtonGetDeviceStatuses_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (DeviceInfo device in allDevices.DeviceList_List)
                {
                    if (device.Type == deviceType.deviceNexa)
                    {
                        // Send message to get device states from the box
                        byte[] messageDeviceState = CreateMessage.CreateDeviceCommandStateMessage(device.BoxID, device.DeviceID);
                        communication.Send(messageDeviceState);
                        DeviceStatus deviceStatus = ReceiveMessage.ParseDeviceStatus(communication.Receive());
                        
                        // Find the correct control from the view that the information belongs to
                        CustomSlideControl slider = FindName(device.DeviceID) as CustomSlideControl;

                        switch (deviceStatus.Status)
                        {
                            case deviceState.on:
                                slider.IsOn = true;
                                break;

                            case deviceState.off:
                                slider.IsOn = false;
                                break;

                            case deviceState.unknown:
                                slider.IsEnabled = false;
                                break;

                            default:
                                slider.IsEnabled = false;
                                break;
                        }
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error!", MessageBoxButton.OK);
            }
        }

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Testcases part 3
        /// </summary>
        /// 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        //////////////////////////////////////////////////////////////
        private void GetSensorValues()
        {
            try
            {
                string boxID = allDevices.DeviceList_List[0].BoxID;
                List<string> sensorIDs = new List<string>();

                foreach (DeviceInfo device in allDevices.DeviceList_List)
                {
                    if (device.Type == deviceType.sensorTemp /*|| deviceType.sensorPIR*/)
                    {
                        sensorIDs.Add(device.DeviceID);
                    }
                }

                // Send message to get sensor states from the box
                byte[] messageSensorState = CreateMessage.CreateQueryMessageListMessage(boxID, sensorIDs);
                communication.Send(messageSensorState);
                SensDataList sensorInfoList = ReceiveMessage.ParseSensorInfo(communication.Receive());

                foreach (DataMessage dataMessage in sensorInfoList.SensorDataList)
                {
                    if (dataMessage.HardwareID.Equals(sensorIDs[0]))
                    {
                        decimal temp = Math.Round((decimal)dataMessage.Data, 1);
                        TextBlockTemperature.Text = temp + "˚C";
                        break;
                    }
                }

                //GraphControl.ParseSensorListData(sensorInfoList);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "Error!", MessageBoxButton.OK);
            }
        }
    }
}