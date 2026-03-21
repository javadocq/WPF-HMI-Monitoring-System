using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Threading;
using WPF_MES_Monitoring_System.Model;
using WPF_MES_Monitoring_System.ViewModel.Command;
using WPF_MES_Monitoring_System.ViewModel.Service;

namespace WPF_MES_Monitoring_System.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        // 불량률 데이터를 담을 시리즈
        public SeriesCollection DefectRateSeries { get; set; }

        private string selectedMachine = "전체";
        private ICollectionView logView;
        public ICollectionView LogView
        {
            get { return logView; }
        }

        public string SelectedMachine
        {
            get { return selectedMachine; }
            set { 
                selectedMachine = value; 
                OnPropertyChanged(); 
                ApplyFilter(); 
                UpdateChartData();
            }
        }


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


        private DispatcherTimer timer;
        public MainViewModel()
        {
            Logs = new ObservableCollection<MachineLog>();

            logView = CollectionViewSource.GetDefaultView(Logs);
            logView.Filter = FilterLogs; // 필터 조건 함수 연결

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2); // 2초마다 실행
            timer.Tick += Timer_Tick; // 생성 로그를 timer.Tick 이벤트에 연결
            timer.Start();
            // 초기 데이터 로드
            LoadDataFromDb();

            // 불량률 차트 초기화
            DefectRateSeries = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "불량률 (%)",
                    Values = new ChartValues<double>(),
                    PointGeometry = DefaultGeometries.Circle,
                    Stroke = Brushes.Crimson,
                    Fill = Brushes.Transparent,
                }
            };

            UpdateChartData();
        }

        private void UpdateChartData()
        {
            var targetList = SelectedMachine == "전체"
                ? Logs.Take(20).ToList()
                : Logs.Where(log => log.MachineName == SelectedMachine).Take(20).ToList();

            UpdateDefectRate(targetList);
        }

        private void UpdateDefectRate(List<MachineLog> logList)
        {
            if (logList.Count == 0)
            {
                return;
            }
            double defectCount = logList.Count(log => log.Status == "ERROR");
            double totalCount = logList.Count;
            double defectRate = (defectCount / totalCount) * 100;

            // 차트 값 (최근 10개만 업데이트)
            if (DefectRateSeries[0].Values.Count > 10)
                DefectRateSeries[0].Values.RemoveAt(0);

            DefectRateSeries[0].Values.Add(defectRate);
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

                // IObservableCollection을 새로 할당하는 대신 기존 컬렉션을 업데이트하여 UI 바인딩 유지
                // ICollectionView는 기존 컬렉션을 참조하기 때문에, 새로 할당하면 필터링이 깨질 수 있음
                Logs.Clear();
                foreach (var log in logList)
                {
                    Logs.Add(log);
                }
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
            UpdateChartData();
        }

        private void ApplyFilter()
        {
            logView.Refresh(); // 뷰를 새로고침해서 필터 적용
        }

        // 필터링 로직
        private bool FilterLogs(object obj)
        {
            if (obj is MachineLog log)
            {
                // "전체"가 선택된 경우 모든 로그를 보여줌
                if (SelectedMachine == "전체")
                    return true;
                // 선택된 머신 이름과 일치하는 로그만 보여줌
                return log.MachineName == SelectedMachine;
            }
            return false;
        }

        // 콤보박스에 뿌려줄 목록
        public List<string> MachineOptions { get; } = new List<string>{ "전체", "CNC-01", "PRESS-02", "ROBOT-03", "PACK-04" };

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
