using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Proekt_TRPO
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;
            using (TRPOEntities db= new TRPOEntities()){
                var user = db.Старосты.FirstOrDefault(u => u.Имя == username && u.Фамилия == password);
                if (user != null)
                {

                    if (user.Имя == "Ирина")
                    {
                        User userPage = new User();
                        userPage.Show();
                        this.Close();
                    }
                    else
                    {
                        Admin adminPage = new Admin();
                        adminPage.Show();
                        this.Close();
                    }
                }
                else
                {
                    Admin adminPage = new Admin();
                    adminPage.Show();
                    this.Close();
                }
            }
        }
    }
}
