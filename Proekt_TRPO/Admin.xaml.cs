using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Windows;
using Microsoft.Win32;
using Excel = Microsoft.Office.Interop.Excel;

namespace Proekt_TRPO
{
    public partial class Admin : Window, INotifyPropertyChanged
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

            private string pos;
            public string Pos
            {
                get { return pos; }
                set
                {
                    pos = value;
                    OnPropertyChanged("Pos");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Admin()
        {
            InitializeComponent();
            DataContext = this;

            Students = new ObservableCollection<Student>();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TRPOEntities"].ConnectionString;
                string query = "SELECT Фамилия, Посещаемость FROM Посещаемость4337";

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
                                    Pos = reader["Посещаемость"].ToString(),
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

            private void Button_Click_1(object sender, RoutedEventArgs e)
            {
                SaveFileDialog sfd = new SaveFileDialog()
                {
                    DefaultExt = "*.xlsx",
                    Filter = "Файл Excel (*.xlsx)|*.xlsx",
                    Title = "Выберите место сохранения файла Excel"
                };

                if (sfd.ShowDialog() == true)
                {
                    string fileName = sfd.FileName;

                    try
                    {
                        List<Student> studentsList;
                        using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["TRPOEntities"].ConnectionString))
                        {
                            connection.Open();
                            string query = "SELECT Фамилия, Посещаемость FROM Посещаемость4337";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    studentsList = new List<Student>();
                                    while (reader.Read())
                                    {
                                        Student student = new Student
                                        {
                                            LastName = reader["Фамилия"].ToString(),
                                            Pos = reader["Посещаемость"].ToString()
                                        };
                                        studentsList.Add(student);
                                    }
                                }
                            }

                            // Создание новой книги Excel
                            Excel.Application excelApp = new Excel.Application();
                            excelApp.Visible = true;
                            Excel.Workbook workbook = excelApp.Workbooks.Add();

                            // Добавление нового листа Excel
                            Excel.Worksheet worksheet = workbook.Sheets.Add();
                            worksheet.Name = "Посещаемость";

                            // Запись заголовков столбцов
                            worksheet.Cells[1, 1] = "Фамилия";
                            worksheet.Cells[1, 2] = "Посещаемость";

                            // Запись данных
                            for (int i = 0; i < studentsList.Count; i++)
                            {
                                worksheet.Cells[i + 2, 1] = studentsList[i].LastName;
                                worksheet.Cells[i + 2, 2] = studentsList[i].Pos;
                            }

                            // Сохранение книги Excel
                            workbook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookDefault);
                            workbook.Close();
                            excelApp.Quit();

                            // Очистка списка и удаление данных из базы
                            studentsList.Clear();
                            string deleteQuery = "DELETE FROM Посещаемость4337";
                            using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection))
                            {
                                deleteCommand.ExecuteNonQuery();
                            }

                            MessageBox.Show("Данные успешно сохранены в файл Excel и удалены из базы данных.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при сохранении данных в файл Excel: {ex.Message}");
                    }
                }
            }



            private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
