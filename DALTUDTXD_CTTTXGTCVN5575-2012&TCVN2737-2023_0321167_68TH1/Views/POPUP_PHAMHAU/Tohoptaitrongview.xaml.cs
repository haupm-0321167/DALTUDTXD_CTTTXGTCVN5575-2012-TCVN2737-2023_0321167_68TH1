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

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.POPUP_PHAMHAU
{
    /// <summary>
    /// Interaction logic for Tohoptaitrongview.xaml
    /// </summary>
    public partial class Tohoptaitrongview : Window
    {
        private ObservableCollection<ToHopTaiTrong> dsTinhToan =
            new ObservableCollection<ToHopTaiTrong>();

        private ObservableCollection<ToHopTaiTrong> dsTieuChuan =
            new ObservableCollection<ToHopTaiTrong>();

        public Tohoptaitrongview()
        {
            InitializeComponent();
            double gio = GlobalData.TaiTrongGio;
            double tinh = GlobalData.TongTinhTai;
            double hoat = GlobalData.TongHoatTai;
            double NhipXaGo = GlobalData.NhipXaGo;
            double B1 = GlobalData.B1;
            double A = GlobalData.A;
            double G = GlobalData.G;
            double Ix = GlobalData.Jx;
            double Iy = GlobalData.Jy;
            double Wx = GlobalData.Wx;
            double Wy = GlobalData.Wy;
            double TG = GlobalData.TyGiang;


            LoadTinhToan();
            LoadTieuChuan();

        }
        private void LoadTinhToan()
        {
            dsTinhToan.Clear();

            dsTinhToan.Add(new ToHopTaiTrong());

            dgnoiluctinhtoan.ItemsSource = dsTinhToan;
        }

        private void LoadTieuChuan()
        {
            dsTieuChuan.Clear();

            dsTieuChuan.Add(new ToHopTaiTrong());

            dgnoiluctieuchuan.ItemsSource = dsTieuChuan;
        }
        private void Truonghop_Changed(
    object sender,
    SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb == null || cb.SelectedItem == null)
                return;

            ComboBoxItem item =
                cb.SelectedItem as ComboBoxItem;

            string truonghop =
                item.Content.ToString();

            ToHopTaiTrong row =
                (ToHopTaiTrong)
                ((FrameworkElement)sender).DataContext;

            double TT = GlobalData.TongTinhTai;
            double HT = GlobalData.TongHoatTai;
            double W = GlobalData.TaiTrongGio;
            double B1 = GlobalData.B1;

            double A = GlobalData.A;
            double G = GlobalData.G;
            double Ix = GlobalData.Jx;
            double Iy = GlobalData.Jy;
            double Wx = GlobalData.Wx;
            double Wy = GlobalData.Wy;
            double TG = GlobalData.TyGiang;


            double alpha =
                Math.Atan(GlobalData.DoDocMai / 100.0);

            double L = GlobalData.NhipXaGo;

            if (truonghop.Contains("TH1: 1*TT + 1*HT"))
            {


                row.Px = (G * 1.05 + HT + TT) * Math.Sin(alpha);
                row.Py = (G * 1.05 + HT + TT) * Math.Cos(alpha);
                if (B1 == 0)
                {
                    row.Mx = row.Py * L * L / 8.0;
                    if (TG == 0)
                    {
                        row.My = row.Px * L * L / 8.0;
                    }
                    else
                        if (TG == 1)
                        {
                            row.My = row.Px * L * L / 32.0;
                        }
                        else
                            if (TG == 2)
                            {
                                row.My = row.Px * L * L / 40.0;
                            }
                            else row.My = row.Px * L * L / (4000 / 17.85);

                }
                else
                {
                    row.Mx = row.Py * L * L / 11.0;
                    if (TG == 0)
                    {
                        row.My = row.Px * L * L / 11.0;
                    }
                    else
                        if (TG == 1)
                        {
                            row.My = row.Px * L * L / 44.0;
                        }
                        else
                            if (TG == 2)
                            {
                                row.My = row.Px * L * L / 99.0;
                            }
                            else
                                if (TG == 3)
                                {
                                    row.My = row.Px * L * L / 176.0;
                                }
                                else row.My = row.Px * L * L / 275;

                }


            }

            else if (truonghop.Contains("TH2: 1*TT + 1*W"))
            {
                row.Px = (-G * 1.05 + W - TT) * Math.Sin(alpha);
                row.Py = (-G * 1.05 + W - TT) * Math.Cos(alpha);
                if (B1 == 0)
                {
                    row.Mx = row.Py * L * L / 8.0;
                    if (TG == 0)
                    {
                        row.My = row.Px * L * L / 8.0;
                    }
                    else
                        if (TG == 1)
                        {
                            row.My = row.Px * L * L / 32.0;
                        }
                        else
                            if (TG == 2)
                            {
                                row.My = row.Px * L * L / 40.0;
                            }
                            else row.My = row.Px * L * L / (4000 / 17.85);

                }
                else
                {
                    row.Mx = row.Py * L * L / 11.0;
                    if (TG == 0)
                    {
                        row.My = row.Px * L * L / 11.0;
                    }
                    else
                        if (TG == 1)
                        {
                            row.My = row.Px * L * L / 44.0;
                        }
                        else
                            if (TG == 2)
                            {
                                row.My = row.Px * L * L / 99.0;
                            }
                            else
                                if (TG == 3)
                                {
                                    row.My = row.Px * L * L / 176.0;
                                }
                                else row.My = row.Px * L * L / 275;

                }
            }

            row.Truonghop = truonghop;

            if (row == dsTinhToan.Last())
            {
                dsTinhToan.Add(new ToHopTaiTrong());

                dgnoiluctinhtoan.ItemsSource = null;
                dgnoiluctinhtoan.ItemsSource = dsTinhToan;
            }
            else
            {
                dgnoiluctinhtoan.Items.Refresh();
            }
        }
    }
}
