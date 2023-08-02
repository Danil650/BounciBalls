using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BounciBalls.Classes
{
    internal class Ball
    {
        int BallSize = 10;
        double PositionX = 0;
        double PositionY = 0;
        float VelocityX = new Random().Next(-7,7);
        float VelocityY = new Random().Next(-7, 7);
        double GridMaxX = 0;
        double GridMaxY = 0;
        public Ball()
        {

        }

        public Ellipse BunceBall { get; set; }
        public Ball(double gridMaxX, double gridMaxY, MouseButtonEventArgs e, object sender, Grid BallsAreaGrid, Color color, int Size)
        {
            BallSize = Size;
            GridMaxX = gridMaxX;
            GridMaxY = gridMaxY;
            Point upPoint = e.GetPosition(sender as Grid);
            if (upPoint.X + BallSize < 0 || upPoint.X + BallSize > GridMaxX ||
                    upPoint.Y + BallSize < 0 || upPoint.Y + BallSize > GridMaxY)
                return;
            Mouse.Capture(null);
            Ellipse ellipsNew = new Ellipse();
            PositionX = upPoint.X;
            PositionY = upPoint.Y;
            ellipsNew.Margin = new Thickness(upPoint.X, upPoint.Y, 0, 0);
            ellipsNew.VerticalAlignment = VerticalAlignment.Top;
            ellipsNew.HorizontalAlignment = HorizontalAlignment.Left;
            ellipsNew.Fill = new SolidColorBrush(color);
            ellipsNew.Width = BallSize;
            ellipsNew.Height = BallSize;
            BunceBall = ellipsNew;
            BallsAreaGrid.Children.Add(ellipsNew);
        }
        public void MixColor(Color color)
        {
            BunceBall.Fill = new SolidColorBrush(color);
        }
        public void MovingBall()
        {
            if (PositionX < 0 || PositionX + BallSize > GridMaxX)
            {
                VelocityX = -VelocityX;
            }
            if (PositionY < 0 || PositionY + BallSize > GridMaxY)
            {
                VelocityY = -VelocityY;
            }
            PositionX += VelocityX;
            PositionY += VelocityY;
            BunceBall.Margin = new Thickness(PositionX, PositionY, 0, 0);
        }
    }
}
