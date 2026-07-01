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

