using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace test3
{
    public partial class MainPage : ContentPage
    {
        //private GyroscopeTest gyro;
        public MainPage()
        {
            InitializeComponent();
            //gyro = new GyroscopeTest();
            //gyro.ToggleGyroscope();
            //Label x_value_label = new Label();
            //Binding gyro_binding = new Binding { Source = gyro, Path = "x" };
            //x_value_label.SetBinding(Label.TextProperty, gyro_binding);
        }        

        private void Button_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Старт")
                button1.Text = "Стоп";
            else
                button1.Text = "Старт";
            (this.Resources["gyro"] as GyroscopeTest).ToggleGyroscope();
        }
    }
}
