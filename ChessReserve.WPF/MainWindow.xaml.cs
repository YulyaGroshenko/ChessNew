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
using ChessReserve.Logic;

namespace ChessReserve.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Play_Click(object sender, RoutedEventArgs e)
        {
            btn_Play.Visibility = Visibility.Hidden;
            Name.Visibility = Visibility.Visible;
            btn_Ok.Visibility = Visibility.Visible;
        }
        private void Records_Click(object sender, RoutedEventArgs e)
        {
            RecordsWindow recordsWindow = new RecordsWindow();
            recordsWindow.Show();
            this.Close();
        }
        private void Options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            optionsWindow.Show();
            this.Close();
        }
        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            if (btn_Ok.ClickMode == ClickMode.Release && Name.Text != "")
            {
                GameWindow gameWindow = new GameWindow(new Game(Name.Text));
                gameWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Введите имя");
            }
        }
    }
}
