using System;
using System.Windows.Input;

namespace WPF_MES_Monitoring_System.ViewModel.Command
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        // 1. 매개변수가 있는 경우를 위한 생성자
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // 2. 매개변수가 없는 경우(Action)를 위한 생성자 (오버로딩)
        // 넘겨받은 Action을 Action<object?> 형태로 감싸서 저장합니다.
        public RelayCommand(Action execute) : this(_ => execute()) { }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute(parameter);

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}