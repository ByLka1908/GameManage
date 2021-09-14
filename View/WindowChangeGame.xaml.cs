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
using System.Windows.Shapes;

namespace GamesManager.View
{
    /// <summary>
    /// Логика взаимодействия для WindowChangeGame.xaml
    /// </summary>
    public partial class WindowChangeGame : Window
    {
        public BL.ViewGames Games { get; set; }
        public WindowChangeGame()
        {
            InitializeComponent();
        }

        public WindowChangeGame(BL.ViewGames games) : this()
        {
            Games = games;
            tbName.Text = Games.Games.Name;
            tbDescription.Text = Games.Games.Discription;
            tbPrice.Text = Convert.ToString(Games.Games.Price);
            try
            {
                cbSteam.ItemsSource = Controller.ControllerLibaryGames.GetSteamComboBox();
                cbEpic.ItemsSource = Controller.ControllerLibaryGames.GetEpicComboBox();
                cbUbisoft.ItemsSource = Controller.ControllerLibaryGames.GetUbisoftComboBox();

                var st = (cbSteam.ItemsSource as List<string>).
                    Single(x => x == games.Games.Steam.YesOrNo);
                cbSteam.SelectedIndex = cbSteam.Items.IndexOf(st);

                var ep = (cbEpic.ItemsSource as List<string>).
                    Single(x => x == games.Games.Epic.YesOrNo);
                cbSteam.SelectedIndex = cbSteam.Items.IndexOf(ep);

                var ub = (cbUbisoft.ItemsSource as List<string>).
                    Single(x => x == games.Games.Ubisoft.YesOrNo);
                cbSteam.SelectedIndex = cbSteam.Items.IndexOf(ub);

                cbImage.ItemsSource = Controller.ControllerImage.GetImages();
                cbImage.SelectedIndex = 1;
            }
            catch
            {
                MessageBox.Show("Error db");
            }
        }

        private void btRemove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show("Вы уверены что хотите удалить", "Удалить обьект?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Controller.ControllerGame.Remove(Games.Games);
                    MessageBox.Show("Обьект удалён");
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Controller.ControllerGame.ChaneGame(tbName.Text, tbDescription.Text, tbPrice.Text, cbSteam.SelectedItem, cbEpic.SelectedItem, cbUbisoft.SelectedItem, Games.Games))
                {
                    MessageBox.Show("Обьект сохранен");
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        private void btDn_Click(object sender, RoutedEventArgs e)
        {
            View.WindowAllGames window = new WindowAllGames();
            window.Show();
            this.Close();
        }
    }
}
