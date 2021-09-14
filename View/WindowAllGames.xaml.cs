using GamesManager.BL;
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
    /// Логика взаимодействия для WindowAllGames.xaml
    /// </summary>
    public partial class WindowAllGames : Window
    {
        private int actualList = 1;
        private int actualMax;

        private List<string> contentCbSort = new List<string>
        {
            "Без сортировки", "Есть в Steam" , "Есть в Epic Games" , "Есть в Ubisoft Store"
        };

        private List<BL.ViewGames> content = new List<BL.ViewGames>();
        public WindowAllGames()
        {
            InitializeComponent();
            lbContent.MouseDoubleClick += lbContent_MouseDoubleClick;
            try
            {
                content = GetContent();
                Run(content);
                cbSort.ItemsSource = contentCbSort;
                cbSort.SelectedIndex = 0;
            }
            catch
            {
                MessageBox.Show("Ошибкаqweqweq");
            }
        }

        private void lbContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var sours = e.OriginalSource as Border;
            if(sours == null)
            {
                return;
            }
            var games = sours.DataContext as BL.ViewGames;
            View.WindowChangeGame window = new WindowChangeGame();
            window.Show();
            this.Close();
        }


        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            var s = content.Where(x => x.Games.Name.ToUpper().StartsWith(tbSearch.Text.ToUpper())).ToList();
            s = s.Distinct().ToList();
            if (s.Count < 1)
            {
                MessageBox.Show("Обьект не найден");
                tbSearch.Text = string.Empty;
                Run(GetContent());
                return;
            }
            Run(s);
        }

        public static int GetCountButton(int count)
        {
            if(count % 15 == 0)
            {
                return count / 15;
            }
            else
            {
                return count / 15 + 1;
            }
        }

        public static int IntMin(int list)
        {
            return (list * 15) - 15;
        }

        public static int CountContent(int start, int Maxcount)
        {
            int rez = Maxcount - start;
            if (rez >= 15)
            {
                return 15;
            }
            else
            {
                return rez;
            }
        }
        
        private Button CreateButton (string name, string content, RoutedEventHandler action)
        {
            var b = new Button() { Name = name, Content = content, Margin = new Thickness(5) };
            b.Padding = new Thickness(4);
            b.Background = new SolidColorBrush(Color.FromArgb(255, 255, 193, 193));
            b.HorizontalAlignment = HorizontalAlignment.Center;
            b.Click += action;
            return b;
        }

        private void btDown_Click(object Sender, RoutedEventArgs e)
        {
            if (actualList > 1)
            {
                actualList--;
                var s = IntMin(actualList);
                RefreshContent(s, CountContent(s, content.Count), content);
            }
        }

        private void btNext_Click(object sender, RoutedEventArgs e)
        {
            var but = e.OriginalSource as Button;
            actualList = Convert.ToInt32(but.Content.ToString());
            var s = IntMin(actualList);
            RefreshContent(s, CountContent(s, content.Count), content);
        }
        private void btUp_Click(object sender, RoutedEventArgs e)
        {
            if(actualList < actualMax)
            {
                actualList++;
                var s = IntMin(actualList);
                RefreshContent(s, CountContent(s, content.Count), content);
            }
        }


        private void RefreshContent(int start, int end, List<ViewGames> games)
        {
            List<BL.ViewGames> s = new List<ViewGames>();
            if(games.Count> end && end > 0)
            {
                s.AddRange(games.GetRange(start, end));
            }
            else
            {
                return;
            }
            lbContent.ItemsSource = null;
            lbContent.ItemsSource = s;
            lbList.Content = $"Лист: {actualList}";
            lbCol.Content = $"Выведено: {lbContent.Items.Count} игр";
        }

        private void DinamycStackButton(int count)
        {
            spButtons.Children.Clear();
            int countButton = GetCountButton(count);
            spButtons.Children.Add(CreateButton("btDown", "<<", btDown_Click));

            for(int i = 0; i < GetCountButton(count); i++)
            {
                spButtons.Children.Add(CreateButton($"btNext{i}", $"{i + 1}", btNext_Click));
            }

            spButtons.Children.Add(CreateButton("btUp", ">>", btUp_Click));
        }

        private void Run(List<BL.ViewGames> games)
        {
            lbContent.ItemsSource = null;
            lbContent.ItemsSource = games;
            DinamycStackButton(games.Count);
            actualMax = spButtons.Children.Count - 2;
            var s = IntMin(actualList);
            RefreshContent(s, CountContent(s, games.Count), games);
            lbList.Content = $"Лист: {actualList}";
            lbCol.Content = $"Выведено: {lbContent.Items.Count} игр";
        }
        private List<ViewGames> GetContent()
        {
            try
            {
                return Controller.ControllerGame.GetViewGames();
            }
            catch
            {
                throw new Exception("Oshiblf");
            }
        }

        private void btAddGame_Click(object sender, RoutedEventArgs e)
        {
            View.WindowAddGame addGame = new WindowAddGame();
            addGame.Show();
            this.Close();
        }

        private void btExist_Click(object sender, RoutedEventArgs e)
        {
            View.WindowMenu menu = new WindowMenu();
            menu.Show();
            this.Close();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedItem.ToString())
            {
                case "Без сортировки": Run(GetContent()); break;
                case "Есть в Steam": SortSteam(); break;
                case "Есть в Epic Games": SortEpicGames(); break;
                case "Есть в Ubisoft Store": SortUbisoft(); break;
            }
        }

        private void SortUbisoft()
        {
            content = content.OrderBy(x => x.UbisoftID).ToList();
            Run(content);
        }

        private void SortEpicGames()
        {
            content = content.OrderBy(x => x.EpicID).ToList();
            Run(content);
        }

        private void SortSteam()
        {
            content = content.OrderBy(x => x.SteamID).ToList();
            Run(content);
        }
    }
}
