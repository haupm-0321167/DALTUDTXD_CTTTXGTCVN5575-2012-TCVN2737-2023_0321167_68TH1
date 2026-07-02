using DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Data;
using DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.Pages
{
    /// <summary>
    /// Interaction logic for ThongsoPage.xaml
    /// </summary>
    public partial class ThongsoPage : Page
    {
        private Dictionary<string, Diadiem> dsTinh =
            new Dictionary<string, Diadiem>();
        private List<XaGo> dsXaGo =
            new List<XaGo>();
        private XaGo xaGoDangChon;
        ObservableCollection<XagoModels> danhSach = new ObservableCollection<XagoModels>();
        int id = 1;
        public ThongsoPage()
        {
            InitializeComponent();

            LoadJson();

            dgCot.ItemsSource = danhSach;
        }
        private void LoadJson()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;

                string projectPath = Directory.GetParent(baseDir).Parent.Parent.FullName;

                string path = System.IO.Path.Combine(projectPath, "Data", "diadiem.json");

                string json = File.ReadAllText(path);

                dsTinh = JsonConvert.DeserializeObject
                    <Dictionary<string, Diadiem>>(json);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được file JSON!\n" + ex.Message);
            }
        }
        private void rbt_huybo_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }








        private void btn_them_click(object sender, RoutedEventArgs e)
        {

            try
            {
                var selectedLoai = (cbb_LoaiXG.SelectedItem as ComboBoxItem)?.Content.ToString();
                if (string.IsNullOrEmpty(selectedLoai))
                {
                    MessageBox.Show("Chọn loại xà gồ!");
                    return;
                }

                XagoModels xg = new XagoModels()
                {
                    Id = id++,
                    Height = double.Parse(txt_H.Text),
                    Width = double.Parse(txt_B.Text),
                    Lip = double.Parse(txt_C.Text),
                    Thickness = double.Parse(txt_t.Text),
                    Length = double.Parse(txt_D.Text),
                    ExtraWidth = double.Parse(txt_B1.Text),

                    A = xaGoDangChon.S,
                    G = xaGoDangChon.P,
                    Ix = xaGoDangChon.Jx,
                    Iy = xaGoDangChon.Jy,
                    Wx = xaGoDangChon.Wx,
                    Wy = xaGoDangChon.Wy,

                    Loai = selectedLoai
                };

                danhSach.Add(xg);
                dgCot.SelectedItem = xg;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_xoa_click(object sender, RoutedEventArgs e)
        {
            if (dgCot.SelectedItem != null)
            {
                danhSach.Remove((XagoModels)dgCot.SelectedItem);
            }
            else
            {
                MessageBox.Show("Chọn dòng cần xóa!");
            }
        }

        private void cbb_LoaiXG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = cbb_LoaiXG.SelectedItem as ComboBoxItem;
            if (item == null) return;

            string selected = item.Content.ToString();

            string loai = ((ComboBoxItem)cbb_LoaiXG.SelectedItem).Content.ToString();

            switch (loai)
            {
                case "XG C dập nguội":
                    LoadXaGo("xagocnguoi.json");
                    break;

                case "XG Z dập nguội":
                    LoadXaGo("xagozdn.json");
                    break;

                case "XG C cán nóng":
                    LoadXaGo("xagocnong.json");
                    break;

                case "XG Z cán nóng":
                    LoadXaGo("xagozn.json");
                    break;

                case "XG thép hộp chữ nhật":
                    LoadXaGo("xagohop.json");
                    break;

                case "XG thép hộp vuông":
                    LoadXaGo("xagov.json");
                    break;
            }

            DrawPreview2D();
        }


        private void LoadXaGo(string fileName)
        {
            try
            {
                string baseDir =
                    AppDomain.CurrentDomain.BaseDirectory;

                string projectPath =
                    Directory.GetParent(baseDir)
                    .Parent
                    .Parent
                    .FullName;

                string path =
                    System.IO.Path.Combine(
                        projectPath,
                        "Data",
                        fileName);

                string json =
                    File.ReadAllText(path);

                dsXaGo =
                    JsonConvert.DeserializeObject<List<XaGo>>(json);

                cbb_SoHieu.Items.Clear();

                foreach (var xg in dsXaGo)
                {
                    cbb_SoHieu.Items.Add(xg.SoHieu);
                }

                cbb_SoHieu.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbb_SoHieu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_SoHieu.SelectedItem == null)
                return;

            string soHieu = cbb_SoHieu.SelectedItem.ToString();

            xaGoDangChon = dsXaGo.FirstOrDefault(x => x.SoHieu == soHieu);

            if (xaGoDangChon == null)
                return;

            txt_H.Text = xaGoDangChon.H.ToString();
            txt_B.Text = xaGoDangChon.B.ToString();
            txt_C.Text = xaGoDangChon.C.ToString();
            txt_t.Text = xaGoDangChon.t.ToString();
            txt_B1.Text = xaGoDangChon.B1.ToString();
            txt_D.Text = xaGoDangChon.D.ToString();

            
            txtProp_A.Text = xaGoDangChon.S.ToString("0.00") + " cm²";
            txtProp_G.Text = xaGoDangChon.P.ToString("0.00") + " kg/m";
            txtProp_Ix.Text = xaGoDangChon.Jx.ToString("0.00") + " cm⁴";
            txtProp_Iy.Text = xaGoDangChon.Jy.ToString("0.00") + " cm⁴";
            txtProp_Wx.Text = xaGoDangChon.Wx.ToString("0.00") + " cm³";
            txtProp_Wy.Text = xaGoDangChon.Wy.ToString("0.00") + " cm³";
        }
        private void txt_Parameter_TextChanged(object sender, TextChangedEventArgs e)
        {
            DrawPreview2D();
        }

        private void canvas2D_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPreview2D();
        }

        private void DrawPreview2D()
        {
            if (canvas2D == null) return;
            canvas2D.Children.Clear();

            double w = canvas2D.ActualWidth;
            double h = canvas2D.ActualHeight;
            if (w == 0) w = 550;
            if (h == 0) h = 350;

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

           
            if (txt_H == null || txt_B == null || txt_C == null || txt_t == null || cbb_LoaiXG == null) return;
            if (!double.TryParse(txt_H.Text, out double H) || H <= 0) return;
            if (!double.TryParse(txt_B.Text, out double B) || B <= 0) return;
            double.TryParse(txt_C.Text, out double C);
            double.TryParse(txt_t.Text, out double t);
            if (t <= 0) t = 2.0;

            var selectedItem = cbb_LoaiXG.SelectedItem as ComboBoxItem;
            if (selectedItem == null) return;
            string type = selectedItem.Content.ToString();

            List<Point> pts = new List<Point>();

            if (type.Contains("C")) 
            {
                pts.Add(new Point(B, H - C));
                pts.Add(new Point(B, H));
                pts.Add(new Point(0, H));
                pts.Add(new Point(0, 0));
                pts.Add(new Point(B, 0));
                pts.Add(new Point(B, C));
            }
            else if (type.Contains("Z")) 
            {
                pts.Add(new Point(B, H - C));
                pts.Add(new Point(B, H));
                pts.Add(new Point(0, H));
                pts.Add(new Point(0, 0));
                pts.Add(new Point(-B, 0));
                pts.Add(new Point(-B, C));
            }
            else if (type.Contains("hộp chữ nhật") || type.Contains("hộp vuông")) // Box
            {
                pts.Add(new Point(0, 0));
                pts.Add(new Point(B, 0));
                pts.Add(new Point(B, H));
                pts.Add(new Point(0, H));
                pts.Add(new Point(0, 0));
            }

            if (pts.Count < 2) return;

           
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

           
            double r = 5.0; 
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
            else // Box
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

            
            SolidColorBrush dimBrush = new SolidColorBrush(Color.FromRgb(239, 68, 68)); // Red

            
            double dimX_H = centerX + minX * scale - 25;
            double startY_H = centerY - maxY * scale;
            double endY_H = centerY - minY * scale;

            
            Line lineH = new Line { X1 = dimX_H, Y1 = startY_H, X2 = dimX_H, Y2 = endY_H, Stroke = dimBrush, StrokeThickness = 1 };
            canvas2D.Children.Add(lineH);
           
            canvas2D.Children.Add(new Line { X1 = dimX_H - 5, Y1 = startY_H, X2 = dimX_H + 5, Y2 = startY_H, Stroke = dimBrush, StrokeThickness = 1 });
            canvas2D.Children.Add(new Line { X1 = dimX_H - 5, Y1 = endY_H, X2 = dimX_H + 5, Y2 = endY_H, Stroke = dimBrush, StrokeThickness = 1 });

            
            TextBlock labelH = new TextBlock
            {
                Text = $"H={H}",
                Foreground = dimBrush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(labelH, dimX_H - 35);
            Canvas.SetTop(labelH, (startY_H + endY_H) / 2 - 7);
            canvas2D.Children.Add(labelH);

            
            double dimY_B = centerY - minY * scale + 25;
            double startX_B = centerX + minX * scale;
            double endX_B = centerX + maxX * scale;

            
            Line lineB = new Line { X1 = startX_B, Y1 = dimY_B, X2 = endX_B, Y2 = dimY_B, Stroke = dimBrush, StrokeThickness = 1 };
            canvas2D.Children.Add(lineB);
            
            canvas2D.Children.Add(new Line { X1 = startX_B, Y1 = dimY_B - 5, X2 = startX_B, Y2 = dimY_B + 5, Stroke = dimBrush, StrokeThickness = 1 });
            canvas2D.Children.Add(new Line { X1 = endX_B, Y1 = dimY_B - 5, X2 = endX_B, Y2 = dimY_B + 5, Stroke = dimBrush, StrokeThickness = 1 });

            
            TextBlock labelB = new TextBlock
            {
                Text = $"B={B}",
                Foreground = dimBrush,
                FontSize = 10,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(labelB, (startX_B + endX_B) / 2 - 15);
            Canvas.SetTop(labelB, dimY_B + 7);
            canvas2D.Children.Add(labelB);

            
            TextBlock labelT = new TextBlock
            {
                Text = $"t = {t} mm",
                Foreground = new SolidColorBrush(Color.FromRgb(148, 163, 184)),
                FontSize = 11,
                FontWeight = FontWeights.Bold
            };
            Canvas.SetLeft(labelT, 10);
            Canvas.SetTop(labelT, h - 25);
            canvas2D.Children.Add(labelT);
        }
        private void btn_KiemTra_Click(object sender, RoutedEventArgs e)
        {
            var data = dgCot.SelectedItem as XagoModels;

            if (data == null)
            {
                MessageBox.Show("Vui lòng chọn một số hiệu xà gồ từ danh sách dưới đây!");
                return;
            }

            
            GlobalData.SelectedPurlin = data;
            GlobalData.B1 = data.ExtraWidth;
            GlobalData.A = data.A;
            GlobalData.G = data.G;
            GlobalData.Jx = data.Ix;
            GlobalData.Jy = data.Iy;
            GlobalData.Wx = data.Wx;
            GlobalData.Wy = data.Wy;

           
            taitrongview loadWindow = new taitrongview();
            loadWindow.ShowDialog();

           
            Tohoptaitrongview comboWindow = new Tohoptaitrongview();
            comboWindow.ShowDialog();

            
            KiemTraDieuKienBenView benWindow = new KiemTraDieuKienBenView();
            benWindow.ShowDialog();

            
            var main = Application.Current.MainWindow as MainWindow;
            if (main != null)
            {
                main.MainFrame.Navigate(new HomePage(data));
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
