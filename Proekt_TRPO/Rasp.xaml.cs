using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Configuration;
using System.Data.SqlClient;


namespace Proekt_TRPO
{
    /// <summary>
    /// Логика взаимодействия для Rasp.xaml
    /// </summary>
    public partial class Rasp : Window
    {
        public ObservableCollection<RaspisanieItem> Raspisanie { get; set; }

        public Rasp()
        {
            InitializeComponent();
            DataContext = this;

            Raspisanie = new ObservableCollection<RaspisanieItem>();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TRPOEntities"].ConnectionString;
                string query = "SELECT Расписание.Время, Расписание.Кабинет, Расписание.День_недели, Предметы.НазваниеПредмета AS Предмет, Преподаватели.Фамилия + ' ' + Преподаватели.Имя + ' ' + Преподаватели.Отчество AS Преподаватель " +
               "FROM Расписание " +
               "JOIN Предметы ON Расписание.IdПредмета = Предметы.Id " +
               "JOIN Преподаватели ON Расписание.IdПреподавателя = Преподаватели.Id";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                RaspisanieItem item = new RaspisanieItem
                                {
                                    Time = (TimeSpan)reader["Время"],
                                    Kab = reader["Кабинет"].ToString(),
                                    Day = reader["День_недели"].ToString(),
                                    Predmet = reader["Предмет"].ToString(),
                                    Prepod = reader["Преподаватель"].ToString()
                                };
                                Raspisanie.Add(item);
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
    }
}