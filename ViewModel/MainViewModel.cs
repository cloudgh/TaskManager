using System.Collections.ObjectModel;
using System.Windows.Input;
using System.ComponentModel;
using TaskManager.Model;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace TaskManager.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DispatcherTimer timer;
        private ObservableCollection<TaskItem> tasks = new ObservableCollection<TaskItem>();
        public ObservableCollection<TaskItem> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged("Tasks");
            }
        }
        private bool _hasBeenAsked;

        public bool HasBeenAsked
        {
            get => _hasBeenAsked;
            set
            {
                _hasBeenAsked = value;
                OnPropertyChanged(nameof(HasBeenAsked));
            }
        }
        public ICommand TaskCommand { get; private set; }
        

        public ObservableCollection<Employers> Employer { get; set; }
        private Employers selectedEmployers;
        public Employers SelectedEmployers
        {
            get { return selectedEmployers; }
            set
            {
                selectedEmployers = value;
                OnPropertyChanged("SelectedEmployers");
            }
        }

        public ICommand AddTaskCommand { get; set; }
        public ICommand ShowMessageBoxCommand { get; set; }

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

        private TaskItem selectedTask;
        public TaskItem SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged(nameof(SelectedTask));
            }
        }

        public MainViewModel()
        {
            Tasks = new ObservableCollection<TaskItem>();
            AddTaskCommand = new RelayCommand(AddTask);
            TaskCommand = new RelayCommand(StartTask);
            ShowMessageBoxCommand = new RelayCommand(ShowMessageBox);
            NewTask = new TaskItem();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMinutes(0.1); 
            timer.Tick += Timer_Tick;
            timer.Start();
            Employer = new ObservableCollection<Employers>
            {
                new Employers(0, "John", "Doe", "Manager"),
                new Employers(1, "Alice", "Smith", "Developer"),
                new Employers(2, "Bob", "Johnson", "Designer"),
                new Employers(3, "Emily", "Brown", "HR Specialist"),
                new Employers(4, "Michael", "Davis", "Marketing Manager"),
                new Employers(5, "Sarah", "Wilson", "Product Manager"),
                new Employers(6, "James", "Taylor", "Software Engineer"),
                new Employers(7, "Patricia", "Anderson", "Quality Assurance"),
                new Employers(8, "Robert", "Hernandez", "Sales Representative"),
                new Employers(9, "Jennifer", "Martinez", "Data Analyst"),
                new Employers(10, "William", "Jackson", "Graphic Designer"),
                new Employers(11, "Linda", "White", "System Administrator"),
                new Employers(12, "Richard", "Harris", "Technical Writer"),
                new Employers(13, "Barbara", "Clark", "Customer Support"),

            };
        }
      
        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckTasksStatus();
        }
        private void StartTask(object parameter)
        {
            if (SelectedTask != null && SelectedTask.Status !="в работе")
            {
                if (SelectedTask.StartDate.Date != DateTime.Now.Date)
                {
                    SelectedTask.StartDate = DateTime.Now;
                }
                SelectedTask.Status = "в работе";
            }
            else
            {
                MessageBox.Show("Задача уже в работе");
            }
        }

        //private void AddTask(object parameter)
        //{
        //    if (SelectedEmployers != null)
        //    {
        //        NewTask.AssignedTo = SelectedEmployers.FullEmployers;
        //    }

        //    tasks.Add(NewTask);
        //    SortTasks();

        //    NewTask = new TaskItem();
        //}

        //private void SortTasks()
        //{
        //    var sortedTasks = tasks.OrderBy(task => task.StartDate).ThenBy(task => task.Deadline).ToList();
        //    tasks.Clear();
        //    foreach (var task in sortedTasks)
        //    {
        //        tasks.Add(task);
        //    }
        //}
        private void AddTask(object parameter)
        {
            if (SelectedEmployers == null)
            {
                MessageBox.Show("Выберите исполнителя.");
                return;
            }

            if (string.IsNullOrEmpty(NewTask.Title))
            {
                MessageBox.Show("Заголовок должен быть заполнены.");
                return;
            }

            if (NewTask.StartDate < DateTime.Now.Date)
            {
                MessageBox.Show("Дата начала не может быть прошедшей.");
                return;
            }

            if (NewTask.Deadline < NewTask.StartDate)
            {
                MessageBox.Show("Дедлайн не может быть раньше даты начала.");
                return;
            }

            NewTask.AssignedTo = SelectedEmployers.FullEmployers;

            tasks.Add(NewTask);

            tasks = new ObservableCollection<TaskItem>(tasks.OrderBy(task => task.StartDate).ThenBy(task => task.Deadline));

            Tasks = new ObservableCollection<TaskItem>(tasks);

            NewTask = new TaskItem();
        }

        private void ShowMessageBox(object parameter)
        {
            if (SelectedTask != null && SelectedTask.Status=="в работе")
            {
                var result = MessageBox.Show($"Вы закончили задачу: {SelectedTask.Title}?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    SelectedTask.EndDate = DateTime.Now;
                    SelectedTask.Status = "завершено";
                    OnPropertyChanged(nameof(SelectedTask));
                    
                }
            }
            else { MessageBox.Show("Вы не можете закончить работу которую не начинали"); }
        }
        public void CheckTasksStatus()
        {
            foreach (var task in Tasks)
            {
                if (task.Status == "в ожидании" && DateTime.Now >= task.StartDate)
                {
                    var result = MessageBox.Show("Приступили ли вы к задаче " + task.Title + "?", "Подтверждение", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        task.Status = "в работе";
                    }
                    else
                    {
                        task.Status = "неактивный";
                        
                    }
                }
                if ((task.Status == "в ожидании" || task.Status == "в работе") && DateTime.Now.Date > task.Deadline)
                {
                    task.Status = "просрочено";
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
