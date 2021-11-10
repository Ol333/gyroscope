using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.ComponentModel;
using System.IO;

namespace test3
{
    public class GyroscopeTest : INotifyPropertyChanged
    {
        public List<string> gyroInfos;
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;

        private double x = 0;
        public double X
        {
            get { return x; }
            set
            {
                if (x != value)
                { 
                    x = value;
                    OnPropertyChanged("X");
                }
            }
        }
        private double y = 0;
        public double Y
        {
            get { return y; }
            set
            {
                if (y != value)
                {
                    y = value;
                    OnPropertyChanged("Y");
                }
            }
        }

        private double z = 0;
        public double Z
        {
            get { return z; }
            set
            {
                if (z != value)
                {
                    z = value;
                    OnPropertyChanged("Z");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public GyroscopeTest()
        {
            // Register for reading changes.
            Gyroscope.ReadingChanged += Gyroscope_ReadingChanged;
            gyroInfos = new List<string>();
        }

        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            // Process Angular Velocity X, Y, and Z reported in rad/s
            X += (180 / Math.PI) * data.AngularVelocity.X / 15;
            Y += (180 / Math.PI) * data.AngularVelocity.Y / 15;
            Z += (180 / Math.PI) * data.AngularVelocity.Z / 15;
            //Console.WriteLine(DateTime.Now); // 15 раз в секунду +-1 раз
            gyroInfos.Add(DateTime.Now.ToString() + ' ' + Z.ToString());
            Console.WriteLine($"Reading: X: {data.AngularVelocity.X}, Y: {data.AngularVelocity.Y}, Z: {data.AngularVelocity.Z}");
        }

        public void ToggleGyroscope()
        {
            try
            {
                if (Gyroscope.IsMonitoring)
                    Gyroscope.Stop();
                else
                {
                    Gyroscope.Start(speed);
                    X = 0;
                    Y = 0;
                    Z = 0;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}
