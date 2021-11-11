using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;
using System.ComponentModel;
using System.IO;
using Xamarin.Forms;

namespace test3
{
    public class GyroscopeTest : INotifyPropertyChanged
    {
        // Set speed delay for monitoring changes.
        SensorSpeed speed = SensorSpeed.UI;

        private double z = 0;
        public double Z
        {
            get { return Math.Round(z, 7); }
            set
            {
                if (z != value)
                {
                    z = value;
                    OnPropertyChanged("Z");
                    OnPropertyChanged("Z_circle");
                }
            }
        }
        public double Z_circle
        {
            get 
            {
                double temp = z % 360;
                return Math.Round(((int)z - temp) / 360); 
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
            //gyroInfos = new List<string>();
        }

        void Gyroscope_ReadingChanged(object sender, GyroscopeChangedEventArgs e)
        {
            var data = e.Reading;
            // Process Angular Velocity Z reported in rad/s
            double temp = (180 / Math.PI) * data.AngularVelocity.Z / 15;
            if (Math.Abs(temp) > 0.01)
                Z += temp;
            //Console.WriteLine(DateTime.Now); // 15 раз в секунду +-1 раз
            DependencyService.Get<IWriteFile>().MyWriteTxtFile(DateTime.Now.ToString() + " | " + Z.ToString() + '\n');
            //Console.WriteLine($"Reading: X: {data.AngularVelocity.X}, Y: {data.AngularVelocity.Y}, Z: {data.AngularVelocity.Z}");
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
                    Z = 0;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
                DependencyService.Get<IWriteFile>().MyWriteTxtFile("Неизвестная ошибка " + fnsEx.ToString() + '\n');
            }
            catch (Exception ex)
            {
                // Other error has occurred.
                DependencyService.Get<IWriteFile>().MyWriteTxtFile("Ошибка " + ex.ToString() + '\n');
            }
        }
    }
}
