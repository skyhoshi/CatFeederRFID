using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Windows.ApplicationModel.Background;
using Windows.Devices.Gpio;
using System.Diagnostics;
using Windows.Foundation;
using Windows.System.Threading;

namespace Cat_Feeder
{
    class Servo
    {
        #region fields
        private const int GPIO04_Servo_Pulse = 4;
        private GpioPin _servoPin;
        private ThreadPoolTimer _timer;
        private GpioController _controller = GpioController.GetDefault();
        //A pulse of 2ms moves the servo clockwise
        double _ForwardPulseWidth = 2;
        //A pulse of 1ms moves the servo counterclockwise
        double _BackwardPulseWidth = 1;
        double _PulseFrequency = 20;
        //the timing of the servo movement
        TimeSpan timeOfMovement = TimeSpan.FromSeconds(3);
        double currentPulseWidth;
        Stopwatch stopwatch;
        #endregion
        #region Properties
        /// <summary>
        /// A pulse of 2ms moves the servo clockwise
        /// </summary>
        public double ForwardPulseWidth
        {
            get
            {
                return _ForwardPulseWidth;
            }

            set
            {
                this._ForwardPulseWidth = value;
            }
        }
        /// <summary>
        /// A pulse of 1ms moves the servo counterclockwise
        /// </summary>
        public double BackwardPulseWidth
        {
            get
            {
                return _BackwardPulseWidth;
            }

            set
            {
                this._BackwardPulseWidth = value;
            }
        }

        public double PulseFrequency
        {
            get
            {
                return _PulseFrequency;
            }

            set
            {
                this._PulseFrequency = value;
            }
        }

        #endregion
        public Servo()
        {
            //Motor starts off
            currentPulseWidth = 0;
            //The stopwatch will be used to precisely time calls to pulse the motor.
            stopwatch = Stopwatch.StartNew();
            
            _servoPin = _controller.OpenPin(13);
            _servoPin.SetDriveMode(GpioPinDriveMode.Output);

            _timer = ThreadPoolTimer.CreatePeriodicTimer(this.Tick, TimeSpan.FromSeconds(2));

            //This will be initialized in the Weight Storage Class. 
            //You do not need to await this, as your goal is to have this run for the lifetime of the application
            //Windows.System.Threading.ThreadPool.RunAsync(this.MotorThread, Windows.System.Threading.WorkItemPriority.High);

        }
        /// <summary>
        /// If you pass in the already instanciated controller we will use it inplace of the default. 
        /// </summary>
        /// <param name="controller"></param>
        public Servo(GpioController controller) : this()
        {
            this._controller = controller;
        }
        private void Tick(ThreadPoolTimer timer)
        {
            if (currentPulseWidth != ForwardPulseWidth)
            {
                currentPulseWidth = ForwardPulseWidth;
            }
            else
            {
                currentPulseWidth = BackwardPulseWidth;
            }
        }
        private void ForwardMotor(IAsyncAction action) { }
        private void BackwardMotor(IAsyncAction action) { }

        private void MotorThread(IAsyncAction action)
        {
            //This motor thread runs on a high priority task and loops forever to pulse the motor as determined by the drive buttons
            while (true)
            {
                //If a button is pressed the pulsewidth is changed to cause the motor to spin in the appropriate direction
                //Write the pin high for the appropriate length of time
                if (currentPulseWidth != 0)
                {
                    _servoPin.Write(GpioPinValue.High);
                }
                //Use the wait helper method to wait for the length of the pulse
                Wait(currentPulseWidth);
                //The pulse if over and so set the pin to low and then wait until it's time for the next pulse
                _servoPin.Write(GpioPinValue.Low);
                Wait(PulseFrequency - currentPulseWidth);
            }
        }


        //A synchronous wait is used to avoid yielding the thread 
        //This method calculates the number of CPU ticks will elapse in the specified time and spins
        //in a loop until that threshold is hit. This allows for very precise timing.
        private void Wait(double milliseconds)
        {
            long initialTick = stopwatch.ElapsedTicks;
            long initialElapsed = stopwatch.ElapsedMilliseconds;
            double desiredTicks = milliseconds / 1000.0 * Stopwatch.Frequency;
            double finalTick = initialTick + desiredTicks;
            while (stopwatch.ElapsedTicks < finalTick)
            {

            }
        }
    }
}
