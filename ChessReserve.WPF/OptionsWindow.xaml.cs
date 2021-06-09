using ChessReserve.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChessReserve.WPF
{
    public partial class OptionsWindow : Window
    {
        public OptionsWindow()
        {
            InitializeComponent();
            Colors = new SolidColorBrush[] { Brushes.Chocolate, Brushes.DarkGray, Brushes.LightGreen, Brushes.LightBlue, Brushes.LightSalmon, Brushes.LightCoral };
        }
        public SolidColorBrush[] Colors { get; private set; }
        private void FirstColor_Click(object sender, RoutedEventArgs e)
        {
            OptionData.FirstCellColor = Array.IndexOf(Colors, (sender as Button).Background);
        }
        private void SecondColor_Click(object sender, RoutedEventArgs e)
        {
            OptionData.SecondCellColor = Array.IndexOf(Colors, (sender as Button).Background);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
