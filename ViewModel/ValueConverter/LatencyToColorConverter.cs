
using System.Globalization;
using System.Windows.Data;

namespace WPF_MES_Monitoring_System.ViewModel.ValueConverter
{
    public class LatencyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value is long latency)
            {
                // 500ms 이상은 '지연(Slow)'으로 판단
                if (latency > 200)
                {
                    Console.WriteLine("dd");
                    return "Slow"; // 빨간색
                }
                return "Normal"; 
            }

            return "Normal";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
