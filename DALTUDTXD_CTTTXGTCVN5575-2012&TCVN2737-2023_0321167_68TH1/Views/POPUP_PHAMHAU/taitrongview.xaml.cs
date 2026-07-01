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
using System.Windows.Shapes;

namespace DALTUDTXD_CTTTXGTCVN5575_2012_TCVN2737_2023_0321167_68TH1.Views.POPUP_PHAMHAU
{
    /// <summary>
    /// Interaction logic for taitrongview.xaml
    /// </summary>
    public partial class taitrongview : Window
    {


        private ObservableCollection<TaiTrongItem> dsTinhTai =
     new ObservableCollection<TaiTrongItem>();
        private ObservableCollection<TaiTrongItem> dsHoatTai =
     new ObservableCollection<TaiTrongItem>();

        private Dictionary<string, Diadiem> dsTinh =
           new Dictionary<string, Diadiem>();
        private double B_Nha = 0;
        private double H_Nha = 0;
        private int currentStep = 1;
        public taitrongview()
        {
            InitializeComponent();
            LoadJson();
            LoadTinh();
            LoadTinhTai();
            LoadHoatTai();

            // Pre-select house type based on user's initial selection in ChonnhaPage
            if (!string.IsNullOrEmpty(ChonnhaPage.LoaiMai))
            {
                foreach (ComboBoxItem item in cbb_loainha.Items)
                {
                    string content = item.Content.ToString();
                    if (content.Contains(ChonnhaPage.LoaiMai) ||
                        (ChonnhaPage.LoaiMai == "Mái dốc 1 mái" && content == "Nhà 1 mái dốc") ||
                        (ChonnhaPage.LoaiMai == "2 mái dốc" && content == "Nhà 2 mái dốc"))
                    {
                        cbb_loainha.SelectedItem = item;
                        break;
                    }
                }
            }
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
        private void LoadTinh()
        {
            cbb_tinh.Items.Clear();

            foreach (string tinh in dsTinh.Keys)
            {
                cbb_tinh.Items.Add(tinh);
            }
        }
        private void cbb_tinh_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (cbb_tinh.SelectedItem == null) return;

            string tinh = cbb_tinh.SelectedItem.ToString();

            cbb_huyen.Items.Clear();
            cbb_phuong.Items.Clear();



            foreach (string huyen in dsTinh[tinh].quan_huyen.Keys)
            {
                cbb_huyen.Items.Add(huyen);
            }
        }

        private void cbb_huyen_SelectionChanged(object sender,
            SelectionChangedEventArgs e)
        {
            if (cbb_tinh.SelectedItem == null ||
         cbb_huyen.SelectedItem == null)
                return;

            string tinh = cbb_tinh.SelectedItem.ToString();

            string huyen = cbb_huyen.SelectedItem.ToString();

            cbb_phuong.Items.Clear();

            if (dsTinh[tinh].quan_huyen == null)
                return;

            foreach (string xa in
                dsTinh[tinh].quan_huyen[huyen])
            {
                cbb_phuong.Items.Add(xa);
            }



        }

        private void Cbb_phuong_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txt_vunggio == null || txt_aplucgio == null || txt_diahinh == null)
                return;

            if (cbb_tinh.SelectedItem == null ||
       cbb_huyen.SelectedItem == null ||
       cbb_phuong.SelectedItem == null)
                return;

            string tinh =
                cbb_tinh.SelectedItem.ToString();

            Diadiem data = dsTinh[tinh];

            txt_vunggio.Text =
                data.vung_gio;

            txt_aplucgio.Text =
                data.ap_luc_gio.ToString();

            txt_diahinh.Text =
                data.dia_hinh;
        }
        private void TinhZe_Auto(object sender, EventArgs e)
        {
            if (txt_ze == null || txt_kze == null || txt_diahinh == null)
            {
                return;
            }
            if (B_Nha == 0 || H_Nha == 0)
            {
                Inputbh frm = new Inputbh();

                if (frm.ShowDialog() == true)
                {
                    B_Nha = frm.B;
                    H_Nha = frm.H;
                }
                else
                {
                    return;
                }
            }
            try
            {
                double Z = TinhZe(B_Nha, H_Nha);

                txt_ze.Text = Z.ToString("0.00");

                double kZe = Tinh_kZe(Z, txt_diahinh.Text);

                txt_kze.Text = kZe.ToString("0.000");
            }
            catch
            {

            }
        }
        private double TinhZe(double b, double h)
        {

            if (h <= b)
                return h;


            if (h <= 2 * b)
                return b;

            return h;
        }
        private double Tinh_kZe(double Ze, string diaHinh)
        {
            double zg = 0;
            double zmin = 0;
            double alpha = 0;

            switch (diaHinh)
            {
                case "A":
                    zg = 213.36;
                    zmin = 2.13;
                    alpha = 11.5;
                    break;

                case "B":
                    zg = 274.32;
                    zmin = 4.57;
                    alpha = 9.5;
                    break;

                case "C":
                    zg = 365.76;
                    zmin = 9.14;
                    alpha = 7.0;
                    break;

                default:
                    return 1.0;
            }


            if (Ze < zmin)
                Ze = zmin;

            double kZe = 2.01 *
                         Math.Pow(
                             Ze / zg,
                             2.0 / alpha);

            return kZe;
        }



        private void Tinhc_Auto(object sender, SelectionChangedEventArgs e)
        {
            if (cbb_loainha == null || txt_c == null)
                return;
            if (cbb_loainha.SelectedItem == null)
                return;
            string loaiNha =
                ((ComboBoxItem)cbb_loainha.SelectedItem)
                .Content.ToString();
            double c = 0;

            switch (loaiNha)
            {
                case "Nhà mái bằng":
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng F")
                        c = -1.2;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng G")
                        c = -0.9;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng H")
                        c = -0.7;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng I")
                        c = -0.2;


                    break;

                case "Nhà 1 mái dốc":

                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng F")
                        c = -2.31;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng G")
                        c = -1.3;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng H")
                        c = -0.81;


                    break;

                case "Nhà 2 mái dốc":

                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng F")
                        c = -1.67;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng G")
                        c = -1.3;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng H")
                        c = -0.69;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng I")
                        c = -0.59;

                    break;

                case "Nhà mái vòm":

                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng A")
                        c = -1.2;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng B")
                        c = -0.87;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "Vùng C")
                        c = -0.4;
                    if (cbb_vunggio.SelectedItem != null && ((ComboBoxItem)cbb_vunggio.SelectedItem).Content.ToString() == "-")
                        c = -0.4;

                    break;

                case "Nhà mái che":
                    c = -1.3;
                    break;
            }

            txt_c.Text = c.ToString("0.##");

            TinhZe_Auto(null, null);

        }
    }
}
