using DALTUDTXD_CTTTXaGo_0321167_68TH1.Models;
using DALTUDTXD_CTTTXaGo_0321167_68TH1.ViewModels;
using DALTUDTXD_CTTTXaGo_0321167_68TH1.Data;

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


    }
}
