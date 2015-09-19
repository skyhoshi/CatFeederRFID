using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Devices.Enumeration;
using Windows.Devices.SerialCommunication;
using Windows.Storage.Streams;
using System.Threading;
using System.Threading.Tasks;


//https://learn.sparkfun.com/tutorials/easy-driver-hook-up-guide

namespace Cat_Feeder
{
    class Stepper
    {
        private SerialDevice serialPort = null;
        DataWriter dataWriteObject = null;
        DataReader dataReaderObject = null;
        public Stepper() {

        }
    }
}
