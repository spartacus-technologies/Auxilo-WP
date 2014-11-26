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

namespace Auxilo_WP
{
    public class ReceiveMessage
    {
        const int HELLO_REQUEST = 0;
        const int DEVICE_LIST = 0;
        const int NORMAL = 1;
        const int ACK = 10; // (Note: Send only header)
        const int HEART_BEAT_ACK = 11; // (Server sends this to find out phone status. Response done with heart beat)
        const int TERMINATE = 1000; // (Makes the server to remove this client from the room)

        const int LENGTH_AND_TYPE_BYTES = 4;
        const int MESSGAGE_START_BYTE = 8;

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Parses the incoming DeviceList message
        /// </summary>
        /// 
        /// <param name="incomingMessage">Byte message</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static DeviceList ParseDeviceList(byte[] incomingMessage)
        {
            int length = 0;
            int type = -1;

            ParseHeader(incomingMessage, ref length, ref type);

            byte[] messagePart = new byte[length];
            Buffer.BlockCopy(incomingMessage, MESSGAGE_START_BYTE, messagePart, 0, length);

            return DeviceList.ParseFrom(messagePart);
        }


        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Parses the incoming DeviceCommand message
        /// </summary>
        /// 
        /// <param name="incomingMessage">Byte message</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static DeviceStatus ParseDeviceStatus(byte[] incomingMessage)
        {
            int length = 0;
            int type = -1;

            ParseHeader(incomingMessage, ref length, ref type);

            byte[] messagePart = new byte[length];
            Buffer.BlockCopy(incomingMessage, MESSGAGE_START_BYTE, messagePart, 0, length);

            return DeviceStatus.ParseFrom(messagePart);
        }

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Parses the incoming DeviceCommand message
        /// </summary>
        /// 
        /// <param name="incomingMessage">Byte message</param>
        /// 
        //////////////////////////////////////////////////////////////
        public static SensDataList ParseSensorInfo(byte[] incomingMessage)
        {
            int length = 0;
            int type = -1;

            ParseHeader(incomingMessage, ref length, ref type);

            byte[] messagePart = new byte[length];
            Buffer.BlockCopy(incomingMessage, MESSGAGE_START_BYTE, messagePart, 0, length);

            Message msg = Message.ParseFrom(messagePart);

            return msg.SensorDataList;
        }

        //////////////////////////////////////////////////////////////
        ///
        /// <summary>
        /// Parses the incoming DeviceList message
        /// </summary>
        /// 
        /// <param name="incomingMessage">Byte message</param>
        /// 
        //////////////////////////////////////////////////////////////
        private static void ParseHeader(byte[] incomingMessage, ref int length, ref int type)
        {
            byte[] lengthArray = new byte[4];
            Array.Copy(incomingMessage, lengthArray, LENGTH_AND_TYPE_BYTES);

            // If the system architecture is little-endian (that is, little end first), 
            // reverse the byte array. 
            if (BitConverter.IsLittleEndian)
                Array.Reverse(lengthArray);

            // Length parsed
            length = BitConverter.ToInt32(lengthArray, 0);

            byte[] typeArray = new byte[4];
            Buffer.BlockCopy(incomingMessage, LENGTH_AND_TYPE_BYTES, typeArray, 0, LENGTH_AND_TYPE_BYTES);

            // If the system architecture is little-endian (that is, little end first), 
            // reverse the byte array. 
            if (BitConverter.IsLittleEndian)
                Array.Reverse(typeArray);

            // Type parsed
            type = BitConverter.ToInt32(typeArray, 0);

            // Check length and type
            if (length == 0)
            {
                throw new Exception("Devicelist message length is 0!");
            }
            
        }

    }

}