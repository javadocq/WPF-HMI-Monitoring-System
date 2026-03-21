using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using System.Windows.Threading;
using WPF_MES_Monitoring_System.Model;
using WPF_MES_Monitoring_System.ViewModel.Command;
using WPF_MES_Monitoring_System.ViewModel.Service;

namespace WPF_MES_Monitoring_System.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer timer;

        // UI에 바인딩 될 로그 컬렉션(즉시 반영)
        private ObservableCollection<MachineLog> logs;

        public ObservableCollection<MachineLog> Logs
        {
            get { return logs; }
            set
            {
                logs = value;
                OnPropertyChanged();
            }
        }

        // 버튼 클릭 시 동작할 Command
        public RelayCommand AddLogCommand { get; }

        

        public MainViewModel()
        {
            Logs = new ObservableCollection<MachineLog>();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2); // 2초마다 실행
            timer.Tick += Timer_Tick; // 생성 로그를 timer.Tick 이벤트에 연결
            timer.Start();
            // 초기 데이터 로드
            LoadDataFromDb();
        }

        public int TotalCount => Logs.GroupBy(x => x.MachineName) // 머신별로 그룹화
                                        .Select(g => g.First())  // 각 그룹에서 첫 번째 로그 선택 (중복 제거)
                                        .Count(); // 고유한 머신 개수 계산

        // 가동 중인 로그 개수 계산
        public int RunningCount => Logs.GroupBy(x => x.MachineName) // 머신별로 그룹화
                                        .Select(g => g.First())  // 각 그룹에서 첫 번째 로그 선택 (중복 제거)
                                        .Count(x => x.Status == "RUN"); // 그 중 가동 중인 것

        // 에러 상태인 로그 개수 계산
        public int ErrorCount => Logs.GroupBy(x => x.MachineName) // 머신별로 그룹화
                                        .Select(g => g.First())  // 각 그룹에서 첫 번째 로그 선택 (중복 제거)
                                        .Count(x => x.Status == "ERROR"); // 그 중 오류인 것

        private void LoadDataFromDb()
        {
            using (var conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<MachineLog>();

                var logList = conn.Table<MachineLog>().OrderByDescending(l => l.Timestamp).ToList();
                Logs = new ObservableCollection<MachineLog>(logList);
            }
        }

        private MachineService machineService = new MachineService();

        private void Timer_Tick(object? sender, EventArgs e)
        {
            var newLog = machineService.GenerateRandomLog();

            machineService.SaveLog(newLog);

            Logs.Insert(0, newLog);
            OnPropertyChanged(nameof(TotalCount));
            OnPropertyChanged(nameof(RunningCount));
            OnPropertyChanged(nameof(ErrorCount));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
