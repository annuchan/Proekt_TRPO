using System;
using System.Collections.Generic;
using System.Data;

using System.Windows;
using System.Data.SqlClient;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;

namespace Proekt_TRPO
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Window, INotifyPropertyChanged
    {

        private ObservableCollection<Student> students;
        public ObservableCollection<Student> Students
        {
            get { return students; }
            set
            {
                students = value;
                OnPropertyChanged("Students");
            }
        }

        private string groupLeaderInfo;
        public string GroupLeaderInfo
        {
            get { return groupLeaderInfo; }
            set
            {
                groupLeaderInfo = value;
                OnPropertyChanged("GroupLeaderInfo");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public class Student : INotifyPropertyChanged
        {
            private string name;
            public string Name
            {
                get { return name; }
                set
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }

            private bool isPresent;
            public bool IsPresent
            {
                get { return isPresent; }
                set
                {
                    isPresent = value;
                    OnPropertyChanged("IsPresent");
                }
            }
            private string lastName;
            public string LastName
            {
                get { return lastName; }
                set
                {
                    lastName = value;
                    OnPropertyChanged("LastName");
                }
            }

            private string firstName;
            public string FirstName
            {
                get { return firstName; }
                set
                {
                    firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }

            private string middleName;
            public string MiddleName
            {
                get { return middleName; }
                set
                {
                    middleName = value;
                    OnPropertyChanged("MiddleName");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public User()
        {
            try
            {
                InitializeComponent();
                DataContext = this;
                Students = new ObservableCollection<Student>();

                string connectionString = ConfigurationManager.ConnectionStrings["TRPOEntities"].ConnectionString;
                string query = "SELECT Фамилия, Имя, Отчество FROM Студенты"; // Пример SQL-запроса

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Student student = new Student
                                {
                                    LastName = reader["Фамилия"].ToString(),
                                    FirstName = reader["Имя"].ToString(),
                                    MiddleName = reader["Отчество"].ToString(),
                                };
                                Students.Add(student);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке данных из базы данных: {ex.Message}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Rasp rasp = new Rasp();
            rasp.Show();

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["TRPOEntities"].ConnectionString;

            // Создаем подключение к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Открываем подключение
                connection.Open();

                // Создаем команду SQL для вставки данных
                string query = "INSERT INTO Посещаемость4337 (Фамилия, Посещаемость) VALUES (@LastName, @Attendance)";
                SqlCommand command = new SqlCommand(query, connection);

                // Проходимся по коллекции студентов
                foreach (var student in Students)
                {
                    // Определяем значение для столбца Посещаемость в зависимости от состояния чекбокса
                    string attendanceStatus = student.IsPresent ? "Присутствует" : "Отсутствует";

                    // Устанавливаем параметры команды
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@LastName", student.LastName);
                    command.Parameters.AddWithValue("@Attendance", attendanceStatus);

                    // Выполняем команду SQL
                    command.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Данные успешно сохранены в таблицу Посещаемость4337.");
        }


        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}