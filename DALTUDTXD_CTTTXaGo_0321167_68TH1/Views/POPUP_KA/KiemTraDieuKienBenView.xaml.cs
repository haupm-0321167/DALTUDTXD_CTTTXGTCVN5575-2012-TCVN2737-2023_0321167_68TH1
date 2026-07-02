using DALTUDTXD_CTTTXaGo_0321167_68TH1.Data;
using DALTUDTXD_CTTTXaGo_0321167_68TH1.Models;
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

namespace DALTUDTXD_CTTTXaGo_0321167_68TH1.Views.POPUP_KA
{
    /// <summary>
    /// Interaction logic for KiemTraDieuKienBenView.xaml
    /// </summary>
    public partial class KiemTraDieuKienBenView : Window
    {
        private ObservableCollection<KiemTraBen> dsKiemTra =
           new ObservableCollection<KiemTraBen>();
        public KiemTraDieuKienBenView()
        {
            InitializeComponent();

            dgKiemTra.ItemsSource = dsKiemTra;
            HienThiToHop();
        }

        private void HienThiToHop()
        {
            dsKiemTra.Clear();


            foreach (var th in GlobalData.DsNoiLucTinhToan)
            {
                double sigmaTd;

                if (GlobalData.B1 == 0)
                {
                    sigmaTd = 100 * (th.Mx / GlobalData.Wx + th.My / GlobalData.Wy);
                }
                else
                {
                    sigmaTd = 100 * (0.5 * th.Mx / GlobalData.Wx + 0.5 * th.My / GlobalData.Wy);
                }

                dsKiemTra.Add(new KiemTraBen()
                {
                    ToHop = th.Truonghop,
                    SigmaTd = Math.Round(sigmaTd, 2),
                    DauSoSanh = "<=",
                    SigmaChoPhep = 0,
                    NhanXet = ""
                });
            }
        }


        private void cbb_LoaiThep_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbb_LoaiThep.SelectedItem == null)
                return;

            string tenThep =
                ((ComboBoxItem)cbb_LoaiThep.SelectedItem)
                .Content.ToString();

            double fy = LayCuongDoThep(tenThep);
            GlobalData.Fy = fy;
            HienThiKetQua(fy);


        }

        private double LayCuongDoThep(string tenThep)
        {
            switch (tenThep)
            {
                case "Thép Nhật JIS G3101:SS400":
                    return 2450;

                case "Thép Nhật JIS G3302:G350":
                    return 3500;

                case "Thép Nhật JIS G3302:G450":
                    return 4500;

                case "Thép Nhật JIS G3302:G550":
                    return 5500;

                case "Thép Trung Quốc Q235":
                    return 2350;

                case "Thép Trung Quốc Q345":
                    return 3450;

                case "Thép Trung Quốc Q390":
                    return 3900;

                case "Thép Hoa Kỳ A36":
                    return 2500;

                case "Thép Hoa Kỳ A570":
                    return 3450;

                default:
                    return 0;
            }
        }
        private void HienThiKetQua(double fy)
        {
            foreach (var item in dsKiemTra)
            {
                item.SigmaChoPhep = fy;

                item.NhanXet =
                    item.SigmaTd <= fy
                    ? "ĐẠT"
                    : "KHÔNG ĐẠT";
            }

            dgKiemTra.Items.Refresh();
        }
        private void btn_KiemTraVong_Click(object sender, RoutedEventArgs e)
        {
            Close();
            KiemTraDieuKienVongView vongWindow = new KiemTraDieuKienVongView();
            vongWindow.ShowDialog();
        }
    }
}
