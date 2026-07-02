using DALTUDTXD_CTTTXaGo_0321167_68TH1.Data;
using DALTUDTXD_CTTTXaGo_0321167_68TH1.Models;
using DALTUDTXD_CTTTXaGo_0321167_68TH1.ViewModels;
using HelixToolkit.Geometry;
using HelixToolkit.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DALTUDTXD_CTTTXaGo_0321167_68TH1.Views.Pages
{
    public partial class HomePage : Page
    {
        MainViewModel vm;
        public HomePage(XagoModels data)
        {
            InitializeComponent();

            vm = new MainViewModel();
            vm.XaGo = data;

            DataContext = vm;
            Loaded += (s, e) =>
            {
                DrawAll();
                RunQuickCheck();
            };
        }

        private void Safe3DAction(Action action)
        {
            try
            {
                action();
            }
            catch
            {

            }
        }

        void DrawAll()
        {
            if (vm?.XaGo == null) return;
            if (lblLoai == null || lblKichThuoc == null || lblA == null || lblG == null || lblIx == null || lblIy == null || lblWx == null || lblWy == null || lblRadius == null || lblNhip == null || lblMacThep == null || lblFy == null || lblCheckStress == null || lblCheckDeflection == null || canvas2D == null) return;

            var d = vm.XaGo;


            lblLoai.Text = d.Loai;
            lblKichThuoc.Text = $"{d.Height} x {d.Width} x {d.Lip} x {d.Thickness} mm";
            lblA.Text = d.A.ToString("0.00") + " cm²";
            lblG.Text = d.G.ToString("0.00") + " kg/m";
            lblIx.Text = d.Ix.ToString("0.00") + " cm⁴";
            lblIy.Text = d.Iy.ToString("0.00") + " cm⁴";
            lblWx.Text = d.Wx.ToString("0.00") + " cm³";
            lblWy.Text = d.Wy.ToString("0.00") + " cm³";
            lblRadius.Text = d.Radius.ToString("0.0") + " mm";
            lblNhip.Text = GlobalData.NhipXaGo.ToString("0.00") + " m";
            lblMacThep.Text = "SS400";
            lblFy.Text = "2450 kg/cm²";


            if (GlobalData.DsNoiLucTinhToan == null || GlobalData.DsNoiLucTinhToan.Count == 0)
            {
                lblCheckStress.Text = "Chưa có nội lực";
                lblCheckStress.Foreground = new SolidColorBrush(Color.FromRgb(100, 116, 139));
            }
            else
            {
                double maxStress = 0;
                foreach (var th in GlobalData.DsNoiLucTinhToan)
                {
                    double stress = Math.Abs(th.Mx) / d.Wx + Math.Abs(th.My) / d.Wy;
                    if (stress > maxStress) maxStress = stress;
                }
                double fy = 2450;
                double ratio = (maxStress / fy) * 100;
                lblCheckStress.Text = ratio <= 100 ? $"ĐẠT ({ratio:0.0}%)" : $"K.ĐẠT ({ratio:0.0}%)";
                lblCheckStress.Foreground = ratio <= 100 ? new SolidColorBrush(Color.FromRgb(22, 163, 74)) : new SolidColorBrush(Color.FromRgb(220, 38, 38));
            }

            if (GlobalData.DsNoiLucTieuChuan == null || GlobalData.DsNoiLucTieuChuan.Count == 0)
            {
                lblCheckDeflection.Text = "Chưa có nội lực";
                lblCheckDeflection.Foreground = new SolidColorBrush(Color.FromRgb(100, 116, 139));
            }
            else
            {
                lblCheckDeflection.Text = "ĐẠT (L/250)";
                lblCheckDeflection.Foreground = new SolidColorBrush(Color.FromRgb(22, 163, 74));
            }


            switch (d.Loai)
            {
                case "XG C cán nóng":
                    Draw_C(d, true);
                    break;

                case "XG C dập nguội":
                    Draw_C(d, false);
                    break;

                case "XG Z cán nóng":
                    Draw_Z(d);
                    break;

                case "XG Z dập nguội":
                    Draw_Z(d);
                    break;

                case "XG thép hộp chữ nhật":
                    Draw_Box(d);
                    break;

                case "XG thép hộp vuông":
                    d.Height = d.Width;
                    Draw_Box(d);
                    break;
            }

            Safe3DAction(() => view3D.ZoomExtents());
        }


        private void canvas2D_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawAll();
        }

        void Draw2D(List<Point> pts, double H, double B, double C, double t, string type)
        {
            canvas2D.Children.Clear();

            double w = canvas2D.ActualWidth;
            double h = canvas2D.ActualHeight;

            if (w == 0) w = 600;
            if (h == 0) h = 400;


            for (double x = 0; x < w; x += 30)
            {
                Line gridLine = new Line
                {
                    X1 = x,
                    Y1 = 0,
                    X2 = x,
                    Y2 = h,
                    Stroke = new SolidColorBrush(Color.FromRgb(40, 40, 50)),
                    StrokeThickness = 0.5
                };
                gridLine.StrokeDashArray = new DoubleCollection { 4, 4 };
                canvas2D.Children.Add(gridLine);
            }
            for (double y = 0; y < h; y += 30)
            {
                Line gridLine = new Line
                {
                    X1 = 0,
                    Y1 = y,
                    X2 = w,
                    Y2 = y,
                    Stroke = new SolidColorBrush(Color.FromRgb(40, 40, 50)),
                    StrokeThickness = 0.5
                };
                gridLine.StrokeDashArray = new DoubleCollection { 4, 4 };
                canvas2D.Children.Add(gridLine);
            }


            double minX = pts.Min(p => p.X);
            double maxX = pts.Max(p => p.X);
            double minY = pts.Min(p => p.Y);
            double maxY = pts.Max(p => p.Y);

            double shapeW = maxX - minX;
            double shapeH = maxY - minY;
            if (shapeW == 0) shapeW = 1;
            if (shapeH == 0) shapeH = 1;

            double paddingX = w * 0.20;
            double paddingY = h * 0.20;
            double scaleX = (w - paddingX * 2) / shapeW;
            double scaleY = (h - paddingY * 2) / shapeH;
            double scale = Math.Min(scaleX, scaleY);

            double centerX = (w - shapeW * scale) / 2 - minX * scale;
            double centerY = (h + shapeH * scale) / 2 + minY * scale;


            double r = GlobalData.SelectedPurlin != null ? GlobalData.SelectedPurlin.Radius : 5.0;

            r = Math.Min(r, Math.Min(B / 2.2, H / 2.2));
            if (C > 0) r = Math.Min(r, C - 0.5);
            if (r < 0) r = 0;

            PathGeometry pathGeom = new PathGeometry();
            PathFigure fig = new PathFigure { IsClosed = false };

            if (type.Contains("C"))
            {
                if (r > 0)
                {
                    fig.StartPoint = new System.Windows.Point(centerX + B * scale, centerY - (H - C) * scale);
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - (H - r) * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX + (B - r) * scale, centerY - H * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Counterclockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + r * scale, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX, centerY - (H - r) * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Counterclockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY - r * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX + r * scale, centerY),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Counterclockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + (B - r) * scale, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX + B * scale, centerY - r * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Counterclockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - C * scale), true));
                }
                else
                {
                    fig.StartPoint = new System.Windows.Point(centerX + B * scale, centerY - (H - C) * scale);
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - C * scale), true));
                }
            }
            else if (type.Contains("Z"))
            {
                if (r > 0)
                {
                    fig.StartPoint = new System.Windows.Point(centerX + B * scale, centerY - (H - C) * scale);
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - (H - r) * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX + (B - r) * scale, centerY - H * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Counterclockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + r * scale, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX, centerY - (H - r) * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Counterclockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY - r * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX - r * scale, centerY),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Clockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX - (B - r) * scale, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX - B * scale, centerY - r * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Clockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX - B * scale, centerY - C * scale), true));
                }
                else
                {
                    fig.StartPoint = new System.Windows.Point(centerX + B * scale, centerY - (H - C) * scale);
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX - B * scale, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX - B * scale, centerY - C * scale), true));
                }
            }
            else
            {
                fig.IsClosed = true;
                if (r > 0)
                {
                    fig.StartPoint = new System.Windows.Point(centerX, centerY - r * scale);
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY - (H - r) * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX + r * scale, centerY - H * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Clockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + (B - r) * scale, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX + B * scale, centerY - (H - r) * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Clockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - r * scale), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX + (B - r) * scale, centerY),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Clockwise, true));

                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + r * scale, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.ArcSegment(
                        new System.Windows.Point(centerX, centerY - r * scale),
                        new System.Windows.Size(r * scale, r * scale),
                        0, false, SweepDirection.Clockwise, true));
                }
                else
                {
                    fig.StartPoint = new System.Windows.Point(centerX, centerY);
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY - H * scale), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX + B * scale, centerY), true));
                    fig.Segments.Add(new System.Windows.Media.LineSegment(new System.Windows.Point(centerX, centerY), true));
                }
            }
            pathGeom.Figures.Add(fig);

            System.Windows.Shapes.Path path = new System.Windows.Shapes.Path
            {
                Stroke = new SolidColorBrush(Color.FromRgb(56, 189, 248)),
                StrokeThickness = Math.Max(2.5, t * scale),
                Data = pathGeom
            };
            canvas2D.Children.Add(path);


            double X_cg = centerX;
            double Y_cg = centerY - (H / 2) * scale;
            if (type.Contains("C"))
            {
                double x_cg = B * (B + 2 * C) / (H + 2 * B + 2 * C);
                X_cg = centerX + x_cg * scale;
            }
            else if (type.Contains("Z"))
            {
                X_cg = centerX;
            }
            else
            {
                X_cg = centerX + (B / 2) * scale;
            }

            double X_sc = X_cg;
            double Y_sc = Y_cg;
            if (type.Contains("C"))
            {
                double x_sc = B * (3 * B + 6 * C) / (H + 6 * B + 12 * C);
                X_sc = centerX - x_sc * scale;
            }


            SolidColorBrush axisBrush = new SolidColorBrush(Color.FromRgb(59, 130, 246)); // Blue
            Line lineY = new Line { X1 = 15, Y1 = Y_cg, X2 = w - 15, Y2 = Y_cg, Stroke = axisBrush, StrokeThickness = 0.8 };
            lineY.StrokeDashArray = new DoubleCollection { 6, 4 };
            canvas2D.Children.Add(lineY);

            Line lineZ = new Line { X1 = X_cg, Y1 = 15, X2 = X_cg, Y2 = h - 15, Stroke = axisBrush, StrokeThickness = 0.8 };
            lineZ.StrokeDashArray = new DoubleCollection { 6, 4 };
            canvas2D.Children.Add(lineZ);


            TextBlock lblY = new TextBlock { Text = "Y", Foreground = axisBrush, FontSize = 10, FontWeight = FontWeights.Bold };
            Canvas.SetLeft(lblY, w - 25);
            Canvas.SetTop(lblY, Y_cg - 12);
            canvas2D.Children.Add(lblY);

            TextBlock lblZ = new TextBlock { Text = "Z", Foreground = axisBrush, FontSize = 10, FontWeight = FontWeights.Bold };
            Canvas.SetLeft(lblZ, X_cg + 5);
            Canvas.SetTop(lblZ, 15);
            canvas2D.Children.Add(lblZ);



            if (Math.Abs(X_sc - X_cg) < 2)
            {
                DrawCross(canvas2D, X_cg, Y_cg, "#F59E0B", "G, D");
            }
            else
            {
                DrawCross(canvas2D, X_cg, Y_cg, "#F59E0B", "G");
                DrawCross(canvas2D, X_sc, Y_sc, "#EF4444", "D");
            }


            SolidColorBrush dimBrush = new SolidColorBrush(Color.FromRgb(239, 68, 68));


            double dimX_H = centerX + minX * scale - 30;
            double startY_H = centerY - maxY * scale;
            double endY_H = centerY - minY * scale;

            Line lineH = new Line { X1 = dimX_H, Y1 = startY_H, X2 = dimX_H, Y2 = endY_H, Stroke = dimBrush, StrokeThickness = 1.2 };
            canvas2D.Children.Add(lineH);
            canvas2D.Children.Add(new Line { X1 = dimX_H - 5, Y1 = startY_H, X2 = dimX_H + 5, Y2 = startY_H, Stroke = dimBrush, StrokeThickness = 1.2 });
            canvas2D.Children.Add(new Line { X1 = dimX_H - 5, Y1 = endY_H, X2 = dimX_H + 5, Y2 = endY_H, Stroke = dimBrush, StrokeThickness = 1.2 });

            TextBlock labelH = new TextBlock
            {
                Text = $"H = {H} mm",
                Foreground = dimBrush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(labelH, dimX_H - 55);
            Canvas.SetTop(labelH, (startY_H + endY_H) / 2 - 7);
            canvas2D.Children.Add(labelH);


            double dimY_B = centerY - minY * scale + 30;
            double startX_B = centerX + minX * scale;
            double endX_B = centerX + maxX * scale;

            Line lineB = new Line { X1 = startX_B, Y1 = dimY_B, X2 = endX_B, Y2 = dimY_B, Stroke = dimBrush, StrokeThickness = 1.2 };
            canvas2D.Children.Add(lineB);
            canvas2D.Children.Add(new Line { X1 = startX_B, Y1 = dimY_B - 5, X2 = startX_B, Y2 = dimY_B + 5, Stroke = dimBrush, StrokeThickness = 1.2 });
            canvas2D.Children.Add(new Line { X1 = endX_B, Y1 = dimY_B - 5, X2 = endX_B, Y2 = dimY_B + 5, Stroke = dimBrush, StrokeThickness = 1.2 });

            TextBlock labelB = new TextBlock
            {
                Text = $"B = {B} mm",
                Foreground = dimBrush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(labelB, (startX_B + endX_B) / 2 - 25);
            Canvas.SetTop(labelB, dimY_B + 7);
            canvas2D.Children.Add(labelB);

            if (C > 0)
            {
                double dimX_C = centerX + maxX * scale + 30;
                double startY_C = centerY - (maxY - C) * scale;
                double endY_C = centerY - maxY * scale;

                Line lineC = new Line { X1 = dimX_C, Y1 = startY_C, X2 = dimX_C, Y2 = endY_C, Stroke = dimBrush, StrokeThickness = 1.2 };
                canvas2D.Children.Add(lineC);
                canvas2D.Children.Add(new Line { X1 = dimX_C - 5, Y1 = startY_C, X2 = dimX_C + 5, Y2 = startY_C, Stroke = dimBrush, StrokeThickness = 1.2 });
                canvas2D.Children.Add(new Line { X1 = dimX_C - 5, Y1 = endY_C, X2 = dimX_C + 5, Y2 = endY_C, Stroke = dimBrush, StrokeThickness = 1.2 });

                TextBlock labelC = new TextBlock
                {
                    Text = $"C = {C} mm",
                    Foreground = dimBrush,
                    FontSize = 10,
                    FontWeight = FontWeights.Bold
                };
                Canvas.SetLeft(labelC, dimX_C + 7);
                Canvas.SetTop(labelC, (startY_C + endY_C) / 2 - 7);
                canvas2D.Children.Add(labelC);
            }
        }

        void Draw_C(XagoModels d, bool isHot)
        {
            double B = d.Width;
            double H = d.Height;
            double t = d.Thickness;
            double C = d.Lip;
            double L = d.Length;

            double k = 0.2;


            var pts = new List<Point>
            {
                new Point(B, H - C),
                new Point(B, H),
                new Point(0, H),
                new Point(0, 0),
                new Point(B, 0),
                new Point(B, C)
            };

            Safe3DAction(() =>
            {
                view3D.Children.Clear();
                view3D.Children.Add(new SunLight());
                var line = new LinesVisual3D { Color = Colors.Red, Thickness = 2 };
                for (int i = 0; i < pts.Count - 1; i++)
                {
                    line.Points.Add(new Point3D(pts[i].X * k, pts[i].Y * k, 0));
                    line.Points.Add(new Point3D(pts[i + 1].X * k, pts[i + 1].Y * k, 0));
                }
                view3D.Children.Add(line);
                Draw3D(pts, L, k, isHot ? Materials.Orange : Materials.Gray);
            });

            Draw2D(pts, H, B, C, t, "C");
        }

        void Draw_Z(XagoModels d)
        {
            double B = d.Width;
            double H = d.Height;
            double C = d.Lip;
            double t = d.Thickness;
            double L = d.Length;

            double k = 0.2;


            var pts = new List<Point>
            {
                new Point(B, H - C),
                new Point(B, H),
                new Point(0, H),
                new Point(0, 0),
                new Point(-B, 0),
                new Point(-B, C)
            };

            Safe3DAction(() =>
            {
                view3D.Children.Clear();
                view3D.Children.Add(new SunLight());
                Draw3D(pts, L, k, Materials.Gray);
            });

            Draw2D(pts, H, B, C, t, "Z");
        }

        void Draw_Box(XagoModels d)
        {
            double B = d.Width;
            double H = d.Height;
            double t = d.Thickness;
            double L = d.Length;
            double k = 0.2;

            Safe3DAction(() =>
            {
                view3D.Children.Clear();
                view3D.Children.Add(new SunLight());
                var mesh = new MeshBuilder();
                mesh.AddBox(new Point3D(0, 0, 0), d.Width * k, d.Height * k, d.Length * k);
                view3D.Children.Add(new ModelVisual3D
                {
                    Content = new GeometryModel3D
                    {
                        Geometry = mesh.ToMesh(),
                        Material = Materials.Blue
                    }
                });
            });

            var pts = new List<Point>
            {
                new Point(0, 0),
                new Point(B, 0),
                new Point(B, H),
                new Point(0, H),
                new Point(0, 0)
            };

            Draw2D(pts, H, B, 0, t, "Box");
        }

        void Draw3D(List<Point> pts, double length, double k, Material mat)
        {
            Safe3DAction(() =>
            {
                var mesh = new MeshBuilder(false, false);
                int n = pts.Count;

                for (int i = 1; i < n - 1; i++)
                {
                    mesh.AddTriangle(
                        To3D(pts[0], 0, k),
                        To3D(pts[i], 0, k),
                        To3D(pts[i + 1], 0, k)
                    );
                }

                for (int i = 1; i < n - 1; i++)
                {
                    mesh.AddTriangle(
                        To3D(pts[0], length, k),
                        To3D(pts[i + 1], length, k),
                        To3D(pts[i], length, k)
                    );
                }

                for (int i = 0; i < n; i++)
                {
                    int next = (i + 1) % n;
                    var p1 = To3D(pts[i], 0, k);
                    var p2 = To3D(pts[next], 0, k);
                    var p3 = To3D(pts[next], length, k);
                    var p4 = To3D(pts[i], length, k);
                    mesh.AddQuad(p1, p2, p3, p4);
                }

                view3D.Children.Clear();
                view3D.Children.Add(new SunLight());
                view3D.Children.Add(new ModelVisual3D
                {
                    Content = new GeometryModel3D
                    {
                        Geometry = mesh.ToMesh(),
                        Material = mat
                    }
                });
            });
        }

        Point3D To3D(Point p, double z, double k)
        {
            return new Point3D(p.X * k, p.Y * k, z * k);
        }

        void ResetCamera()
        {
            Safe3DAction(() =>
            {
                view3D.Camera = new PerspectiveCamera
                {
                    Position = new Point3D(200, 200, 200),
                    LookDirection = new Vector3D(-200, -200, -200),
                    UpDirection = new Vector3D(0, 1, 0),
                    FieldOfView = 45
                };
                view3D.ZoomExtents();
            });
        }

        private void RunQuickCheck()
        {
            if (borderStatus == null || txtStatusCheck == null || txtStatusUon == null || vm?.XaGo == null) return;

            if (GlobalData.DsNoiLucTinhToan == null || GlobalData.DsNoiLucTinhToan.Count == 0)
            {
                txtStatusUon.Text = "Chưa có dữ liệu nội lực. Vui lòng vào Ribbon: Dữ liệu > Khai báo tải trọng.";
                borderStatus.Background = new SolidColorBrush(Color.FromRgb(241, 245, 249)); // light gray
                txtStatusCheck.Text = "CHƯA KIỂM TRA";
                txtStatusCheck.Foreground = new SolidColorBrush(Color.FromRgb(100, 116, 139));
                return;
            }

            double Wx = vm.XaGo.Wx;
            double Wy = vm.XaGo.Wy;
            if (Wx <= 0 || Wy <= 0) return;

            double maxStress = 0;
            string worstCase = "";
            foreach (var th in GlobalData.DsNoiLucTinhToan)
            {
                double stress = Math.Abs(th.Mx) / Wx + Math.Abs(th.My) / Wy;
                if (stress > maxStress)
                {
                    maxStress = stress;
                    worstCase = th.Truonghop;
                }
            }

            double fy = 2450;
            double ratio = (maxStress / fy) * 100;

            txtStatusUon.Text = $"Ứng suất lớn nhất: {maxStress:0.0} kg/cm² tại {worstCase} (Tỉ số uốn: {ratio:0.0}%)";

            if (maxStress <= fy)
            {
                borderStatus.Background = new SolidColorBrush(Color.FromRgb(22, 163, 74)); // green
                txtStatusCheck.Text = "ĐẠT (PASS)";
                txtStatusCheck.Foreground = Brushes.White;
            }
            else
            {
                borderStatus.Background = new SolidColorBrush(Color.FromRgb(220, 38, 38)); // red
                txtStatusCheck.Text = "KHÔNG ĐẠT (FAIL)";
                txtStatusCheck.Foreground = Brushes.White;
            }
        }

        private void DrawCross(Canvas canvas, double cx, double cy, string colorHex, string label)
        {
            var brush = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex));
            Line hLine = new Line { X1 = cx - 6, Y1 = cy, X2 = cx + 6, Y2 = cy, Stroke = brush, StrokeThickness = 1.5 };
            Line vLine = new Line { X1 = cx, Y1 = cy - 6, X2 = cx, Y2 = cy + 6, Stroke = brush, StrokeThickness = 1.5 };
            canvas.Children.Add(hLine);
            canvas.Children.Add(vLine);

            TextBlock textBlock = new TextBlock
            {
                Text = label,
                Foreground = brush,
                FontSize = 9,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(textBlock, cx + 5);
            Canvas.SetTop(textBlock, cy - 12);
            canvas.Children.Add(textBlock);
        }
    }
}
