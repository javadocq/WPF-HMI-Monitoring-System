using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace WPF_MES_Monitoring_System.Model
{
    public class MachineLog
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string MachineName { get; set; }
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public string Status { get; set; }

        public string LogMessage { get; set; }
    }
}
