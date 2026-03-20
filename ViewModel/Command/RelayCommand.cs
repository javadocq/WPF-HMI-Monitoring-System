using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WPF_MES_Monitoring_System.ViewModel.Command
{
    internal class RelayCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        // 생성자에서 실행할 액션을 받아 저장
        private readonly Action _execute;

        public RelayCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object? parameter)
        {
            return true; // 항상 실행 가능하도록 설정 (필요에 따라 조건 추가 가능)
        }

        public void Execute(object? parameter)
        {
            _execute();
        }
    }
}
