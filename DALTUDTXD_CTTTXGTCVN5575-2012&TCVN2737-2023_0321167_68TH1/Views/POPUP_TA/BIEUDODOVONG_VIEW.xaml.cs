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
    public partial class BIEUDODOVONG_VIEW : Window
    {
        double L = 6, q = 10;
        double scaleX;

        public BIEUDODOVONG_VIEW()
        {
            InitializeComponent();


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

            double maxDef = (5 * q * Math.Pow(L / 2, 2) * Math.Pow(L - L / 2, 2)) / 384;
            double scaleY = maxDef > 0 ? (graphH * 0.7) / maxDef : 1.0;

            double baselineY = h / 2 - 50;


            canvas.Children.Add(new Line
            {
                X1 = 50,
                Y1 = baselineY,
                X2 = 50 + graphW,
                Y2 = baselineY,
                Stroke = Brushes.White,
                StrokeThickness = 1.5
            });


            DrawSupport(canvas, 50, baselineY);
            DrawSupport(canvas, 50 + graphW, baselineY);


            Polyline line = new Polyline
            {
                Stroke = new SolidColorBrush(Color.FromRgb(56, 189, 248)), // Cyan
                StrokeThickness = 3
            };

            for (int i = 0; i <= 100; i++)
            {
                double x = L * i / 100;
                double y = (5 * q * Math.Pow(x, 2) * Math.Pow(L - x, 2)) / 384;

                line.Points.Add(new Point(
                    50 + x * scaleX,
                    baselineY + y * scaleY
                ));
            }
            canvas.Children.Add(line);


            DrawText(canvas, "A", 45, baselineY - 20, Brushes.White);
            DrawText(canvas, "B", 50 + graphW - 5, baselineY - 20, Brushes.White);


            double midX = 50 + (L / 2) * scaleX;
            double midY = baselineY + maxDef * scaleY;
            canvas.Children.Add(new Line { X1 = midX, Y1 = baselineY, X2 = midX, Y2 = midY, Stroke = Brushes.Gray, StrokeThickness = 0.8, StrokeDashArray = new DoubleCollection { 2, 2 } });
            DrawText(canvas, $"fmax = {maxDef:0.000} cm", midX - 45, midY + 10, new SolidColorBrush(Color.FromRgb(56, 189, 248)));
        }
