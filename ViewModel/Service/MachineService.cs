using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;
using WPF_MES_Monitoring_System.Model;

namespace WPF_MES_Monitoring_System.ViewModel.Service
{
    public class MachineService
    {
        // 시뮬레이션 데이터
        private string[] machines = { "CNC-01", "PRESS-02", "ROBOT-03", "PACK-04" };
        private Random random = new Random();

        // 랜덤 로그 생성
        public MachineLog GenerateRandomLog()
        {
            var newLog = new MachineLog
            {
                Timestamp = DateTime.Now,
                MachineName = machines[random.Next(machines.Length)],
                Temperature = random.Next(20, 100),
                Status = random.Next(10) > 8 ? "ERROR" : "RUN",
                Pressure = random.Next(1, 10),
                LogMessage = "Simulated log message"
            };

            return newLog;
        }


        // 저장
        public void SaveLog(MachineLog log)
        {
            using (var conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<MachineLog>();
                conn.Insert(log);
            }
        }
    }
}
