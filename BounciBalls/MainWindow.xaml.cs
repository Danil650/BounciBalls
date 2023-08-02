using BounciBalls.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace BounciBalls
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool TimeIsTicking = false;
        List<Ball> Balls = new List<Ball>();
        List<Color> Colors = new List<Color>();
        int MaxSize = 10;
        public MainWindow()
        {
            InitializeComponent();
  
            Colors.Add(Color.FromRgb(128, 128, 128));
            Colors.Add(Color.FromRgb(255, 0, 0));
            Colors.Add(Color.FromRgb(255, 255, 0));
        }
        private DispatcherTimer timer = null;
        private void timerStart()
        {
            if(TimeIsTicking)
            {
                timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timerTick);
                timer.Interval = new TimeSpan(0, 0, 0, 0, 10);
                timer.Start();
            }
            else
            {
                timer.Stop();
                timer = null;
            }

        }
        private void timerTick(object sender, EventArgs e)
        {
            foreach (Ball ball in Balls)
            {
                ball.MovingBall();
            }
        }
        private void BallsAreaGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Ball newBall = new Ball();
            if (ColorBox.SelectedIndex == 0)
            {
                newBall = new Ball(BallsAreaGrid.ActualWidth, BallsAreaGrid.ActualHeight, e, sender, BallsAreaGrid, Colors[new Random().Next(0, ColorBox.Items.Count - 1)], new Random().Next(10, MaxSize));

            }
            else
            {
                newBall = new Ball(BallsAreaGrid.ActualWidth, BallsAreaGrid.ActualHeight, e, sender, BallsAreaGrid, Colors[ColorBox.SelectedIndex - 1], new Random().Next(10, MaxSize));

            }
            if (newBall.BunceBall != null)
            {
                Balls.Add(newBall);
                CountBallsLbl.Content = $"Колличество шаров на экране: {Balls.Count}";
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MaxSize = (int)MaxSizeSlider.Value;
        }

        private void CleanAreaBtn_Click(object sender, RoutedEventArgs e)
        {
            Balls = new List<Ball>();
            BallsAreaGrid.Children.Clear();
            CountBallsLbl.Content = "Колличество шаров на экране: 0";

        }

        private void StartMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            TimeIsTicking = true;

            timerStart();

            StartMoveBtn.IsEnabled = false;

            StopMoveBtn.IsEnabled = true;

        }

        private void StopMoveBtn_Click(object sender, RoutedEventArgs e)
        {
            TimeIsTicking = false;

            timerStart();

            StartMoveBtn.IsEnabled = true;

            StopMoveBtn.IsEnabled = false;
        }

        private void MixColorsBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Ball item in Balls)
            {
                item.MixColor(Colors[new Random().Next(0, Colors.Count)]);
            }
        }
    }
}
