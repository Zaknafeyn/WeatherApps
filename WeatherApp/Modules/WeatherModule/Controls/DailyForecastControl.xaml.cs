using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherModule.Models;

namespace WeatherModule.Controls
{
    /// <summary>
    /// Interaction logic for DailyForecastControl.xaml
    /// </summary>
    public partial class DailyForecastControl 
    {
        public DailyForecastControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty WeatherForecastProperty = DependencyProperty.Register(
            "WeatherForecast", typeof(WeatherForecastModel), typeof(DailyForecastControl), new PropertyMetadata(default(WeatherForecastModel)));

        public WeatherForecastModel WeatherForecast
        {
            get { return (WeatherForecastModel) GetValue(WeatherForecastProperty); }
            set { SetValue(WeatherForecastProperty, value);}
        }

    }
}
