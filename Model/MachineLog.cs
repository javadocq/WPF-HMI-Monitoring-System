using SQLite;

using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace WPF_MES_Monitoring_System.Model
{
    public class MachineLog : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string MachineName { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string LogMessage { get; set; } = string.Empty;
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        private long _responseTime;
        public long ResponseTime
        {
            get => _responseTime;
            set
            {
                _responseTime = value;
                OnPropertyChanged(); 
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
