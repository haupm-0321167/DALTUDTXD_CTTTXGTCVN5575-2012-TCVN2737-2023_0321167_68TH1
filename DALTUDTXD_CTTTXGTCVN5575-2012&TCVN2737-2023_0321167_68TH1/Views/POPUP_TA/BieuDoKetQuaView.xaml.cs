using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class BieuDoKetQuaView : Window
    {
        public class TableRow
        {
            public int X_mm { get; set; }
            public double Value { get; set; }
            public string ValueFormatted { get; set; }
        }

        private string defaultDiagType = "Moment";
        private int numSpans = 5;
        private double spanL = 7.0; // in meters
        private double loadQ = 10.0; // in kN/m
        private int activeSpanIndex = 0; // 0-based
        private double[] supportMoments;

        private bool isUpdating = false;

        public BieuDoKetQuaView(string defaultDiag = "Moment")
        {
            InitializeComponent();
            this.defaultDiagType = defaultDiag;

            // Load default inputs from GlobalData
            double nhip = GlobalData.NhipXaGo;
            if (nhip <= 0) nhip = 7.0;
            this.spanL = nhip;
            txtL.Text = nhip.ToString("0.00");

            double q_load = (GlobalData.TongTinhTai + GlobalData.TongHoatTai) / 100.0;
            if (q_load <= 0) q_load = 10.0;
            this.loadQ = Math.Round(q_load, 2);
            txtQ.Text = this.loadQ.ToString("0.00");

            isUpdating = true;

            // Set diagram type combobox
            foreach (ComboBoxItem item in cbbDiagType.Items)
            {
                if (item.Tag.ToString() == defaultDiagType)
                {
                    cbbDiagType.SelectedItem = item;
                    break;
                }
            }

            // Fill Combos
            FillLoadCombinations();
            UpdateActiveSpanCombo();

            isUpdating = false;

            Loaded += (s, e) =>
            {
                RecalculateAndRedraw();
            };
        }

        private void FillLoadCombinations()
        {
            cbbCombo.Items.Clear();
            if (GlobalData.DsNoiLucTinhToan != null && GlobalData.DsNoiLucTinhToan.Count > 0)
            {
                foreach (var th in GlobalData.DsNoiLucTinhToan)
                {
                    cbbCombo.Items.Add(new ComboBoxItem { Content = th.Truonghop, Tag = th });
                }
                cbbCombo.SelectedIndex = 0;
            }
            else
            {
                cbbCombo.Items.Add(new ComboBoxItem { Content = "TH1: Tải trọng thiết kế quy đổi", Tag = null });
                cbbCombo.SelectedIndex = 0;
            }
        }

        private void UpdateActiveSpanCombo()
        {
            if (cbbActiveSpan == null) return;
            isUpdating = true;
            cbbActiveSpan.Items.Clear();
            for (int i = 0; i < numSpans; i++)
            {
                cbbActiveSpan.Items.Add(new ComboBoxItem { Content = $"Nhịp {i + 1}", Tag = i });
            }
            cbbActiveSpan.SelectedIndex = Math.Min(activeSpanIndex, numSpans - 1);
            isUpdating = false;
        }

        private void RecalculateAndRedraw()
        {
            if (isUpdating) return;
            if (sliderX == null || lblMaxSpanX == null || canvasOverview == null || canvasDetail == null || dgValues == null) return;

            // Solve support moments
            supportMoments = SolveSupportMoments(numSpans, spanL, loadQ);

            // Update UI sliders boundaries
            sliderX.Minimum = 0;
            sliderX.Maximum = spanL * 1000.0;
            sliderX.Value = spanL * 1000.0 / 2.0;
            lblMaxSpanX.Text = $"{(spanL * 1000.0):0} mm";

            // Draw Overview
            DrawOverview();

            // Draw Detail
            DrawDetail();

            // Fill Data Table
            FillDataTable();
        }

        private double[] SolveSupportMoments(int numSpans, double spanL, double loadQ)
        {
            int numSupports = numSpans + 1;
            double[] M = new double[numSupports];
            if (numSpans == 1)
            {
                M[0] = 0;
                M[1] = 0;
                return M;
            }

            int n = numSpans - 1; // number of unknowns (M_1 to M_{N-1})
            double[] a = new double[n]; // sub-diagonal
            double[] b = new double[n]; // main diagonal
            double[] c = new double[n]; // super-diagonal
            double[] d = new double[n]; // right hand side

            for (int i = 0; i < n; i++)
            {
                a[i] = 1.0;
                b[i] = 4.0;
                c[i] = 1.0;
                d[i] = -0.5 * loadQ * spanL * spanL;
            }

            // Thomas algorithm
            double[] cPrime = new double[n];
            double[] dPrime = new double[n];

            cPrime[0] = c[0] / b[0];
            dPrime[0] = d[0] / b[0];

            for (int i = 1; i < n; i++)
            {
                double denom = b[i] - a[i] * cPrime[i - 1];
                cPrime[i] = c[i] / denom;
                dPrime[i] = (d[i] - a[i] * dPrime[i - 1]) / denom;
            }

            double[] x = new double[n];
            x[n - 1] = dPrime[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                x[i] = dPrime[i] - cPrime[i] * x[i + 1];
            }

            // Copy to M
            M[0] = 0;
            M[numSpans] = 0;
            for (int i = 0; i < n; i++)
            {
                M[i + 1] = x[i];
            }

            return M;
        }

        private double GetValAtCoordinate(int spanIndex, double localX, string type)
        {
            if (supportMoments == null || spanIndex < 0 || spanIndex >= supportMoments.Length - 1) return 0;
            double M_left = supportMoments[spanIndex];
            double M_right = supportMoments[spanIndex + 1];

            if (type == "Moment")
            {
                // Bending moment
                return M_left * (1.0 - localX / spanL) + M_right * (localX / spanL) + (loadQ * localX * (spanL - localX)) / 2.0;
            }
            else if (type == "Shear")
            {
                // Shear Force
                return (M_right - M_left) / spanL + (loadQ * (spanL - 2.0 * localX)) / 2.0;
            }
            else // Deflection
            {
                // E = 2.1e5 N/mm2, I = Jx in cm4. Convert Jx to mm4
                double Jx = GlobalData.Jx;
                if (Jx <= 0) Jx = 200.0; // default inertia
                double E = 2.1e5; // N/mm2
                double I = Jx * 10000.0; // cm4 to mm4

                double q_N_mm = loadQ; // 1 kN/m = 1 N/mm
                double L_mm = spanL * 1000.0;
                double x_mm = localX * 1000.0;

                // Deflection formula
                double y_mm = (M_left * 1000000.0 * (x_mm * x_mm / 2.0 - x_mm * x_mm * x_mm / (6.0 * L_mm) - L_mm * x_mm / 3.0) +
                               M_right * 1000000.0 * (x_mm * x_mm * x_mm / (6.0 * L_mm) - L_mm * x_mm / 6.0) +
                               q_N_mm * (L_mm * x_mm * x_mm * x_mm / 12.0 - x_mm * x_mm * x_mm * x_mm / 24.0 - L_mm * L_mm * L_mm * x_mm / 24.0)) / (E * I);

                // return deflection in mm
                return Math.Abs(y_mm);
            }
        }
        private string GetDiagType()
        {
            return (cbbDiagType.SelectedItem as ComboBoxItem)?.Tag?.ToString() ?? "Moment";
        }

        private void DrawOverview()
        {
            if (supportMoments == null || canvasOverview == null) return;
            canvasOverview.Children.Clear();
            double w = canvasOverview.ActualWidth;
            double h = canvasOverview.ActualHeight;
            if (w == 0) w = 840;
            if (h == 0) h = 140;

            // CAD background grid
            DrawCADGrid(canvasOverview, w, h);

            double graphW = w - 80;
            double graphH = h - 60;
            double scaleX_all = graphW / (numSpans * spanL);

            string type = GetDiagType();

            // Find max absolute value for scaling
            double maxAbsVal = 0.001;
            int steps = 200;
            for (int i = 0; i <= steps; i++)
            {
                double totalX = (numSpans * spanL) * i / (double)steps;
                int sIndex = (int)(totalX / spanL);
                if (sIndex >= numSpans) sIndex = numSpans - 1;
                double localX = totalX - sIndex * spanL;
                double val = GetValAtCoordinate(sIndex, localX, type);
                if (Math.Abs(val) > maxAbsVal) maxAbsVal = Math.Abs(val);
            }

            double scaleY = (graphH * 0.45) / maxAbsVal;
            double baselineY = h / 2;

            // Baseline (Axis)
            canvasOverview.Children.Add(new Line
            {
                X1 = 40,
                Y1 = baselineY,
                X2 = 40 + graphW,
                Y2 = baselineY,
                Stroke = Brushes.White,
                StrokeThickness = 1.5
            });

            // Draw supports
            for (int i = 0; i <= numSpans; i++)
            {
                double sx = 40 + (i * spanL) * scaleX_all;
                DrawSupportTriangle(canvasOverview, sx, baselineY);
                // label support index
                DrawText(canvasOverview, (i + 1).ToString(), sx - 4, baselineY + 12, Brushes.LightGray, 9);
            }

            // Draw curve & hatch lines
            Polyline poly = new Polyline
            {
                Stroke = GetDiagramBrush(type),
                StrokeThickness = 2.2
            };

            for (int i = 0; i <= steps; i++)
            {
                double totalX = (numSpans * spanL) * i / (double)steps;
                int sIndex = (int)(totalX / spanL);
                if (sIndex >= numSpans) sIndex = numSpans - 1;
                double localX = totalX - sIndex * spanL;
                double val = GetValAtCoordinate(sIndex, localX, type);

                double px = 40 + totalX * scaleX_all;
                double py = baselineY + ((type == "Moment" || type == "Deflection") ? val * scaleY : -val * scaleY); // moment & deflection plotted downwards

                poly.Points.Add(new Point(px, py));

                // Draw bold hatch lines for overview
                Line hatch = new Line
                {
                    X1 = px,
                    Y1 = baselineY,
                    X2 = px,
                    Y2 = py,
                    Stroke = GetDiagramBrush(type),
                    StrokeThickness = 1.0,
                    Opacity = 0.65
                };
                canvasOverview.Children.Add(hatch);
            }
            canvasOverview.Children.Add(poly);
        }

        private void DrawDetail()
        {
            if (supportMoments == null || canvasDetail == null) return;
            canvasDetail.Children.Clear();
            double w = canvasDetail.ActualWidth;
            double h = canvasDetail.ActualHeight;
            if (w == 0) w = 840;
            if (h == 0) h = 320;

            // CAD background grid
            DrawCADGrid(canvasDetail, w, h);

            double graphW = w - 100;
            double graphH = h - 100;
            double scaleX_detail = graphW / spanL;

            string type = GetDiagType();

            // Find max absolute value in this span
            double maxAbsVal = 0.001;
            int steps = 100;
            for (int i = 0; i <= steps; i++)
            {
                double localX = spanL * i / (double)steps;
                double val = GetValAtCoordinate(activeSpanIndex, localX, type);
                if (Math.Abs(val) > maxAbsVal) maxAbsVal = Math.Abs(val);
            }

            double scaleY = (graphH * 0.45) / maxAbsVal;
            double baselineY = h / 2 - 20;

            // Baseline (Axis)
            canvasDetail.Children.Add(new Line
            {
                X1 = 50,
                Y1 = baselineY,
                X2 = 50 + graphW,
                Y2 = baselineY,
                Stroke = Brushes.White,
                StrokeThickness = 2
            });

            // Supports at ends
            DrawSupportTriangle(canvasDetail, 50, baselineY);
            DrawSupportTriangle(canvasDetail, 50 + graphW, baselineY);

            // Draw Dimension Line
            double dimY = baselineY + 50;
            canvasDetail.Children.Add(new Line { X1 = 50, Y1 = dimY, X2 = 50 + graphW, Y2 = dimY, Stroke = Brushes.LightGray, StrokeThickness = 1 });
            canvasDetail.Children.Add(new Line { X1 = 50, Y1 = dimY - 5, X2 = 50, Y2 = dimY + 5, Stroke = Brushes.LightGray, StrokeThickness = 1 });
            canvasDetail.Children.Add(new Line { X1 = 50 + graphW, Y1 = dimY - 5, X2 = 50 + graphW, Y2 = dimY + 5, Stroke = Brushes.LightGray, StrokeThickness = 1 });
            DrawText(canvasDetail, $"{(spanL * 1000.0):0} mm", 50 + graphW / 2 - 30, dimY - 18, Brushes.LightGray, 11);

            // Draw curve & vertical hatch lines
            Polyline poly = new Polyline
            {
                Stroke = GetDiagramBrush(type),
                StrokeThickness = 2.5
            };

            for (int i = 0; i <= steps; i++)
            {
                double localX = spanL * i / (double)steps;
                double val = GetValAtCoordinate(activeSpanIndex, localX, type);

                double px = 50 + localX * scaleX_detail;
                double py = baselineY + ((type == "Moment" || type == "Deflection") ? val * scaleY : -val * scaleY);

                poly.Points.Add(new Point(px, py));

                // Hatch lines (vertical stripes) - bold & dense
                Line hatch = new Line
                {
                    X1 = px,
                    Y1 = baselineY,
                    X2 = px,
                    Y2 = py,
                    Stroke = GetDiagramBrush(type),
                    StrokeThickness = 1.5,
                    Opacity = 0.75
                };
                canvasDetail.Children.Add(hatch);
            }
            canvasDetail.Children.Add(poly);

            // Label key values
            double supportLeftVal = GetValAtCoordinate(activeSpanIndex, 0, type);
            double supportRightVal = GetValAtCoordinate(activeSpanIndex, spanL, type);
            string unit = GetUnit(type);

            DrawText(canvasDetail, $"{supportLeftVal:0.000}", 35, baselineY - 20, Brushes.White, 10);
            DrawText(canvasDetail, $"{supportRightVal:0.000}", 50 + graphW - 15, baselineY - 20, Brushes.White, 10);

            // Redraw green cursor line based on slider value
            UpdateCursorLine();
        }

        private void UpdateCursorLine()
        {
            if (supportMoments == null || canvasDetail == null) return;
            // Remove previous cursor elements
            var itemsToRemove = canvasDetail.Children.OfType<FrameworkElement>()
                .Where(x => x.Name == "cursorLine" || x.Name == "cursorMarker" || x.Name == "cursorLabel")
                .ToList();
            foreach (var item in itemsToRemove)
            {
                canvasDetail.Children.Remove(item);
            }

            double w = canvasDetail.ActualWidth;
            double h = canvasDetail.ActualHeight;
            if (w == 0) w = 840;
            if (h == 0) h = 320;

            double graphW = w - 100;
            double graphH = h - 100;
            double scaleX_detail = graphW / spanL;
            double baselineY = h / 2 - 20;

            string type = GetDiagType();

            // Find scaleY
            double maxAbsVal = 0.001;
            int steps = 100;
            for (int i = 0; i <= steps; i++)
            {
                double lX = spanL * i / (double)steps;
                double v = GetValAtCoordinate(activeSpanIndex, lX, type);
                if (Math.Abs(v) > maxAbsVal) maxAbsVal = Math.Abs(v);
            }
            double scaleY = (graphH * 0.45) / maxAbsVal;

            double sliderXValue_m = sliderX.Value / 1000.0;
            double val = GetValAtCoordinate(activeSpanIndex, sliderXValue_m, type);

            double cx = 50 + sliderXValue_m * scaleX_detail;
            double cy = baselineY + ((type == "Moment" || type == "Deflection") ? val * scaleY : -val * scaleY);

            // Vertical cursor line
            Line vertLine = new Line
            {
                Name = "cursorLine",
                X1 = cx,
                Y1 = 20,
                X2 = cx,
                Y2 = h - 60,
                Stroke = new SolidColorBrush(Color.FromRgb(34, 197, 94)), // Neon Green
                StrokeThickness = 1.2
            };
            vertLine.StrokeDashArray = new DoubleCollection { 3, 3 };
            canvasDetail.Children.Add(vertLine);

            // Marker dot
            Ellipse dot = new Ellipse
            {
                Name = "cursorMarker",
                Width = 8,
                Height = 8,
                Fill = new SolidColorBrush(Color.FromRgb(34, 197, 94)),
                Stroke = Brushes.White,
                StrokeThickness = 1
            };
            Canvas.SetLeft(dot, cx - 4);
            Canvas.SetTop(dot, cy - 4);
            canvasDetail.Children.Add(dot);

            // Update label at cursor
            string unit = GetUnit(type);
            string valName = type == "Moment" ? "My" : (type == "Shear" ? "Q" : "f");
            lblCoordinateInfo.Text = $"Tọa độ x = {sliderX.Value:0} mm | Giá trị {valName} = {val:0.000} {unit}";
        }

        private void FillDataTable()
        {
            if (dgValues == null || colValueHeader == null) return;
            var rows = new ObservableCollection<TableRow>();
            double step = spanL / 20.0; // 21 rows total
            string type = GetDiagType();
            string unit = GetUnit(type);

            colValueHeader.Header = type == "Moment" ? "Moment My (kNm)" : (type == "Shear" ? "Lực cắt Q (kN)" : "Độ võng f (mm)");

            for (int i = 0; i <= 20; i++)
            {
                double localX = i * step;
                int x_mm = (int)Math.Round(localX * 1000.0);
                double val = GetValAtCoordinate(activeSpanIndex, localX, type);
                rows.Add(new TableRow
                {
                    X_mm = x_mm,
                    Value = val,
                    ValueFormatted = $"{val:0.000} {unit}"
                });
            }

            dgValues.ItemsSource = rows;
        }

        private void DrawCADGrid(Canvas cv, double w, double h)
        {
            // Dark grid background
            for (double xGrid = 30; xGrid < w; xGrid += 30)
            {
                Line gridLine = new Line { X1 = xGrid, Y1 = 0, X2 = xGrid, Y2 = h, Stroke = new SolidColorBrush(Color.FromRgb(30, 41, 59)), StrokeThickness = 0.5 };
                gridLine.StrokeDashArray = new DoubleCollection { 4, 4 };
                cv.Children.Add(gridLine);
            }
            for (double yGrid = 30; yGrid < h; yGrid += 30)
            {
                Line gridLine = new Line { X1 = 0, Y1 = yGrid, X2 = w, Y2 = yGrid, Stroke = new SolidColorBrush(Color.FromRgb(30, 41, 59)), StrokeThickness = 0.5 };
                gridLine.StrokeDashArray = new DoubleCollection { 4, 4 };
                cv.Children.Add(gridLine);
            }
        }

        private void DrawSupportTriangle(Canvas cv, double cx, double cy)
        {
            Polygon triangle = new Polygon
            {
                Fill = new SolidColorBrush(Color.FromRgb(22, 163, 74)), // green support
                Points = new PointCollection
                {
                    new Point(cx, cy),
                    new Point(cx - 8, cy + 12),
                    new Point(cx + 8, cy + 12)
                }
            };
            cv.Children.Add(triangle);
        }

        private void DrawText(Canvas cv, string text, double x, double y, Brush color, double size)
        {
            TextBlock tb = new TextBlock { Text = text, Foreground = color, FontSize = size, FontWeight = FontWeights.Bold };
            Canvas.SetLeft(tb, x);
            Canvas.SetTop(tb, y);
            cv.Children.Add(tb);
        }

        private Brush GetDiagramBrush(string type)
        {
            if (type == "Moment") return new SolidColorBrush(Color.FromRgb(16, 185, 129)); // Emerald Green
            if (type == "Shear") return new SolidColorBrush(Color.FromRgb(244, 63, 94)); // Rose
            return new SolidColorBrush(Color.FromRgb(56, 189, 248)); // Cyan
        }

        private string GetUnit(string type)
        {
            if (type == "Moment") return "kNm";
            if (type == "Shear") return "kN";
            return "mm";
        }

        private double ParseDoubleSafe(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0;
            string clean = new string(text.Where(c => char.IsDigit(c) || c == '.' || c == ',' || c == '-').ToArray());
            if (double.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double val))
            {
                return val;
            }
            if (double.TryParse(clean.Replace(".", ","), out val))
            {
                return val;
            }
            if (double.TryParse(clean.Replace(",", "."), out val))
            {
                return val;
            }
            return 0;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            double newL = ParseDoubleSafe(txtL.Text);
            double newQ = ParseDoubleSafe(txtQ.Text);
            if (newL > 0 && newQ >= 0)
            {
                spanL = newL;
                loadQ = newQ;
                RecalculateAndRedraw();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ!");
            }
        }

        private void Param_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbSpans == null || cbbDiagType == null || lblHeaderTitle == null || cbbActiveSpan == null) return;

            // Update numSpans
            if (cbbSpans.SelectedItem is ComboBoxItem itemSpan && int.TryParse(itemSpan.Tag.ToString(), out int s))
            {
                numSpans = s;
            }

            // Update activeDiag
            string diag = GetDiagType();
            lblHeaderTitle.Text = $"BIỂU ĐỒ NỘI LỰC CHI TIẾT - {diag.ToUpper()}";

            UpdateActiveSpanCombo();
            RecalculateAndRedraw();
        }

        private void ActiveSpan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isUpdating || cbbActiveSpan == null || cbbActiveSpan.SelectedItem == null || lblDetailTitle == null) return;

            if (cbbActiveSpan.SelectedItem is ComboBoxItem item && int.TryParse(item.Tag.ToString(), out int index))
            {
                activeSpanIndex = index;
                lblDetailTitle.Text = $"BIỂU ĐỒ CHI TIẾT NHỊP {activeSpanIndex + 1}";
                RecalculateAndRedraw();
            }
        }

        private void Combo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbbCombo == null || cbbCombo.SelectedItem == null || txtQ == null) return;

            if (cbbCombo.SelectedItem is ComboBoxItem item && item.Tag is ToHopTaiTrong th)
            {
                // Vector sum load q from th.Px and th.Py
                double loadVal = Math.Sqrt(th.Px * th.Px + th.Py * th.Py) / 100.0;
                if (loadVal <= 0) loadVal = 10.0;
                loadQ = Math.Round(loadVal, 2);
                txtQ.Text = loadQ.ToString("0.00");
                RecalculateAndRedraw();
            }
        }

        private void sliderX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderX == null || canvasDetail == null) return;
            UpdateCursorLine();
        }

        private void dgValues_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgValues == null || sliderX == null) return;
            if (dgValues.SelectedItem is TableRow row)
            {
                sliderX.Value = row.X_mm;
            }
        }

        private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RecalculateAndRedraw();
        }

        private void canvasDetail_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            double w = canvasDetail.ActualWidth;
            if (w == 0) w = 840;
            double graphW = w - 100;
            double scaleX_detail = graphW / spanL;

            double localX = (e.GetPosition(canvasDetail).X - 50) / scaleX_detail;
            if (localX < 0 || localX > spanL) return;

            sliderX.Value = localX * 1000.0;
        }
    }
}

