﻿using ChessReserve.Logic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChessReserve.WPF
{
    public partial class GameWindow : Window
    {
        public GameWindow(Game game)
        {
            InitializeComponent();
            Canvas.Width = this.Width;
            Canvas.Height = this.Height;
            Field = game.Board.Field;
            Game = game;
            CreateField();
        }
        public Figure[,] Field { get; private set; }
        public Figure Figure { get; private set; }
        public Letters Letter { get; private set; }
        public int Digit { get; private set; }
        public bool SelectFigure { get; private set; } = true;
        public bool SelectCell { get; private set; }
        public Game Game { get; set; }
        private void CreateField()
        {
            for (int i = 0; i < 64; i++) // y
            {
                Button button = new Button()
                {
                    BorderThickness = new Thickness(1),
                    BorderBrush = Brushes.Black
                };
                button.Click += Button_Click;
                button.KeyDown += Button_KeyDown;
                Canvas.Children.Add(button);
            }
        }

        private void Button_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                SelectCell = false;
                SelectFigure = true;
            }
        }
        private void PrintField()
        {
            for (int i = 0; i < Canvas.Children.Count; i++)
            {
                Button button = Canvas.Children[i] as Button;
                button.Width = Canvas.Width / 8 - 1;
                button.Height = Canvas.Height / 8 - 4;
                if (Field[i / 8, i % 8] != null)
                {
                    button.Content = Field[i / 8, i % 8].Abbreviation;
                    button.Foreground = ChooseColor(Field[i / 8, i % 8]);
                }
                else
                {
                    button.Content = " ";
                }
                Canvas.SetLeft(button, button.Width * (i % 8));
                Canvas.SetTop(button, button.Height * (i / 8));
                PaintField(button, i);
            }
        }
        private void PaintField(Button button, int i)
        {
            OptionsWindow optionsWindow = new OptionsWindow();
            if (((i / 8) % 2 == 0 && i % 2 != 0) || ((i / 8) % 2 != 0 && i % 2 == 0))
                button.Background = optionsWindow.Colors[OptionData.FirstCellColor];
            if (((i / 8) % 2 != 0 && i % 2 != 0) || ((i / 8) % 2 == 0 && i % 2 == 0))
                button.Background = optionsWindow.Colors[OptionData.SecondCellColor];
        }
        private SolidColorBrush ChooseColor(Figure figure)
        {
            if (figure.Side == Sides.Black)
                return Brushes.Black;
            return Brushes.White;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = Canvas.Children.IndexOf(sender as Button);
            Digit = index / 8;
            Letter = (Letters)(index % 8);
            try
            {
                if (SelectFigure && Game.CheckSide(Field[Digit, (int)Letter]))
                {
                    Field[Digit, (int)Letter].Digit = Digit;
                    Field[Digit, (int)Letter].Letter = Letter;
                    Figure = Field[Digit, (int)Letter];
                    SelectFigure = false;
                    SelectCell = true;
                }
                else if (SelectCell)
                {
                    //Game.Cheats();
                    //if (Game.CheckEndGame())
                    //    EndGame(true);
                    Game.Play(Figure, Digit, Letter);
                    if (!Game.CheckEndGame())
                        EndGame(true);
                    SelectCell = false;
                    SelectFigure = true;
                    PrintField();
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Выберите клетку, на которой стоит фигура");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void EndGame(bool end)
        {
            Game.SaveGame(end);
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Canvas.Width = this.ActualWidth;
            Canvas.Height = this.ActualHeight;
            PrintField();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                EndGame(false);
        }
    }
}
