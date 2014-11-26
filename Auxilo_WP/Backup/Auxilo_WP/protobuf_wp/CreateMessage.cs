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
using auxilo;
using System.IO;
using System.Collections.Generic;

namespace Auxilo_WP
{
    public class CreateMessage
    {
        const int HELLO_REQUEST = 0;
		const int DEVICE_LIST = 0;
     	const int NORMAL = 1;
        const int ACK = 10; // (Note: Send only header)
        const int HEART_BEAT_ACK = 11; // (Server sends this to find out phone status. Response done with heart beat)
     	const int TERMINATE = 1000; // (Makes the server to remove this client from the room)

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Creates a Hello Request protobuf message including the necessary header.
        /// </summary>
        /// 
        /// <param name="customerID">Customers ID</param>
        /// <param name="deviceID">This phones apps ID</param>
        /// <param name="isBox">Is the connecting device box or not. In this software it is never a box</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static byte[] CreateHelloRequest(string customerID, string deviceID, bool isBox = false)
        {
            byte[] dataPart;

            // Build the protobuf message
            HelloRequest.Builder newHelloRequest = HelloRequest.CreateBuilder();

            newHelloRequest.SetCustomerID(customerID);
            newHelloRequest.SetDeviceName(deviceID);
            newHelloRequest.SetIsBox(isBox);

            HelloRequest helloRequest = newHelloRequest.Build();

            // Create byte array of the protobuf message
            using (MemoryStream stream = new MemoryStream())
            {
                helloRequest.WriteTo(stream);
                dataPart = stream.ToArray();
            }

            // Create the header
            byte[] headerPart = CreateHeader(dataPart.Length, HELLO_REQUEST);

            // Create the entire message
            byte[] message = new byte[headerPart.Length + dataPart.Length];

            headerPart.CopyTo(message, 0); //Header bytes 0-7
            dataPart.CopyTo(message, 8); //Body bytes 8-...

            return message;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Creates a device query protobuf message including the necessary header.
        /// </summary>
        /// 
        /// <param name="boxID">box's ID</param>
        /// <param name="deviceID">Controlled device's ID</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static byte[] CreateAckMessage()
        {
            // Create the header, no body needed.
            byte[] headerPart = CreateHeader(0, ACK);

            return headerPart;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Creates a device query protobuf message including the necessary header.
        /// </summary>
        /// 
        /// <param name="boxID">box's ID</param>
        /// <param name="deviceID">Controlled device's ID</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static byte[] CreateDeviceCommandStateMessage(string boxID, string deviceID)
        {
            byte[] dataPart;

            // Build the protobuf message
            Message.Builder newMessage = Message.CreateBuilder();
            DeviceCommand.Builder newDeviceCommand = DeviceCommand.CreateBuilder();

            newDeviceCommand.DeviceID = deviceID;
            DeviceCommand deviceCommand = newDeviceCommand.Build();

            newMessage.ReceiverDeviceName = boxID;
            newMessage.SenderDeviceName = SettingsPage.DeviceID;
            newMessage.DeviceCommand = deviceCommand;
            Message dataMessage = newMessage.Build();

            // Create byte array of the protobuf message
            using (MemoryStream stream = new MemoryStream())
            {
                dataMessage.WriteTo(stream);
                dataPart = stream.ToArray();
            }

            // Create the header
            byte[] headerPart = CreateHeader(dataPart.Length, NORMAL);

            // Create the entire message
            byte[] message = new byte[headerPart.Length + dataPart.Length];

            headerPart.CopyTo(message, 0); //Header bytes 0-7
            dataPart.CopyTo(message, 8); //Body bytes 8-...

            return message;
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Creates a sensor info query protobuf message including the necessary header.
        /// </summary>
        /// 
        /// <param name="boxID">box's ID</param>
        /// <param name="deviceID">Controlled device's ID</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static byte[] CreateQueryMessageListMessage(string boxID, List<string> sensorIDs)
        {
            byte[] dataPart;

            // Build the protobuf message
            Message.Builder newMessage = Message.CreateBuilder();
            QueryMessageList.Builder newQueryMessageList = QueryMessageList.CreateBuilder();
            
            foreach (string sensorID in sensorIDs)
            {
                QueryMessage.Builder newQueryMessage = QueryMessage.CreateBuilder();
                newQueryMessage.SensorID = sensorID;
                QueryMessage queryMessage = newQueryMessage.Build();
                newQueryMessageList.QueryList.Add(queryMessage);
            }

            QueryMessageList queryMessageList = newQueryMessageList.Build();

            newMessage.ReceiverDeviceName = boxID;
            newMessage.SenderDeviceName = SettingsPage.DeviceID;
            newMessage.Qry = queryMessageList;
            Message dataMessage = newMessage.Build();

            // Create byte array of the protobuf message
            using (MemoryStream stream = new MemoryStream())
            {
                dataMessage.WriteTo(stream);
                dataPart = stream.ToArray();
            }

            // Create the header
            byte[] headerPart = CreateHeader(dataPart.Length, NORMAL);

            // Create the entire message
            byte[] message = new byte[headerPart.Length + dataPart.Length];

            headerPart.CopyTo(message, 0); //Header bytes 0-7
            dataPart.CopyTo(message, 8); //Body bytes 8-...

            return message;
        }

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Creates a device query protobuf message including the necessary header.
        /// </summary>
        /// 
        /// <param name="boxID">box's ID</param>
        /// <param name="deviceID">Controlled device's ID</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static byte[] CreateDeviceCommandSetMessage(string boxID, string deviceID, deviceState onOff)
        {
            byte[] dataPart;

            // Build the protobuf message
            Message.Builder newMessage = Message.CreateBuilder();
            DeviceCommand.Builder newDeviceCommand = DeviceCommand.CreateBuilder();

            newDeviceCommand.DeviceID = deviceID;
            newDeviceCommand.SetStatus = onOff;
            DeviceCommand deviceCommand = newDeviceCommand.Build();

            newMessage.ReceiverDeviceName = boxID;
            newMessage.SenderDeviceName = SettingsPage.DeviceID;
            newMessage.DeviceCommand = deviceCommand;
            Message dataMessage = newMessage.Build();

            // Create byte array of the protobuf message
            using (MemoryStream stream = new MemoryStream())
            {
                dataMessage.WriteTo(stream);
                dataPart = stream.ToArray();
            }

            // Create the header
            byte[] headerPart = CreateHeader(dataPart.Length, NORMAL);

            // Create the entire message
            byte[] message = new byte[headerPart.Length + dataPart.Length];

            headerPart.CopyTo(message, 0); //Header bytes 0-7
            dataPart.CopyTo(message, 8); //Body bytes 8-...

            return message;
        }

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Creates a header to the message
        /// </summary>
        /// 
        /// <param name="length">The length of actual data. This figure is in the header</param>
        /// <param name="type">What kind of message are we sending</param>
        /// 
        //////////////////////////////////////////////////////////////
        private static byte[] CreateHeader(int length, int type)
        {
            byte[] lengthSection = BitConverter.GetBytes((Int32)length);
            Array.Reverse(lengthSection, 0, lengthSection.Length); // Make Big-Endian representation

            byte[] typeSection = BitConverter.GetBytes((Int32)type);
            Array.Reverse(typeSection, 0, typeSection.Length); // Make Big-Endian representation

            byte[] returnable = new byte[lengthSection.Length + typeSection.Length];

            lengthSection.CopyTo(returnable, 0); //Length goes in bytes 0-3
            typeSection.CopyTo(returnable, 4); //Type goes to bytes 4-7

            return returnable;
        }
    }
}
