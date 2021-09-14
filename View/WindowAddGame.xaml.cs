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
    /// Логика взаимодействия для WindowAddGame.xaml
    /// </summary>
    public partial class WindowAddGame : Window
    {
        public WindowAddGame()
        {
            InitializeComponent();
            this.Loaded += WindowAddGame_Loaded;
        }


        private void WindowAddGame_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Run();
            }
            catch
            {
                MessageBox.Show("Error");
            }
        }

        /// <summary>
        /// Кнопка закрыть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btDn_Click(object sender, RoutedEventArgs e)
        {
            View.WindowAllGames window = new WindowAllGames();
            window.Show();
            this.Close();
        }

        /// <summary>
        /// Запуск прогр
        /// </summary>
        private void Run()
        {
            cbEpic.ItemsSource = Controller.ControllerLibaryGames.GetEpicComboBox();
            cbSteam.ItemsSource = Controller.ControllerLibaryGames.GetSteamComboBox();
            cbUbisoft.ItemsSource = Controller.ControllerLibaryGames.GetUbisoftComboBox();
            cbImage.ItemsSource = Controller.ControllerImage.GetImages();            
        }

        /// <summary>
        /// Кнопка Добавить
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btAdd_Click(object sender, RoutedEventArgs e)
        {
            #region Валидация
            if (string.IsNullOrWhiteSpace(tbName.Text))
            {
                MessageBox.Show("Имя не задано");
                return;
            }

            if (string.IsNullOrWhiteSpace(tbPrice.Text))
            {
                MessageBox.Show("Укажиете цену");
                return;
            }
            try
            {
                double p = Convert.ToDouble(tbPrice.Text);
                if (p < 0)
                {
                    MessageBox.Show("Цена не может быть отрицательной");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Укажите правильный формат цены");
                return;
            }

            if (cbEpic.SelectedIndex < 0)
            {
                MessageBox.Show("Укажите есть ли у вас игра в Epic Games");
                return;
            }

            if(cbSteam.SelectedIndex < 0)
            {
                MessageBox.Show("Укажите есть ли у вас игра в Steam");
                return;
            }

            if (cbUbisoft.SelectedIndex < 0)
            {
                MessageBox.Show("Укажите есть ли у вас игра в Ubisoft Store");
                return;
            }
            #endregion

            try
            {
                if (Controller.ControllerGame.AddGame(tbName.Text, tbDescription.Text, tbPrice.Text, cbSteam.SelectedItem, cbEpic.SelectedItem, cbUbisoft.SelectedItem))
                {
                    MessageBox.Show("Обьект добавлен в бд");
                }
                else
                {
                    MessageBox.Show("Обьект не добавлен в бд");
                }
            }
            catch
            {
                MessageBox.Show("Ошибка добавления в бд");
            }
        }
    }
}
