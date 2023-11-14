using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using TaskManager.Model;
using System;

namespace TaskManager.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TaskItem> Tasks { get; set; }

        public ICommand AddTaskCommand { get; set; }

        private TaskItem _newTask;
        public TaskItem NewTask
        {
            get { return _newTask; }
            set
            {
                _newTask = value;
                OnPropertyChanged("NewTask");
            }
        }


        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            AddTaskCommand = new RelayCommand(AddTask);
            NewTask = new TaskItem();
        }

        private void AddTask(object parameter)
        {
            Tasks.Add(NewTask);
            NewTask = new TaskItem();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
