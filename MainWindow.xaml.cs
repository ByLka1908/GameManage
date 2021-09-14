using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GamesManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xamlS
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            tbName.Text = "bylka";
            tbPassword.Text = "123";
        }

        private void btGo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var user = BL.Auntifucation.AuntifucationUser(tbName.Text, tbPassword.Text);
                App.User = user;
                View.WindowMenu menu = new View.WindowMenu();
                menu.Show();
                this.Close();                
            }
            catch
            {
                MessageBox.Show("Ny ti i lox");
            }
        }

        private void btClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
