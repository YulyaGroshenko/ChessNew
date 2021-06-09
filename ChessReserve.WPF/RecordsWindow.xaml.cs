using ChessReserve.Logic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace ChessReserve.WPF
{
    public partial class RecordsWindow : Window
    {
        public RecordsWindow()
        {
            InitializeComponent();
            RecordsList.Content = SetRecords();
        }
        public StringBuilder SetRecords()
        {
            string[] records = File.ReadAllLines($"{FileWorker.Records}");
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string rec in records)
            {
                stringBuilder.Append($"{rec}\n");
            }
            return stringBuilder;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Grid.Width = this.ActualWidth;
            Grid.Height = this.ActualHeight;
        }
    }
}
