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
        public MainPage()
        {
            InitializeComponent();
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
