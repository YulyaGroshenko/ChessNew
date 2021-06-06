using ChessReserve.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
