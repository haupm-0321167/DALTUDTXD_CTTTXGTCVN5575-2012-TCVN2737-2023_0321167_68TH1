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

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.POPUP_TA
{
    public partial class BIEUDOLUCCAT_VIEW : Window
    {
        double L = 6, q = 10;
        double scaleX;

        public BIEUDOLUCCAT_VIEW()
        {
            InitializeComponent();

            // Link actual data tightly
            double nhip = GlobalData.NhipXaGo;
            if (nhip <= 0) nhip = 6.0;
            this.L = nhip;
            txtL.Text = nhip.ToString("0.00");

            double q_load = (GlobalData.TongTinhTai + GlobalData.TongHoatTai) / 100.0;
            if (q_load <= 0) q_load = 10.0;
            this.q = Math.Round(q_load, 2);
            txtQ.Text = this.q.ToString("0.00");

            Loaded += (s, e) => Draw();
        }

        void Draw()
        {
            canvas.Children.Clear();
            double w = canvas.ActualWidth;
            double h = canvas.ActualHeight;
            if (w == 0) w = 780;
            if (h == 0) h = 420;

            // Draw background CAD grid
            for (double xGrid = 0; xGrid < w; xGrid += 30)
            {
                Line gridLine = new Line { X1 = xGrid, Y1 = 0, X2 = xGrid, Y2 = h, Stroke = new SolidColorBrush(Color.FromRgb(30, 41, 59)), StrokeThickness = 0.5 };
                gridLine.StrokeDashArray = new DoubleCollection { 4, 4 };
                canvas.Children.Add(gridLine);
            }
            for (double yGrid = 0; yGrid < h; yGrid += 30)
            {
                Line gridLine = new Line { X1 = 0, Y1 = yGrid, X2 = w, Y2 = yGrid, Stroke = new SolidColorBrush(Color.FromRgb(30, 41, 59)), StrokeThickness = 0.5 };
                gridLine.StrokeDashArray = new DoubleCollection { 4, 4 };
                canvas.Children.Add(gridLine);
            }

            double graphW = w - 100;
            double graphH = h - 100;
            scaleX = graphW / L;

            double maxQ = q * L / 2;
            double scaleY = maxQ > 0 ? (graphH * 0.4) / maxQ : 1.0;

            double baselineY = h / 2 - 20;

            // Draw Baseline (Axis)
            canvas.Children.Add(new Line
            {
                X1 = 50,
                Y1 = baselineY,
                X2 = 50 + graphW,
                Y2 = baselineY,
                Stroke = Brushes.White,
                StrokeThickness = 1.5
            });

            // Draw support indicators (triangle shape at ends)
            DrawSupport(canvas, 50, baselineY);
            DrawSupport(canvas, 50 + graphW, baselineY);

            // Shear diagram curve
            Polyline line = new Polyline
            {
                Stroke = new SolidColorBrush(Color.FromRgb(244, 63, 94)), // Rose/Red
                StrokeThickness = 3
            };

            for (int i = 0; i <= 100; i++)
            {
                double x = L * i / 100;
                double Q = q * (L / 2 - x);

                line.Points.Add(new Point(
                    50 + x * scaleX,
                    baselineY - Q * scaleY
                ));
            }
            canvas.Children.Add(line);

            // Label key values
            // Support labels
            DrawText(canvas, "A", 45, baselineY - 20, Brushes.White);
            DrawText(canvas, "B", 50 + graphW - 5, baselineY - 20, Brushes.White);

            // Max Shear at ends
            DrawText(canvas, $"+{maxQ:0.00} kN", 55, baselineY - maxQ * scaleY - 15, new SolidColorBrush(Color.FromRgb(244, 63, 94)));
            DrawText(canvas, $"-{maxQ:0.00} kN", 50 + graphW - 80, baselineY + maxQ * scaleY + 5, new SolidColorBrush(Color.FromRgb(244, 63, 94)));
        }
        private void DrawSupport(Canvas cv, double cx, double cy)
        {
            Polygon triangle = new Polygon
            {
                Fill = Brushes.LightGray,
                Points = new PointCollection
                {
                    new Point(cx, cy),
                    new Point(cx - 8, cy + 12),
                    new Point(cx + 8, cy + 12)
                }
            };
            cv.Children.Add(triangle);
        }

        private void DrawText(Canvas cv, string text, double x, double y, Brush color)
        {
            TextBlock tb = new TextBlock { Text = text, Foreground = color, FontSize = 10, FontWeight = FontWeights.Bold };
            Canvas.SetLeft(tb, x);
            Canvas.SetTop(tb, y);
            cv.Children.Add(tb);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(txtL.Text, out double newL) && newL > 0 &&
                double.TryParse(txtQ.Text, out double newQ) && newQ >= 0)
            {
                L = newL;
                q = newQ;
                Draw();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!");
            }
        }

        private void Canvas_Click(object sender, MouseButtonEventArgs e)
        {
            double w = canvas.ActualWidth;
            if (w == 0) w = 780;
            double graphW = w - 100;
            scaleX = graphW / L;

            double x = (e.GetPosition(canvas).X - 50) / scaleX;
            if (x < 0 || x > L) return;

            double Q = q * (L / 2 - x);
            txtStatus.Text = $"Tọa độ x = {x:0.00} m | Lực cắt Q = {Q:0.00} kN";
        }
    }
}
